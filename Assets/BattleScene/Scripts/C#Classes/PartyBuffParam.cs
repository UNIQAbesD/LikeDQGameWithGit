using System.Collections.Generic;

public class PartyBuffParam
{
    protected string _Name = "None";
    public string Name { get { return _Name; } }

    protected int _maxRank = 1;
    public int maxRank { get { return _maxRank; } }

    protected int _minRank = 0;
    public int minRank { get { return _minRank; } }

    protected bool _isPermanence = false;
    public bool isPermanence { get { return _isPermanence; } }

    protected CostBuffTurnTiming _costBuffTurnTiming;
    public CostBuffTurnTiming costBuffTurnTiming { get { return _costBuffTurnTiming; } }

    protected int _integrateID = -1;
    public int integrateID { get { return _integrateID; } }

    public BattleUnit whosBuff;
    public int lastTurn;
    public int rank;


    public virtual PartyBuffParam whenApplySameIDBuff(BuffParam skillEfcParam) { return this; }


    //WhenCalcSkill-----------------------------------------------------------------
    public virtual List<ParamFilter<int>> MaxHPFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<int>> ATKFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<int>> DEFFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<int>> SATKFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<int>> SDEFFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<int>> SPDFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<List<ElementType>>> TypeFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<List<ElementType>>>();
    }

    public virtual List<ParamFilter<List<BattleUnit>>> OUnitsFilter_WhenCalcSkill(BattleUnit sUnit, SkillSubst useSkillSubst, BattleField bf)
    {
        return new List<ParamFilter<List<BattleUnit>>>();
    }

    public virtual List<ParamFilter<List<SkillEfcFunc>>> skillEfcFuncFilter_WhenCalcSkill(BattleUnit sUnit, SkillSubst useSkillSubst, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<List<SkillEfcFunc>>>();
    }

    //WhenMakeSkillEfc--------------------------------------------------------------
    public virtual List<ParamFilter<ExSkillEfc>> ExSkillEfcFilter(BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<ExSkillEfc>>();
    }
    public virtual List<ParamFilter<AcSkillEfc>> AcSkillEfcFilter(BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<AcSkillEfc>>();
    }

    //WhenSort----------------------------------------------------------
    public virtual List<ParamFilter<(int spd, int actPriority)>> SPDAndPriorityFilter_WhenSort(BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<(int spd, int actPriority)>>();
    }

    //event---------------------------------------------------------------
    //whosBuffのturn開始時に呼び出される
    public virtual List<ParamFilter<int>> BeforeTurnEventFilter(BattleUnit whosTurn, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    //whosBuffのturn終了時に呼び出される
    public virtual List<ParamFilter<int>> AfterTurnEventFilter(BattleUnit whosTurn, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    //whosBuffがsUnitのActCmd時に呼び出される
    public virtual List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>> BeforeActCmdEventFilter((BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit) cmdData, BattleField bf)
    {
        return new List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>>();
    }

    //whosBuffがsUnitのActCmd時に呼び出される
    public virtual List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>> AfterActCmdEventFilter((BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit) cmdData, BattleField bf)
    {
        return new List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>>();
    }

    //Round開始時に場にいるユニットのどれかがwhosbuffならば呼び出される
    public virtual List<ParamFilter<int>> BeforeRoundEventFilter(BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    //Round開始時に場にいるユニットのどれかがwhosbuffならば呼び出される
    public virtual List<ParamFilter<int>> AfterRoundEventFilter(BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    //whosBuffをoUnitとしてapplySkillEfcされたときに呼び出される
    public virtual List<ParamFilter<int>> AfterOneTimeApplyEventFilter(List<AcSkillEfc> acSkillEfcs, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public PartyBuffParam(string Name, int maxRank, int minRank, bool isPermanence, int integrateID, CostBuffTurnTiming costBuffTurnTiming)
    {
        //identity = buffIdentity;
        this._Name = Name;
        this._maxRank = maxRank;
        this._minRank = minRank;
        this._isPermanence = isPermanence;
        this._integrateID = integrateID;
        this._costBuffTurnTiming = costBuffTurnTiming;
        lastTurn = 0;
        rank = 0;
    }
    public PartyBuffParam() : this("None", 1, 0, false, -1, CostBuffTurnTiming.RoundEnd) { }
}