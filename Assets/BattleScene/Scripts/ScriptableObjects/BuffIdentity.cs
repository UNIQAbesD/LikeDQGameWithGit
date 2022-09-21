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

}

public class BuffParam
{
    public BattleUnit whosBuff;
    public int lastTurn;
    public int rank;
    public BuffIdentity identity;

    public virtual void whenApplySameIDBuff(BuffParam skillEfcParam) { }


    //WhenCalcSkill-----------------------------------------------------------------
    public virtual List<ParamFilter<int>> MaxHPFilter_WhenCalcSkill(BattleUnit whosParamCalc,BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<int>> ATKFilter_WhenCalcSkill(BattleUnit whosParamCalc,BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit,  BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<int>> DEFFilter_WhenCalcSkill(BattleUnit whosParamCalc,BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<int>> SATKFilter_WhenCalcSkill(BattleUnit whosParamCalc,BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<int>> SDEFFilter_WhenCalcSkill(BattleUnit whosParamCalc,BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit,  BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<int>> SPDFilter_WhenCalcSkill(BattleUnit whosParamCalc,BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit,  BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public virtual List<ParamFilter<List<ElementType>>> TypeFilter_WhenCalcSkill(BattleUnit whosParamCalc,BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
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
    //whosBuff��turn�J�n���ɌĂяo�����
    public virtual List<ParamFilter<int>> BeforeTurnEventFilter(BattleUnit whosTurn,BattleField bf) 
    {
        return new List<ParamFilter<int>>();
    }
    //whosBuff��turn�I�����ɌĂяo�����
    public virtual List<ParamFilter<int>> AfterTurnEventFilter(BattleUnit whosTurn,BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    //whosBuff��sUnit��ActCmd���ɌĂяo�����
    public virtual List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>> BeforeActCmdEventFilter((BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit) cmdData, BattleField bf)
    {
        return new List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>>();
    }

    //whosBuff��sUnit��ActCmd���ɌĂяo�����
    public virtual List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>> AfterActCmdEventFilter((BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit) cmdData, BattleField bf)
    {
        return new List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>>();
    }

    //Round�J�n���ɏ�ɂ��郆�j�b�g�̂ǂꂩ��whosbuff�Ȃ�ΌĂяo�����
    public virtual List<ParamFilter<int>> BeforeRoundEventFilter( BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    //Round�J�n���ɏ�ɂ��郆�j�b�g�̂ǂꂩ��whosbuff�Ȃ�ΌĂяo�����
    public virtual List<ParamFilter<int>> AfterRoundEventFilter( BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    //whosBuff��oUnit�Ƃ���applySkillEfc���ꂽ�Ƃ��ɌĂяo�����
    public virtual List<ParamFilter<int>> AfterOneTimeApplyEventFilter(List<AcSkillEfc> acSkillEfcs,BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    public BuffParam(BattleUnit whosBuff,BuffIdentity buffIdentity)
    {
        identity = buffIdentity;
        lastTurn = 0;
        rank = 0;
    }
}