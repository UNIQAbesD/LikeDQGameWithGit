using System.Collections.Generic;
using UnityEngine;

public class Buff_StatusUP : BuffParam
{
    private stat _targetStatus;

    public override List<ParamFilter<int>> ATKFilter_WhenCalcSkill( BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        List<ParamFilter<int>> ReturnFilter = new List<ParamFilter<int>>();
        ParamFilter<int> Filter;
        if (_targetStatus == stat.ATK & whosParamCalc == whosBuff)
        { Filter = new ParamFilter<int>((value, filterList) => statusFilterFunc(value, this), 5, this); }
        else { Filter = new ParamFilter<int>(); }
        ReturnFilter.Add(Filter);

        return ReturnFilter;
    }
    public override List<ParamFilter<int>> DEFFilter_WhenCalcSkill(BattleUnit whosParamCalc,BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        List<ParamFilter<int>> ReturnFilter = new List<ParamFilter<int>>();
        ParamFilter<int> Filter;
        if (_targetStatus == stat.DEF & whosParamCalc == this.whosBuff)
        { Filter = new ParamFilter<int>((value, filterList) => statusFilterFunc(value, this), 5, this); }
        else { Filter = new ParamFilter<int>(); }
        ReturnFilter.Add(Filter);
        return ReturnFilter;
    }
    public override List<ParamFilter<int>> SATKFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        List<ParamFilter<int>> ReturnFilter = new List<ParamFilter<int>>();
        ParamFilter<int> Filter;
        if (_targetStatus == stat.SATK)
        { Filter = new ParamFilter<int>((value, filterList) => statusFilterFunc(value, this), 5, this); }
        else { Filter = new ParamFilter<int>(); }
        ReturnFilter.Add(Filter);
        return ReturnFilter;
    }
    public override List<ParamFilter<int>> SDEFFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        List<ParamFilter<int>> ReturnFilter = new List<ParamFilter<int>>();
        ParamFilter<int> Filter;
        if (_targetStatus == stat.SDEF & whosParamCalc == whosBuff)
        { Filter = new ParamFilter<int>((value, filterList) => statusFilterFunc(value, this), 5, this); }
        else { Filter = new ParamFilter<int>(); }
        ReturnFilter.Add(Filter);
        return ReturnFilter;
    }
    public override List<ParamFilter<int>> SPDFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        List<ParamFilter<int>> ReturnFilter = new List<ParamFilter<int>>();
        ParamFilter<int> Filter;
        if (_targetStatus == stat.ATK & whosParamCalc == whosBuff)
        { Filter = new ParamFilter<int>((value, filterList) => statusFilterFunc(value, this), 5, this); }
        else { Filter = new ParamFilter<int>(); }
        ReturnFilter.Add(Filter);
        return ReturnFilter;
    }
    public virtual List<ParamFilter<(int spd, int actPriority)>> SPDAndPriorityFilter_WhenSort(BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        List<ParamFilter<(int spd, int actPriority)>> ReturnFilter = new List<ParamFilter<(int spd, int actPriority)>>();
        ParamFilter<(int spd, int actPriority)> Filter;
        if (_targetStatus == stat.SPD & sUnit == whosBuff)
        {
            Filter = new ParamFilter<(int spd, int actPriority)>(
                (value, filterList) => (statusFilterFunc(value.spd,this), value.actPriority), 5, this);
        }
        else { Filter = new ParamFilter<(int spd, int actPriority)>(); }
        ReturnFilter.Add(Filter);
        return ReturnFilter;
    }

    public int statusFilterFunc(int value, BuffParam buff)
    {
        if (buff.rank > 0) { return (int)(value * (1 + 0.5 * buff.rank)); }
        else { return (int)(value * 2 / (2 - buff.rank)); }
    }

    public Buff_StatusUP(BattleUnit whosBuff,int targetStatus) : base(whosBuff)
    {
        //identity = buffIdentity;
        if (targetStatus <= (int)stat.SPD)
        {
            _targetStatus = (stat)targetStatus;
        }
        else 
        {
            Debug.Log($"TargetStat‚ª0~4‚ÌŠÔ‚É‚ ‚è‚Ü‚¹‚ñ:{targetStatus}");
            _targetStatus = stat.ATK;
        }
        _maxRank = 6;
        _minRank = -6;
        _isPermanence = false;
        switch (_targetStatus) 
        {
            case stat.ATK:
                _Name ="ATKUP";
                _integrateID = 0;
                break;
            case stat.DEF:
                _Name = "DEFUP";
                _integrateID = 1;
                break;
            case stat.SATK:
                _Name = "SATKUP";
                _integrateID = 2;
                break;
            case stat.SDEF:
                _Name = "SDEFUP";
                _integrateID = 3;
                break;
            case stat.SPD:
                _Name = "SPDUP";
                _integrateID = 4;
                break;
        }

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