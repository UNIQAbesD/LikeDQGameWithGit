using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityIdentity : ScriptableObject
{
    [SerializeField] protected string _Name;
    public string Name { get { return _Name; } }

    //WhenCalcSkill-----------------------------------------------------------------

    public virtual List<ParamFilter<int>> MaxHPFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit whosAbility, BattleUnit sUnit, SkillSubst useSkillSubst, BattleUnit oUnit,BattleField bf) 
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<int>> ATKFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit whosAbility, BattleUnit sUnit, SkillSubst useSkillSubst, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<int>> DEFFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit whosAbility, BattleUnit sUnit, SkillSubst useSkillSubst, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<int>> SATKFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit whosAbility, BattleUnit sUnit, SkillSubst useSkillSubst, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<int>> SDEFFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit whosAbility, BattleUnit sUnit, SkillSubst useSkillSubst, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<int>> SPDFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit whosAbility, BattleUnit sUnit, SkillSubst useSkillSubst, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<List<ElementType>>> TypeFilter_WhenCalcSkill(BattleUnit whosParamCalc,BattleUnit whosAbility, BattleUnit sUnit, SkillSubst useSkillSubst, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<List<ElementType>>>();
    }

    public virtual List<ParamFilter<List<BattleUnit>>> OUnitsFilter_WhenCalcSkill(BattleUnit whosAbility, BattleUnit sUnit, SkillSubst useSkillSubst, BattleField bf)
    {
        return new List<ParamFilter<List<BattleUnit>>>();
    }

    public virtual List<ParamFilter<List<SkillEfcFunc>>> skillEfcFuncFilter_WhenCalcSkill(BattleUnit whosAbility, BattleUnit sUnit, SkillSubst useSkillSubst, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<List<SkillEfcFunc>>>();
    }


    //WhenMakeSkillEfc--------------------------------------------------------------
    public virtual List<ParamFilter< ExSkillEfc>> ExSkillEfcFilter(BattleUnit whosAbility,BattleUnit sUnit,SkillSubst useSkill,BattleUnit oUnit,BattleField bf) 
    {
        return new List<ParamFilter<ExSkillEfc>>();
    }
    public virtual List<ParamFilter<AcSkillEfc>> AcSkillEfcFilter(BattleUnit whosAbility, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<AcSkillEfc>>();
    }

    //WhenSort----------------------------------------------------------
    public virtual List<ParamFilter<(int spd, int actPriority)>> SPDAndPriorityFilter_WhenSort(BattleUnit whosAbility, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<(int spd, int actPriority)>>();
    }

    //event---------------------------------------------------------------

    public virtual List<ParamFilter<int>> BeforeTurnEventFilter(BattleUnit whosAbility,BattleUnit whosTurn, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }

    //ActCmdされたときに呼び出される
    public virtual List<ParamFilter<int>> AfterTurnEventFilter(BattleUnit whosAbility, BattleUnit whosTurn, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }

    //ActCmdされたときに呼び出される
    public virtual List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>> BeforeActCmdEventFilter(BattleUnit whosAbility,(BattleUnit sUnit,SkillIdentity,BattleUnit oUnit) cmdData, BattleField bf) 
    {
        return new List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>>();
    }

    //ActCmdされたときに呼び出される
    public virtual List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>> AfterActCmdEventFilter(BattleUnit whosAbility, (BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit) cmdData, BattleField bf)
    {
        return new List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>>();
    }
    //Roundが開始されたときに呼び出される
    public virtual List<ParamFilter<int>> BeforeRoundEventFilter(BattleUnit whosAbility, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }

    //Roundが終了されたときに呼び出される
    public virtual List<ParamFilter<int>> AfterRoundEventFilter(BattleUnit whosAbility, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    
    //applySkillEfcされたときに呼び出される
    public virtual List<ParamFilter<int>> AfterOneTimeApplyEventFilter(BattleUnit whosAbility, List<AcSkillEfc> acSkillEfcs,BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
}

public class AbilityParam
{
    public string name;
    public AbilityIdentity identity;
}
