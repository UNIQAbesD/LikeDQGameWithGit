using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SkillIdentity/AddDebuffSkillIdentity", fileName = "AddDebuffSkillIdentity")]
public class AddDebuffSkillIdentity : SkillIdentity
{
    [SerializeField] private int minRepeat=1;
    [SerializeField] private int maxRepeat=1;
    [SerializeField] private int mp;
    [SerializeField] private ElementType skillElement;
    [SerializeField] private SkillCategory skillCategory;
    [SerializeField] private BuffResistElement buffElement;
    [SerializeField] private SkillOUnitType efcOUnitType;

    //SkillEfcFuncParam----------------------------------------------
    [SerializeField] private int power;
    [SerializeField] private float accuracy;
    [SerializeField] private int actPriority;
    [SerializeField] private AdditionalBuff additionalBuff;
    [SerializeField] private float addBuffPossibility;
    [SerializeField] private int addBuffRank;
    [SerializeField] private int addBuffTurn;

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

            SkillEfcFunc_deligate tempSkillEfcFunc = new SkillEfcFunc_deligate(
                (BattleUnit sUnit, BattleUnit oUnit, BattleField bf) =>
                {
                    ExSkillEfc exSkillEfc = new ExSkillEfc();
                    int atk = 0;
                    int def = 0;
                    switch (skillCategory)
                    {
                        case SkillCategory.Phisical:
                            atk = sUnit.status.atk;
                            atk = bf.makeFilteredValue(bf.ATKFilter_WhenCalcSkill(sUnit, sUnit, aSkillSubst, oUnit), atk);
                            def = sUnit.status.def;
                            def = bf.makeFilteredValue(bf.DEFFilter_WhenCalcSkill(sUnit, sUnit, aSkillSubst, oUnit), def);
                            break;
                        case SkillCategory.Magical:
                            atk = sUnit.status.satk;
                            atk = bf.makeFilteredValue(bf.SATKFilter_WhenCalcSkill(sUnit, sUnit, aSkillSubst, oUnit), atk);
                            def = sUnit.status.sdef;
                            def = bf.makeFilteredValue(bf.SDEFFilter_WhenCalcSkill(sUnit, sUnit, aSkillSubst, oUnit), def);
                            break;

                    }
                    if (skillCategory == SkillCategory.Phisical | skillCategory == SkillCategory.Magical)
                    {
                        exSkillEfc.maxHPDamage = 22 * (power * atk / def) / 50 + 2;
                        exSkillEfc.minHPDamage = (int)(exSkillEfc.maxHPDamage * 0.85f);
                    }

                    return exSkillEfc;
                }, AdditionalEfcTrigger.DamagedUnit);

            aSkillSubst._skillEfcFuncs.Add(tempSkillEfcFunc);

            if (additionalBuff==AdditionalBuff.None) { return skillSubst; }
            tempSkillEfcFunc = new SkillEfcFunc_deligate(
                            (BattleUnit sUnit, BattleUnit oUnit, BattleField bf) =>
                            {
                                BuffParam buffParam;
                                ExSkillEfc exSkillEfc = new ExSkillEfc();
                                switch (additionalBuff)
                                {
                                    case AdditionalBuff.ATKUP:
                                        buffParam = new Buff_StatusUP(0);
                                        break;
                                    case AdditionalBuff.DEFUP:
                                        buffParam = new Buff_StatusUP( 1);
                                        break;
                                    case AdditionalBuff.SATKUP:
                                        buffParam = new Buff_StatusUP( 2);
                                        break;
                                    case AdditionalBuff.SDEFUP:
                                        buffParam = new Buff_StatusUP( 3);
                                        break;
                                    case AdditionalBuff.SPDUP:
                                        buffParam = new Buff_StatusUP( 4);
                                        break;
                                    default:
                                        Debug.Log("AdditionalBuff‚ÌŽw’è‚ª‘z’èŠO");
                                        buffParam = new Buff_StatusUP( 0);
                                        break;
                                }
                                buffParam.rank = addBuffRank;
                                buffParam.lastTurn = addBuffTurn;

                                exSkillEfc.AddBuff(buffParam, addBuffPossibility);
                                return exSkillEfc;
                            },AdditionalEfcTrigger.effctedUnit);

            aSkillSubst._skillEfcFuncs.Add(tempSkillEfcFunc);
            return skillSubst;
        }
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
