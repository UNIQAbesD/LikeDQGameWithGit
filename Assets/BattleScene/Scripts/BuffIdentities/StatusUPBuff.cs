using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "ScriptableObject/BuffIdentityIdentity/StatusUP", fileName = "StatusUP_Buff")]
public class Buff_StatusUP : BuffIdentity
{
    [SerializeField] private stat targetStatus;

    public override List<ParamFilter<int>> ATKFilter_WhenCalcSkill(BuffParam buff, BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        List<ParamFilter<int>> ReturnFilter= new List<ParamFilter<int>>();
        ParamFilter<int> Filter;
        if (targetStatus == stat.ATK & whosParamCalc == buff.whosBuff)
        {Filter = new ParamFilter<int>((value, filterList) => statusFilterFunc(value, buff), 5, buff);}
        else { Filter = new ParamFilter<int>(); }
        ReturnFilter.Add(Filter);
        return ReturnFilter;
    }
    public override List<ParamFilter<int>> DEFFilter_WhenCalcSkill(BuffParam buff, BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        List<ParamFilter<int>> ReturnFilter = new List<ParamFilter<int>>();
        ParamFilter<int> Filter;
        if (targetStatus == stat.DEF & whosParamCalc == buff.whosBuff)
        { Filter = new ParamFilter<int>((value, filterList) => statusFilterFunc(value, buff), 5, buff);}
        else { Filter = new ParamFilter<int>(); }
        ReturnFilter.Add(Filter);
        return ReturnFilter;
    }
    public override List<ParamFilter<int>> SATKFilter_WhenCalcSkill(BuffParam buff, BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        List<ParamFilter<int>> ReturnFilter = new List<ParamFilter<int>>();
        ParamFilter<int> Filter;
        if (targetStatus == stat.SATK)
        {Filter = new ParamFilter<int>((value, filterList) => statusFilterFunc(value, buff), 5, buff);}
        else { Filter = new ParamFilter<int>(); }
        ReturnFilter.Add(Filter);
        return ReturnFilter;
    }
    public override List<ParamFilter<int>> SDEFFilter_WhenCalcSkill(BuffParam buff, BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        List<ParamFilter<int>> ReturnFilter = new List<ParamFilter<int>>();
        ParamFilter<int> Filter;
        if (targetStatus == stat.SDEF & whosParamCalc == buff.whosBuff)
        {Filter = new ParamFilter<int>((value, filterList) => statusFilterFunc(value, buff), 5, buff);}
        else{Filter = new ParamFilter<int>();}
        ReturnFilter.Add(Filter);
        return ReturnFilter;
    }
    public override List<ParamFilter<int>> SPDFilter_WhenCalcSkill(BuffParam buff, BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        List<ParamFilter<int>> ReturnFilter = new List<ParamFilter<int>>();
        ParamFilter<int> Filter;
        if (targetStatus == stat.ATK&whosParamCalc==buff.whosBuff)
        {Filter = new ParamFilter<int>((value, filterList) => statusFilterFunc(value, buff), 5, buff);}
        else { Filter = new ParamFilter<int>(); }
        ReturnFilter.Add(Filter);
        return ReturnFilter;
    }
    public virtual List<ParamFilter<(int spd, int actPriority)>> SPDAndPriorityFilter_WhenSort(BuffParam buff, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        List<ParamFilter<(int spd, int actPriority)>> ReturnFilter = new List<ParamFilter<(int spd, int actPriority)>>();
        ParamFilter<(int spd, int actPriority)> Filter;
        if (targetStatus == stat.SPD& sUnit == buff.whosBuff)
        {
            Filter = new ParamFilter<(int spd, int actPriority)>(
                (value, filterList) => (statusFilterFunc(value.spd, buff),value.actPriority), 5, buff);
        }
        else { Filter = new ParamFilter<(int spd, int actPriority)>(); }
        ReturnFilter.Add(Filter);
        return ReturnFilter;
    }

    public int statusFilterFunc(int value,BuffParam buff) 
    {
        if (buff.rank > 0) { return (int)(value * (1 + 0.5 * buff.rank)); }
        else { return (int)(value * 2 / (2 - buff.rank)); }
    }

    enum stat 
    {
        ATK,
        DEF,
        SATK,
        SDEF,
        SPD
    }
}
    