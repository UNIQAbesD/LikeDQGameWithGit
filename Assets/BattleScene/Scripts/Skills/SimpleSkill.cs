using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SkillIdentity/SimpleSkill", fileName = "SimpleSkill")]
public class SimpleSkillIdentity : SkillIdentity
{
    [SerializeField] private int minRepeat;
    [SerializeField] private int maxRepeat;
    [SerializeField] private int mp;
    [SerializeField] private SkillElement skillElement;
    [SerializeField] private SkillCategory skillCategory;
    [SerializeField] private BuffResistElement buffElement;
    [SerializeField] private SkillOUnitType efcOUnitType;

    //SkillEfcFuncParam----------------------------------------------
    [SerializeField] private int power;
    [SerializeField] private int accuracy;
    [SerializeField] private int actPriority;
    


    public override SkillSubst skillSubst 
    {
        get 
        {
            SkillSubst_EfcFuncs aSkillSubst =new SkillSubst_EfcFuncs() ;
            aSkillSubst.minRepeat = minRepeat;
            aSkillSubst.maxRepeat = maxRepeat;
            aSkillSubst.mp = mp;
            aSkillSubst.skillElement = skillElement;
            aSkillSubst.skillCategory= skillCategory;
            aSkillSubst.buffElement= buffElement;
            aSkillSubst.efcOUnitType= efcOUnitType;
            aSkillSubst.name = _Name;
            aSkillSubst.skillElement = skillElement ;
            aSkillSubst._skillEfcFuncs = new List<SkillEfcFunc>();

            SkillEfcFunc_deligate tempSkillEfcFunc = new SkillEfcFunc_deligate() ;

            tempSkillEfcFunc.func = (BattleUnit sUnit, BattleUnit oUnit, BattleField bf) =>
            {
                ExSkillEfc exSkillEfc= new ExSkillEfc();
                int atk=0;
                int def=0;
                switch (skillCategory) 
                {
                    case SkillCategory.Phisical:
                        atk = sUnit.status.atk;
                        atk = bf.makeFilteredValue(bf.ATKFilter_WhenCalcSkill(sUnit, sUnit, aSkillSubst, oUnit),atk);
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
                if(skillCategory== SkillCategory.Phisical|skillCategory == SkillCategory.Magical)
                {
                    exSkillEfc.maxHPDamage =22*(power*atk/def)/50+2;
                    exSkillEfc.minHPDamage = (int)(exSkillEfc.maxHPDamage*0.85f);
                }
                
                return exSkillEfc;
            };

            return (SkillSubst)base.skillSubst; 
        }
    }


}
