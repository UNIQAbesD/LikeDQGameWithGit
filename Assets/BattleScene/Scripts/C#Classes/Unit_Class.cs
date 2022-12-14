using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//const int MaxBuffExclusiveID = 100;

[System.Serializable]
public class BattleUnit
{
    [SerializeField] private UnitStatus _status;
    public UnitStatus status { get { return _status; } }

    [SerializeField] private int _curHP;
    public int curHP { get { return _curHP; } }
    [SerializeField] private int _curMP;
    public int curMP { get { return _curMP; } }

    private List<BuffParam> _buffParams;
    public List<BuffParam> buffParams { get { return _buffParams; } }

    public void applyAcSkillEfc(AcSkillEfc acSkillEfc)
    {
        BattleUnit sUnit = acSkillEfc.sUnit;
        BattleUnit oUnit = acSkillEfc.oUnit;
        _curHP -= acSkillEfc.hpDamage;
        _curMP -= acSkillEfc.mpDamage;

        //buffの更新処理を行う
        foreach (BuffParam aSkillEfcBuff in acSkillEfc.buffParam)
        {
            if (aSkillEfcBuff.integrateID < 0) 
            {
                _buffParams.Add(aSkillEfcBuff);
            }
            else 
            {
                bool isIntegrated = false;
                /*
                for (int i=0;i< acSkillEfc.buffParam.Count;i++)
                {
                    BuffParam aUnitBuff=acSkillEfc.buffParam[i];
                    if (aUnitBuff.integrateID == aSkillEfcBuff.integrateID) 
                    {
                        
                        acSkillEfc.buffParam[i]=aUnitBuff.whenApplySameIDBuff(aSkillEfcBuff);
                        acSkillEfc.buffParam[i].whosBuff = this;
                        isIntegrated =true;
                        break;
                        
                    }
                }
                */
                for (int i=0;i< _buffParams.Count;i++)
                {
                    //BuffParam aUnitBuff=_buffParams[i];
                    if (_buffParams[i].integrateID == aSkillEfcBuff.integrateID) 
                    {
                        _buffParams[i]= _buffParams[i].whenApplySameIDBuff(aSkillEfcBuff);
                        _buffParams[i].whosBuff = this;
                        isIntegrated =true;
                        break;
                        
                    }
                }
                if (!isIntegrated) 
                {
                    _buffParams.Add(aSkillEfcBuff);
                    aSkillEfcBuff.whosBuff = this;
                }
            }
        }
    }

    public BattleUnit()
    {

    }
}

[System.Serializable]
public class UnitStatus
{
    [SerializeField] private int _maxHP;
    public int maxHP { get { return _maxHP; } }
    [SerializeField] private int _atk;
    public int atk { get { return _atk; } }
    [SerializeField] private int _def;
    public int def { get { return _def; } }
    [SerializeField] private int _spow;
    public int satk { get { return _spow; } }
    [SerializeField] private int _sdef;
    public int sdef { get { return _sdef; } }
    [SerializeField] private int _spd;
    public int spd { get { return _spd; } }

    [SerializeField] List<ElementType> _types;
    public List<ElementType> types { get {List<ElementType> r=new List<ElementType>(); r.AddRange(_types); return r; } }

    [SerializeField] private int _heavy;
    public int heavy { get { return _heavy; } }

    [SerializeField] private List<SkillIdentity> _skills;
    public List<SkillIdentity> Skills { get { return _skills; } }

    [SerializeField] private AbilityIdentity _ability;
    public AbilityIdentity ability { get { return _ability; } }

    
}




//enum-------------------------------------------------------------------------------------
public enum ElementType 
{
    None,
    Normal,
    Fire,
    Water,
    Grass,
    Electric,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dark,
    Dragon,
    Steel,
    EnumEnd
}

