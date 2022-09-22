using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//const int ATKUP_IntegID = 0;

public class BuffIdentity : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] protected string _Name;
    public string Name { get { return _Name; } }

    [SerializeField] protected int _maxRank=1;
    public int maxRank { get { return _maxRank; } }

    [SerializeField] protected int _minRank=0;
    public int minRank { get { return _minRank; } }

    [SerializeField] protected bool _isPermanence=false;
    public bool isPermanence { get { return _isPermanence; } }

    [SerializeField] protected int _integrateID=-1;
    public int integrateID { get { return _integrateID; } }

    [SerializeField] protected BuffParam _buffParam;
    public bool BuffParam { get { return _isPermanence; } }

}

public class BuffParam
{
    protected string _Name="None";
    public string Name { get { return _Name; } }

    protected int _maxRank=1;
    public int maxRank { get { return _maxRank; } }

    protected int _minRank=0;
    public int minRank { get { return _minRank; } }

    protected bool _isPermanence=false;
    public bool isPermanence { get { return _isPermanence; } }
    
    protected int _integrateID=-1;
    public int integrateID { get { return _integrateID; } }

    public BattleUnit whosBuff;
    public int lastTurn;
    public int rank;
    //public BuffIdentity identity;

    public virtual BuffParam whenApplySameIDBuff(BuffParam skillEfcParam) { return this; }


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
    public BuffParam(BattleUnit whosBuff,string Name,int maxRank,int minRank,bool isPermanence,int integrateID)
    {
        //identity = buffIdentity;
        this.whosBuff = whosBuff;
        this._Name = Name;
        this._maxRank = maxRank;
        this._minRank = minRank;
        this._isPermanence = isPermanence;
        this._integrateID = integrateID;
        lastTurn = 0;
        rank = 0;
    }
    public BuffParam(BattleUnit whosBuff): this(whosBuff, "None", 1, 0, false, -1){}
}