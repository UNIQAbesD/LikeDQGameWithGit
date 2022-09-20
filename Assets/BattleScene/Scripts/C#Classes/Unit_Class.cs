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

        //buffÇÃçXêVèàóùÇçsÇ§
        foreach (BuffParam aSkillEfcBuff in acSkillEfc.buffParam)
        {
            if (aSkillEfcBuff.identity.integrateID < 0) 
            {
                _buffParams.Add(aSkillEfcBuff);
            }
            else 
            {
                bool isIntegrated = false;
                foreach (BuffParam aUnitBuff in acSkillEfc.buffParam)
                {
                    
                    if (aUnitBuff.identity.integrateID == aSkillEfcBuff.identity.integrateID) 
                    {
                        aUnitBuff.identity. whenApplySameIDBuff(aUnitBuff, aSkillEfcBuff);
                        isIntegrated=true;
                        break;
                    }
                }
                if (!isIntegrated) 
                {
                    _buffParams.Add(aSkillEfcBuff);
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

