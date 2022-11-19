using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/SkillIdentity/BasicSkillIdentity", fileName = "BasicSkillIdentity")]
public class BasicSkillIdentity : SkillIdentity
{
    [SerializeField] private int minRepeat = 1;
    [SerializeField] private int maxRepeat = 1;
    [SerializeField] private int mp;
    [SerializeField] private ElementType skillElement;
    [SerializeField] private SkillCategory skillCategory;
    [SerializeField] private BuffResistElement buffElement;
    [SerializeField] private SkillOUnitType efcOUnitType;
    [SerializeField] private List<BasicSkillEfcFuncData> skillEfcFuncDatas;
    //cache-------------------------------


    public override SkillSubst skillSubst
    {
        get
        {
            SkillSubst_EfcFuncs aSkillSubst = new SkillSubst_EfcFuncs();
            aSkillSubst.minRepeat = minRepeat;
            aSkillSubst.maxRepeat = maxRepeat;
            aSkillSubst.mp = mp;
            aSkillSubst.skillElement = skillElement;
            aSkillSubst.skillCategory = skillCategory;
            aSkillSubst.buffElement = buffElement;
            aSkillSubst.efcOUnitType = efcOUnitType;
            aSkillSubst.name = _Name;
            aSkillSubst.skillElement = skillElement;
            aSkillSubst._skillEfcFuncs = new List<SkillEfcFunc>();

            foreach (var aSkillEfcFuncData in skillEfcFuncDatas) 
            {
                aSkillSubst._skillEfcFuncs.Add(aSkillEfcFuncData.GenFunc(aSkillSubst));
            }
            return skillSubst;
        }



    }

    [System.Serializable]
    class BasicSkillEfcFuncData
    {
        public FuncType funcType;
        public int accuracy;
        public AdditionalEfcTrigger addEfcTrigger;
        //Attack------------------------------------
        [EnableByOtherMemVal("funcType",1)]
        public int power;
        [EnableByOtherMemVal("funcType", 1)]
        public bool isSpecial;
        //Buff---------------------------------------
        [EnableByOtherMemVal("funcType", 2)]
        public BasicBuffType buffType;
        [EnableByOtherMemVal("funcType", 2)]
        public int buffTurn;
        [EnableByOtherMemVal("funcType", 2)]
        public int buffRank;
        [EnableByOtherMemVal("funcType", 2)]
        public float possibility;
        //Heal--------------------------------------
        [EnableByOtherMemVal("funcType", 3)]
        public float HealPower;
        [EnableByOtherMemVal("funcType", 3)]
        public BasicHealType basicHealType;

        public SkillEfcFunc GenFunc(SkillSubst skillSubst) 
        {
            SkillEfcFunc_deligate tempSkillEfcFunc;
            if (funcType == FuncType.Attack)
            {
                if (!isSpecial)//物理攻撃 
                {
                    tempSkillEfcFunc = new SkillEfcFunc_deligate(
                    (BattleUnit sUnit, BattleUnit oUnit, BattleField bf) =>
                    {
                        ExSkillEfc exSkillEfc = new ExSkillEfc();
                        exSkillEfc.accuracy = accuracy;
                        int atk = 0;
                        int def = 0;
                        atk = sUnit.status.atk;
                        atk = bf.makeFilteredValue(bf.ATKFilter_WhenCalcSkill(sUnit, sUnit, skillSubst, oUnit), atk);
                        def = sUnit.status.def;
                        def = bf.makeFilteredValue(bf.DEFFilter_WhenCalcSkill(sUnit, sUnit, skillSubst, oUnit), def);
                        exSkillEfc.maxHPDamage = 22 * (power * atk / def) / 50 + 2;
                        exSkillEfc.minHPDamage = (int)(exSkillEfc.maxHPDamage * 0.85f);
                        return exSkillEfc;
                    }, addEfcTrigger);
                }
                else//特殊攻撃
                {
                    tempSkillEfcFunc = new SkillEfcFunc_deligate(
                    (BattleUnit sUnit, BattleUnit oUnit, BattleField bf) =>
                    {
                        ExSkillEfc exSkillEfc = new ExSkillEfc();
                        exSkillEfc.accuracy = accuracy;
                        int atk = 0;
                        int def = 0;
                        atk = sUnit.status.satk;
                        atk = bf.makeFilteredValue(bf.SATKFilter_WhenCalcSkill(sUnit, sUnit, skillSubst, oUnit), atk);
                        def = sUnit.status.sdef;
                        def = bf.makeFilteredValue(bf.SDEFFilter_WhenCalcSkill(sUnit, sUnit, skillSubst, oUnit), def);
                        exSkillEfc.maxHPDamage = 22 * (power * atk / def) / 50 + 2;
                        exSkillEfc.minHPDamage = (int)(exSkillEfc.maxHPDamage * 0.85f);
                        return exSkillEfc;
                    }
                    , addEfcTrigger);
                }
            }
            else if (funcType == FuncType.Buff)
            {
                BuffParam buffParam;
                switch (buffType)
                {
                    case BasicBuffType.ATKUP:
                        buffParam = new Buff_StatusUP(0);
                        break;
                    case BasicBuffType.DEFUP:
                        buffParam = new Buff_StatusUP(1);
                        break;
                    case BasicBuffType.SATKUP:
                        buffParam = new Buff_StatusUP(2);
                        break;
                    case BasicBuffType.SDEFUP:
                        buffParam = new Buff_StatusUP(3);
                        break;
                    case BasicBuffType.SPDUP:
                        buffParam = new Buff_StatusUP(4);
                        break;
                    default:
                        Debug.Log("buffTypeの指定が想定外");
                        buffParam = new Buff_StatusUP(0);
                        break;
                }
                buffParam.rank = buffRank;
                buffParam.lastTurn = buffTurn;
                tempSkillEfcFunc = new SkillEfcFunc_deligate(
                    (BattleUnit sUnit, BattleUnit oUnit, BattleField bf) =>
                    {
                        ExSkillEfc exSkillEfc = new ExSkillEfc();
                        exSkillEfc.accuracy = accuracy;
                        exSkillEfc.AddBuff(buffParam, possibility);
                        return exSkillEfc;
                    }, addEfcTrigger);
            }
            else //funcType == FuncType.Heal
            {
                
                tempSkillEfcFunc = new SkillEfcFunc_deligate(
                    (BattleUnit sUnit, BattleUnit oUnit, BattleField bf) =>
                    {
                        ExSkillEfc exSkillEfc = new ExSkillEfc();
                        exSkillEfc.accuracy = accuracy;
                        if (basicHealType == BasicHealType.Const)
                        {
                            exSkillEfc.maxHPDamage = (int)HealPower;
                            exSkillEfc.minHPDamage = exSkillEfc.maxHPDamage;
                        }
                        else //basicHealType == BasicHealType.Percentage
                        {
                            exSkillEfc.maxHPDamage = (int)HealPower * oUnit.status.maxHP;
                            exSkillEfc.minHPDamage = exSkillEfc.maxHPDamage;

                        }
                        return exSkillEfc;
                    }, addEfcTrigger);
            }
            
            return tempSkillEfcFunc;
        }
    }

    enum FuncType
    {
        None,
        Attack,
        Buff,
        Heal
    }
    enum BasicBuffType
    {
        None,
        ATKUP,
        DEFUP,
        SATKUP,
        SDEFUP,
        SPDUP
    }

    enum BasicHealType 
    {
        None,
        Const,
        Percentage
    }

    enum AdditionalBuff
    {
        None,
        ATKUP,
        DEFUP,
        SATKUP,
        SDEFUP,
        SPDUP
    }
}
