using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class SkillSubst 
{
    public string name;
    public int minRepeat;
    public int maxRepeat;
    public int mp;
    public SkillElement skillElement = SkillElement.None;
    public SkillCategory skillCategory;
    public BuffResistElement buffElement = BuffResistElement.None;
    public SkillOUnitType efcOUnitType=SkillOUnitType.OneFoe;
    public SkillSubst nextSkill;
    public virtual List<SkillEfcFunc> skillEfcFuncs { get { return new List<SkillEfcFunc>(); } }
    
    public virtual List<ParamFilter<List<(BattleUnit, SkillIdentity, BattleUnit)>>> cmdFilters ()
    {
        return new List<ParamFilter<List<(BattleUnit, SkillIdentity, BattleUnit)>>>() ;
    }

    public virtual List<ParamFilter<List<BattleUnit>>> OUnitsFilter_WhenCalcSkill(BattleUnit sUnit, BattleField bf) 
    {
        return new List<ParamFilter<List<BattleUnit>>>();
    }


    //WhenMakeSkillEfc--------------------------------------------------------------
    public virtual List<ExSkillEfc> ExSkillEfcFilter(BattleUnit sUnit, BattleUnit oUnit, BattleField bf)
    {
        return new List<ExSkillEfc>();
    }
    public virtual List<AcSkillEfc> AcSkillEfcFilter(BattleUnit sUnit, BattleUnit oUnit, BattleField bf)
    {
        return new List<AcSkillEfc>();
    }



    //WhenSort----------------------------------------------------------
    public virtual List<ParamFilter<(int spd, int actPriority)>> SPDAndPriorityFilter_WhenSort(BattleUnit sUnit, BattleField bf)
    {
        return new List<ParamFilter<(int spd, int actPriority)>>();
    }

    //event---------------------------------------------------------------

    //cmdDataに含まれているskillIdentityのskillsubstのfilterが呼び出される
    public virtual List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>> BeforeActCmdEventFilter((BattleUnit sUnit, SkillIdentity skill,BattleUnit oUnit) cmdData, BattleField bf)
    {
        return new List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>>();
    }

    //cmdDataに含まれているskillIdentityのskillsubstのfilterが呼び出される
    public virtual List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>> AfterActCmdEventFilter((BattleUnit sUnit, SkillIdentity skill,BattleUnit oUnit) cmdData, BattleField bf)
    {
        return new List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>>();
    }

    //cmdDataに含まれているskillIdentityのskillsubstのfilterが呼び出される
    public virtual List<ParamFilter<int>> BeforeRoundEventFilter(BattleUnit sUnit,BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }

    //cmdDataに含まれているskillIdentityのskillsubstのfilterが呼び出される
    public virtual List<ParamFilter<int>> AfterRoundEventFilter(BattleUnit sUnit,BattleField bf)
    {
        return new List<ParamFilter<int>>();
    }
    
    //applyskillEfcされるskillEfcの元となったskillSubstを用いる

}


public class SkillSubst_EfcFuncs : SkillSubst 
{
    public List<SkillEfcFunc> _skillEfcFuncs;
    public override List<SkillEfcFunc> skillEfcFuncs { get { return _skillEfcFuncs; } }

}



public class SkillEfcFunc 
{
    
    protected AdditionalEfcTrigger _addefcTrigger;
    public AdditionalEfcTrigger addefcTrigger { get{ return _addefcTrigger; } }
    //public BattleParamFilte ExSkillFilter;
    //public BattleParamFilter AcSkillFilter;
    
    public virtual ExSkillEfc fun(BattleUnit sUnit,BattleUnit oUnit,BattleField bf) 
    {
        return new ExSkillEfc();
    }

}

public class SkillEfcFunc_deligate : SkillEfcFunc 
{
    public Func<BattleUnit, BattleUnit, BattleField, ExSkillEfc> func;
    public override ExSkillEfc fun(BattleUnit sUnit, BattleUnit oUnit, BattleField bf)
    {
        return func(sUnit,oUnit,bf);
    }
}




public class ParamFilter<FilteredClass>
{
    public bool isActivate;
    private Func<FilteredClass, List<ParamFilter<FilteredClass>>,FilteredClass> _filter;
    private int _priority;
    public int priority { get { return _priority; } }
    private object _source;
    public object source { get { return _source; } }

    //private FilterTag _filterTag;
    //public FilterTag filterTag{get{return _filterTag;}}
    //private bool Exclusive;
    //public bool 
    public FilteredClass fun(FilteredClass value, List<ParamFilter<FilteredClass>> filterList)
    {
        if (_filter == null) { Debug.Log("FilterIsNull");return value; }
        if (isActivate){return _filter(value, filterList);}
        else{return value;}
    }

    public ParamFilter(Func<FilteredClass, List<ParamFilter<FilteredClass>>, FilteredClass> filter,
        int priority,object source)
    {
        _filter = filter;
        isActivate = true;
        _priority = priority;
        _source = source;
    }
    public ParamFilter()
    {
        _filter = null;
        isActivate = false;
        _priority = 0;
        _source = null;
    }
}






public class ExSkillEfc 
{
    public BattleUnit sUnit;
    public SkillSubst skillSubst;
    public BattleUnit oUnit;
    public int minHPDamage;
    public int maxHPDamage;
    public int minMPDamage;
    public int maxMPDamage;
    public float accuracy;
    public List<BuffParam> buffParams;
    public List<float> buffPossiblity;


    public AcSkillEfc makeAcSkillEfc() 
    {
        AcSkillEfc acSkillEfc = new AcSkillEfc();

        Debug.Assert(sUnit != null, $"ExSkillEfcでsUnit==null");
        Debug.Assert(oUnit != null, $"ExSkillEfcでoUnit==null");
        acSkillEfc.sUnit = sUnit;
        acSkillEfc.oUnit = oUnit;


        if (accuracy * 100 > UnityEngine.Random.Range(0, 100))
        {
            acSkillEfc.isHit = true;
        }
        else 
        {
            acSkillEfc.isHit = false;
            return acSkillEfc;
        }

        int tempValue = minHPDamage;
        if (minHPDamage > maxHPDamage) 
        {
            minHPDamage = maxHPDamage;
            maxHPDamage = tempValue;
        }
        acSkillEfc.hpDamage=UnityEngine.Random.Range(minHPDamage,maxHPDamage+1);
        tempValue = minMPDamage;
        if (minMPDamage > maxMPDamage)
        {
            minMPDamage = maxMPDamage;
            maxMPDamage = tempValue;
        }
        acSkillEfc.mpDamage = UnityEngine.Random.Range(minMPDamage, maxMPDamage + 1);

        for (int i=0;i<buffParams.Count;i++) 
        {
            if (buffParams[i].rank != 0 & buffParams[i].lastTurn != 0) 
            {
                if (buffPossiblity[i] * 100 > UnityEngine.Random.Range(0, 100)) 
                {
                    acSkillEfc.buffParam.Add(buffParams[i]);
                }
            }
        }

        return acSkillEfc;
    }





    public ExSkillEfc() 
    {
        minHPDamage = 0;
        maxHPDamage = 0;
        minMPDamage = 0;
        maxMPDamage = 0;
        accuracy = 0;
        buffParams = new List<BuffParam>();
        buffPossiblity = new List<float>();
    }
}





public class AcSkillEfc
{
    public BattleUnit sUnit;
    public BattleUnit oUnit;
    public int hpDamage;
    public int mpDamage;
    public bool isHit;
    public List<BuffParam> buffParam;

    public AcSkillEfc() 
    {
        hpDamage = 0;
        mpDamage = 0;
        isHit = false;
        buffParam = new List<BuffParam>();
    }
}





//enum---------------------------------------

public enum SkillElement
{
    None,
    Fire,
    Ice,
    Wind,
    EnumEnd
}

public enum BuffResistElement 
{
    None,
    Mental,
    Imunity,
    robustness,
    EnumEnd
}

public enum SkillCategory 
{
    None,
    Phisical,
    Magical,
    Status,

}

public enum AdditionalEfcTrigger
{
    SameUnit,
    DamagedUnit,
    effctedUnit
}
public enum SkillOUnitType
{
    User,
    OneAlly,
    OneFoe,
    OneUnit,
    AllAlly,
    AllFoe,
    AllUnit,
    RandomAlly,
    RandomFoe,
    RandomUnit,

}



