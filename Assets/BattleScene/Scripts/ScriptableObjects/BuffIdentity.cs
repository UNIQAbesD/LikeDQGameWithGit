using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffIdentity : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] protected string _Name;
    public string Name { get { return _Name; } }

    [SerializeField] protected int _maxRank;
    public int maxRank { get { return _maxRank; } }

    [SerializeField] protected int _minRank;
    public int minRank { get { return _minRank; } }

    [SerializeField] protected bool _isPermanence;
    public bool isPermanence { get { return _isPermanence; } }

    [SerializeField] protected int _integrateID;
    public int integrateID { get { return _integrateID; } }

    [SerializeField] protected BuffParam _buffParam;
    public bool BuffParam { get { return _isPermanence; } }

    public virtual void whenApplySameIDBuff(BuffParam unitBuff,BuffParam skillEfcParam) { }

    //WhenCalcSkill-----------------------------------------------------------------
    public virtual List<ParamFilter<int>> MaxHPFilter_WhenCalcSkill(BuffParam buff,BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<int>> ATKFilter_WhenCalcSkill(BuffParam buff,BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<int>> DEFFilter_WhenCalcSkill(BuffParam buff, BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<int>> SATKFilter_WhenCalcSkill(BuffParam buff, BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<int>> SDEFFilter_WhenCalcSkill(BuffParam buff, BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<int>> SPDFilter_WhenCalcSkill(BuffParam buff, BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<List<ElementType>>> TypeFilter_WhenCalcSkill(BuffParam buff, BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<List<ElementType>>>();
    }

    public virtual List<ParamFilter<List<BattleUnit>>> OUnitsFilter_WhenCalcSkill(BuffParam buff, BattleUnit sUnit, SkillSubst useSkillSubst, BattleField bf)
    {
        return new List<ParamFilter<List<BattleUnit>>>();
    }

    public virtual List<ParamFilter<List<SkillEfcFunc>>> skillEfcFuncFilter_WhenCalcSkill(BuffParam buff, BattleUnit sUnit, SkillSubst useSkillSubst, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<List<SkillEfcFunc>>>();
    }

    //WhenMakeSkillEfc--------------------------------------------------------------
    public virtual List<ParamFilter<ExSkillEfc>> ExSkillEfcFilter(BuffParam buff, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<ExSkillEfc>>();
    }
    public virtual List<ParamFilter<AcSkillEfc>> AcSkillEfcFilter(BuffParam buff, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<AcSkillEfc>>();
    }

    //WhenSort----------------------------------------------------------
    public virtual List<ParamFilter<(int spd, int actPriority)>> SPDAndPriorityFilter_WhenSort(BuffParam buff, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<(int spd, int actPriority)>>();
    }

    //event---------------------------------------------------------------
    //whosBuffのturn開始時に呼び出される
    public virtual List<ParamFilter<int>> BeforeTurnEventFilter(BuffParam buff, BattleUnit whosTurn, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    //whosBuffのturn終了時に呼び出される
    public virtual List<ParamFilter<int>> AfterTurnEventFilter(BuffParam buff, BattleUnit whosTurn, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    //whosBuffがsUnitのActCmd時に呼び出される
    public virtual List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>> BeforeActCmdEventFilter(BuffParam buff, (BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit) cmdData, BattleField bf)
    {
        return new List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>>();
    }

    //whosBuffがsUnitのActCmd時に呼び出される
    public virtual List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>> AfterActCmdEventFilter(BuffParam buff, (BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit) cmdData, BattleField bf)
    {
        return new List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>>();
    }

    //Round開始時に場にいるユニットのどれかがwhosbuffならば呼び出される
    public virtual List<ParamFilter<int>> BeforeRoundEventFilter(BuffParam buff, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    //Round開始時に場にいるユニットのどれかがwhosbuffならば呼び出される
    public virtual List<ParamFilter<int>> AfterRoundEventFilter(BuffParam buff, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    //whosBuffをoUnitとしてapplySkillEfcされたときに呼び出される
    public virtual List<ParamFilter<int>> AfterOneTimeApplyEventFilter(BuffParam buff, List<AcSkillEfc> acSkillEfcs, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
}

public class BuffParam
{
    public BattleUnit whosBuff;
    public int lastTurn;
    public int rank;
    public BuffIdentity identity;

    public BuffParam(BattleUnit whosBuff,BuffIdentity buffIdentity)
    {
        identity = buffIdentity;
        lastTurn = 0;
        rank = 0;
    }
}