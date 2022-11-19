using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "ScriptableObject/BattleAbility/UnAware", fileName = "Unaware")]
public class Unaware : AbilityIdentity//てんねん
{
    [SerializeField]private int _filterPriority=-100;
    public override List<ParamFilter<int>> ATKFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit whosAbility, BattleUnit sUnit, SkillSubst useSkillSubst, BattleUnit oUnit, BattleField bf)
    {   
        return filterGen(whosParamCalc,whosAbility,sUnit,useSkillSubst,oUnit,bf);
    }
    public override List<ParamFilter<int>> DEFFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit whosAbility, BattleUnit sUnit, SkillSubst useSkillSubst, BattleUnit oUnit, BattleField bf)
    {
        return filterGen(whosParamCalc, whosAbility, sUnit, useSkillSubst, oUnit, bf);
    }
    public override List<ParamFilter<int>> SATKFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit whosAbility, BattleUnit sUnit, SkillSubst useSkillSubst, BattleUnit oUnit, BattleField bf)
    {
        return filterGen(whosParamCalc, whosAbility, sUnit, useSkillSubst, oUnit, bf);
    }
    public override List<ParamFilter<int>> SDEFFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit whosAbility, BattleUnit sUnit, SkillSubst useSkillSubst, BattleUnit oUnit, BattleField bf)
    {
        return filterGen(whosParamCalc, whosAbility, sUnit, useSkillSubst, oUnit, bf);
    }
    public override List<ParamFilter<int>> SPDFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit whosAbility, BattleUnit sUnit, SkillSubst useSkillSubst, BattleUnit oUnit, BattleField bf)
    {
        return filterGen(whosParamCalc, whosAbility, sUnit, useSkillSubst, oUnit, bf);
    }
    private List<ParamFilter<int>> filterGen(BattleUnit whosParamCalc, BattleUnit whosAbility, BattleUnit sUnit, SkillSubst useSkillSubst, BattleUnit oUnit, BattleField bf)
    {
        ParamFilter<int> filter = new ParamFilter<int>(
            (int value, List<ParamFilter<int>> filterList) =>
            {
                if (whosAbility == oUnit & whosParamCalc != whosAbility)
                {
                    foreach (var aFilter in filterList)
                    {
                        if (aFilter.source is Buff_StatusUP)
                        {
                            aFilter.isActivate = false;
                        }
                    }
                }
                return value;
            }, _filterPriority, this);
        List < ParamFilter<int> > ReturnValue=new List<ParamFilter<int>>();
        ReturnValue.Add(filter);
        return ReturnValue;
    }
}
