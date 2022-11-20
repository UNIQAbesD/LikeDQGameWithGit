using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;



public delegate void AnAction();


//MP isDead Buffの継続ターンなどの概念が盛り込まれてない
public class BattleField
{
    private List<BattleUnit> _party1;
    public List<BattleUnit> party1 { get { return _party1; } }

    private List<BattleUnit> _party2;
    public List<BattleUnit> party2 { get { return _party2; } }

    List<PartyBuffParam> _allPartyBuffs;
    List<PartyBuffParam> _party1Buffs;
    List<PartyBuffParam> _party2Buffs;

    public int RoundCount { private set; get; }
    public int turnCount { private set; get; }
    List<(BattleUnit, SkillIdentity, BattleUnit)> cmdDatas;
/*    AnAction roundStartEvent;
    AnAction roundEndEvent;
    AnAction turnStartEvent;
    AnAction turnEndEvent;
    AnAction actStartEvent;
    AnAction actEndEvent;
    AnAction oneTimeApplyEvent;*/
    public SkillIdentity doNothingSkill;

    public void setCmd(BattleUnit sUnit, SkillIdentity skill,BattleUnit oUnit) //Arg: unit skill
    {

        cmdDatas.Add((sUnit, skill, oUnit));
    }



    public void passOneRound() 
    {
        while (turnCount >= cmdDatas.Count) 
        {
            passOneRoundGradually();
        }

    }


    public void passOneRoundGradually()
    {

        if (turnCount < 0 | turnCount > cmdDatas.Count) { return; }

        if (turnCount == 0)
        {
            makeCmdData();

            List<ParamFilter<int>> roundStartEventFilters = BeforeRoundEventFilter();
            
            foreach (ParamFilter<int> aEvent in roundStartEventFilters)
            {
                aEvent.fun(0, roundStartEventFilters);
            }

        }

        actCmd(cmdDatas[turnCount]);
        turnCount++;

        if (turnCount == cmdDatas.Count) 
        {
            List<ParamFilter<int>> roundEndEventFilters = AfterRoundEventFilter();
            foreach (ParamFilter<int> aEvent in roundEndEventFilters)
            {
                aEvent.fun(0, roundEndEventFilters);
            }
            cmdDatas.Clear();
        }
    }

    private void makeCmdData() 
    {
        List<(int spd, int priority, int index)> spdAndPrioritys = new List<(int spd, int priority, int index)>();//どのcmdDataに対応するSPDとPriorityなのか判断するため、indexも含める
        List<ParamFilter<(int spd, int priority)>> spdAndPriorityFilter;
        (int spd, int priority) tempSPDAndPrioriy;
        for (int i = 0; i < cmdDatas.Count; i++)
        {
            spdAndPriorityFilter = SPDAndPriorityFilter_WhenSort(cmdDatas[i].Item1, cmdDatas[i].Item2.skillSubst, cmdDatas[i].Item3);
            tempSPDAndPrioriy = makeFilteredValue(spdAndPriorityFilter, (cmdDatas[i].Item1.status.spd, 0));
            spdAndPrioritys.Add((tempSPDAndPrioriy.spd, tempSPDAndPrioriy.priority, i));
        }

        spdAndPrioritys = spdAndPrioritys.OrderByDescending(x => x.spd + x.priority * 200000).ToList();

        List<(BattleUnit, SkillIdentity, BattleUnit)> tempCmdData = new List<(BattleUnit, SkillIdentity, BattleUnit)>();
        for (int i = 0; i < spdAndPrioritys.Count; i++)
        {
            tempCmdData.Add(cmdDatas[spdAndPrioritys[i].index]);
        }
        cmdDatas = tempCmdData;
    }



    private void actCmd((BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)cmdData) //Arg :unit SkillIdentity
    {
        List<SkillEfcFunc> tempSkillEfcFuncs;


        SkillSubst skillSubst = cmdData.skill.skillSubst;

        List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>> beforeCmdActEventFilters = BeforeActCmdEventFilter(cmdData);
        foreach (ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)> aEvent in beforeCmdActEventFilters)
        {
            aEvent.fun(cmdData, beforeCmdActEventFilters);
        }

        simpleActCmd(cmdData);

        List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>> afterCmdActEventFilters = AfterActCmdEventFilter(cmdData);
        foreach (var aEvent in afterCmdActEventFilters)
        {
            aEvent.fun(cmdData,afterCmdActEventFilters);
        }
    }

    public void simpleActCmd((BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit) cmdData)//EventFilterが動かない
    {
        List<SkillEfcFunc> tempSkillEfcFuncs;
        SkillSubst skillSubst = cmdData.skill.skillSubst;

        while (skillSubst != null)
        {
            int repeatTime = (skillSubst.minRepeat <= skillSubst.maxRepeat) ?
                UnityEngine.Random.Range(skillSubst.minRepeat, skillSubst.maxRepeat + 1)
                : UnityEngine.Random.Range(skillSubst.maxRepeat, skillSubst.minRepeat + 1);

            for (int i = 0; i < repeatTime; i++)
            {
                List<BattleUnit> oUnits = makeOUnitsList(cmdData.sUnit, skillSubst.efcOUnitType, cmdData.oUnit);
                tempSkillEfcFuncs = skillSubst.skillEfcFuncs;
                List<ParamFilter<List<SkillEfcFunc>>> skillEfcFuncFilter = skillEfcFuncFilter_WhenCalcSkill(cmdData.sUnit, skillSubst, cmdData.oUnit);
                tempSkillEfcFuncs = makeFilteredValue(skillEfcFuncFilter, tempSkillEfcFuncs);
                for (int v = 0; v < tempSkillEfcFuncs.Count; v++)
                {
                    SkillEfcFunc skillEfcFunc = skillSubst.skillEfcFuncs[v];
                    List<AcSkillEfc> acSkillEfcs = new List<AcSkillEfc>();
                    foreach (BattleUnit aUnit in oUnits)
                    {
                        acSkillEfcs.Add(makeFilteredValue(ExSkillEfcFilter(cmdData.sUnit, skillSubst, aUnit), skillEfcFunc.fun(cmdData.sUnit, cmdData.oUnit, this)).makeAcSkillEfc());
                    }
                    applySkillEfc(acSkillEfcs);
                }
            }
            skillSubst = skillSubst.nextSkill;
        }
    }

    public void applySkillEfc(List< AcSkillEfc> acSkillEfcs) 
    {
        foreach (AcSkillEfc acSkillEfc in acSkillEfcs) 
        {
            acSkillEfc.oUnit.applyAcSkillEfc(acSkillEfc);

        }

        List<ParamFilter<int>> applyEventFilters = AfterOneTimeApplyEventFilter(acSkillEfcs);

        foreach (ParamFilter<int> aEvent in applyEventFilters)
        {
            aEvent.fun(0, applyEventFilters);
        }
    }

    private void setPartyBuff(PartyBuffParam partyBuffParam)
    {
        List<PartyBuffParam> targetPartyBuffParams= _allPartyBuffs;
        switch (partyBuffParam.whichPartysBuff)
        {
            case 0://party1/2共用バフ
                targetPartyBuffParams = _allPartyBuffs;
                break;
            case 1://party1バフ
                targetPartyBuffParams = _party1Buffs;
                break;
            case 2://party2バフ
                targetPartyBuffParams = _party2Buffs;
                break;
            default:
                throw new Exception($"partyBuffParam.whichPartysBuffが想定外の値を撮っています:{partyBuffParam.whichPartysBuff}");
                break;

        }
        if (partyBuffParam.integrateID < 0)
        {
            targetPartyBuffParams.Add(partyBuffParam);
        }
        else
        {
            bool isIntegrated = false;
            for (int i = 0; i < targetPartyBuffParams.Count; i++)
            {
                //BuffParam aUnitBuff=_buffParams[i];
                if (targetPartyBuffParams[i].integrateID == partyBuffParam.integrateID)
                {
                    targetPartyBuffParams[i] = targetPartyBuffParams[i].whenApplySameIDBuff(partyBuffParam);
                    targetPartyBuffParams[i].whichPartysBuff = partyBuffParam.whichPartysBuff;
                    isIntegrated = true;
                    break;

                }
            }
            if (!isIntegrated)
            {
                targetPartyBuffParams.Add(partyBuffParam);
                partyBuffParam.whichPartysBuff = partyBuffParam.whichPartysBuff;
            }
        }
    }



   






    public List<BattleUnit> makeOUnitsList(BattleUnit sUnit, SkillOUnitType skillOUnitType, BattleUnit cmdOUnit) 
    {
        List<BattleUnit> oUnits = new List<BattleUnit>();

        switch (skillOUnitType) 
        {
            case SkillOUnitType.User:
                oUnits.Add(sUnit);break;
            case SkillOUnitType.OneAlly:
                oUnits.Add(cmdOUnit);break;
            case SkillOUnitType.OneFoe:
                oUnits.Add(cmdOUnit);break;
            case SkillOUnitType.OneUnit:
                oUnits.Add(cmdOUnit); break;
            case SkillOUnitType.AllAlly:
                oUnits.AddRange(party1.Contains(sUnit)?party1:party2);break;
            case SkillOUnitType.AllFoe:
                oUnits.AddRange(party1.Contains(sUnit) ? party2 : party1); break;
            case SkillOUnitType.AllUnit:
                oUnits.AddRange(party1); oUnits.AddRange(party2); break;
            case SkillOUnitType.RandomAlly:
                oUnits.Add(party1.Contains(sUnit)?party1[UnityEngine.Random.Range(0,party1.Count)]
                    : party2[UnityEngine.Random.Range(0, party2.Count)]); 
                break;
            case SkillOUnitType.RandomFoe:
                oUnits.Add(party1.Contains(sUnit) ? party2[UnityEngine.Random.Range(0, party2.Count)] 
                    : party1[UnityEngine.Random.Range(0, party1.Count)]);
                break;
            case SkillOUnitType.RandomUnit:
                int tempInt= UnityEngine.Random.Range(0, party1.Count + party2.Count);
                oUnits.Add(tempInt < party1.Count ? party1[tempInt] : party2[tempInt -party1.Count]);
                break;
        }
        return oUnits;
    }


    //場のユニットが持つbuffオブジェクトが持つfilterはそのユニットのステータス取得時に呼び出される
    //場のユニットが持つ特性オブジェクトがもつfilterはすべてのユニットのステータス取得時に呼び出される
    //makeFilters------------------------------------------------------------------
    public List<ParamFilter<int>> MaxHPFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit)
    {
        return makeFilter_BuffAndAbility((buff) => buff.MaxHPFilter_WhenCalcSkill(whosParamCalc, sUnit, useSkill, oUnit, this),
                                  (ability, whosAbility) => ability.MaxHPFilter_WhenCalcSkill(whosParamCalc, whosAbility, sUnit, useSkill, oUnit, this));
    }
    public  List<ParamFilter<int>> ATKFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit)
    {
        return makeFilter_BuffAndAbility((buff) => buff.ATKFilter_WhenCalcSkill(whosParamCalc, sUnit, useSkill, oUnit, this),
                           (ability, whosAbility) => ability.ATKFilter_WhenCalcSkill(whosParamCalc, whosAbility, sUnit, useSkill, oUnit, this));
    }
    public List<ParamFilter<int>> DEFFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit)
    {
        return makeFilter_BuffAndAbility((buff) => buff.DEFFilter_WhenCalcSkill(whosParamCalc, sUnit, useSkill, oUnit, this),
                           (ability, whosAbility) => ability.DEFFilter_WhenCalcSkill(whosParamCalc, whosAbility, sUnit, useSkill, oUnit, this));
    }
    public  List<ParamFilter<int>> SATKFilter_WhenCalcSkill(BattleUnit whosParamCalc,  BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit)
    {
        return makeFilter_BuffAndAbility((buff) => buff.SATKFilter_WhenCalcSkill(whosParamCalc, sUnit, useSkill, oUnit, this),
                           (ability, whosAbility) => ability.SATKFilter_WhenCalcSkill(whosParamCalc, whosAbility, sUnit, useSkill, oUnit, this));
    }
    public  List<ParamFilter<int>> SDEFFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit)
    {
        return makeFilter_BuffAndAbility((buff) => buff.SDEFFilter_WhenCalcSkill(whosParamCalc, sUnit, useSkill, oUnit, this),
                   (ability, whosAbility) => ability.SDEFFilter_WhenCalcSkill(whosParamCalc, whosAbility, sUnit, useSkill, oUnit, this));
    }
    public List<ParamFilter<int>> SPDFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit)
    {
        return makeFilter_BuffAndAbility((buff) => buff.SPDFilter_WhenCalcSkill(whosParamCalc, sUnit, useSkill, oUnit,this),
                   (ability, whosAbility) => ability.SPDFilter_WhenCalcSkill(whosParamCalc, whosAbility, sUnit, useSkill, oUnit, this));
    }



    public  List<ParamFilter<List<ElementType>>> TypeFilter_WhenCalcSkill(BattleUnit whosParamCalc, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit)
    {
        return makeFilter_BuffAndAbility((buff) => buff.TypeFilter_WhenCalcSkill(whosParamCalc, sUnit, useSkill, oUnit, this),
                         (ability, whosAbility) => ability.TypeFilter_WhenCalcSkill(whosParamCalc, whosAbility, sUnit, useSkill, oUnit, this));
    }


    public List<ParamFilter<List<BattleUnit>>> OUnitsFilter_WhenCalcSkill(BattleUnit sUnit, SkillSubst useSkillSubst)
    {
        List<ParamFilter<List<BattleUnit>>> filters = new List<ParamFilter<List<BattleUnit>>>();

        filters = makeFilter_BuffAndAbility((buff) => buff.OUnitsFilter_WhenCalcSkill(sUnit, useSkillSubst, this),
                        (ability, whosAbility) => ability.OUnitsFilter_WhenCalcSkill(whosAbility, sUnit, useSkillSubst, this));
        filters.AddRange(useSkillSubst.OUnitsFilter_WhenCalcSkill(sUnit, this));
        return filters;

    }
    public  List<ParamFilter<List<SkillEfcFunc>>> skillEfcFuncFilter_WhenCalcSkill(BattleUnit sUnit, SkillSubst useSkillSubst, BattleUnit oUnit)
    {
        return makeFilter_BuffAndAbility((buff) => buff.skillEfcFuncFilter_WhenCalcSkill(sUnit, useSkillSubst, oUnit, this),
                    (ability, whosAbility) => ability.skillEfcFuncFilter_WhenCalcSkill(whosAbility, sUnit, useSkillSubst, oUnit, this));
    }




    //WhenMakeSkillEfc--------------------------------------------------------------
    public  List<ParamFilter< ExSkillEfc>> ExSkillEfcFilter( BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit)
    {
        List<ParamFilter<ExSkillEfc>> filters = new List<ParamFilter<ExSkillEfc>>();

        filters= makeFilter_BuffAndAbility((buff) => buff.ExSkillEfcFilter(sUnit, useSkill, oUnit, this),
                            (ability, whosAbility) => ability.ExSkillEfcFilter(whosAbility, sUnit, useSkill, oUnit, this));
        filters.AddRange(useSkill.ExSkillEfcFilter(sUnit,oUnit,this));
        filters = filters.OrderByDescending(x => x.priority).ToList();
        return filters;
    }



    public List<ParamFilter<AcSkillEfc>> AcSkillEfcFilter( BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit)
    {
        List<ParamFilter<AcSkillEfc>> filters = new List<ParamFilter<AcSkillEfc>>();
        filters= makeFilter_BuffAndAbility((buff) => buff.AcSkillEfcFilter(sUnit, useSkill, oUnit, this),
                    (ability, whosAbility) => ability.AcSkillEfcFilter(whosAbility, sUnit, useSkill, oUnit, this));
        filters.AddRange(useSkill.AcSkillEfcFilter(sUnit, oUnit, this));
        filters = filters.OrderByDescending(x => x.priority).ToList();
        return filters;
    }



    //WhenSort----------------------------------------------------------
    public List<ParamFilter<(int spd, int actPriority)>> SPDAndPriorityFilter_WhenSort(BattleUnit sUnit, SkillSubst useSkill ,BattleUnit oUnit)
    {
        List<ParamFilter<(int spd, int actPriority)>> filters = new List<ParamFilter<(int spd, int actPriority)>>();
        filters= makeFilter_BuffAndAbility((buff)=>buff.SPDAndPriorityFilter_WhenSort(sUnit, useSkill, oUnit, this),
            (ability,unit)=> ability.SPDAndPriorityFilter_WhenSort(unit, sUnit, useSkill, oUnit, this));

        filters.AddRange(useSkill.SPDAndPriorityFilter_WhenSort(sUnit,this));
        filters = filters.OrderByDescending(x => x.priority).ToList();
        return filters;
    }




    //event---------------------------------------------------------------
    public List<ParamFilter<int>> AfterRoundEventFilter()
    {
        List<ParamFilter<int>> filters = new List<ParamFilter<int>>();
        filters = makeFilter_BuffAndAbility((buff) => buff.AfterRoundEventFilter(this),
            (ability, unit) => ability.AfterRoundEventFilter(unit, this)) ;
        foreach (var aCmd in cmdDatas) 
        {
            filters.AddRange(aCmd.Item2.skillSubst.AfterRoundEventFilter(aCmd.Item1,this));
        }
        filters = filters.OrderByDescending(x => x.priority).ToList();
        return filters;
    }

    public List<ParamFilter<int>> BeforeRoundEventFilter()
    {
        List<ParamFilter<int>> filters = new List<ParamFilter<int>>();
        filters = makeFilter_BuffAndAbility((buff) => buff.BeforeRoundEventFilter(this),
            (ability, whosAbility) => ability.BeforeRoundEventFilter(whosAbility, this));
        foreach (var aCmd in cmdDatas)
        {
            filters.AddRange(aCmd.Item2.skillSubst.BeforeRoundEventFilter(aCmd.Item1, this));
        }
        filters = filters.OrderByDescending(x => x.priority).ToList();
        return filters;

    }

    public List<ParamFilter<int>> BeforeTurnEventFilter(BattleUnit whosTurn)
    {
        List<ParamFilter<int>> filters = new List<ParamFilter<int>>();
        filters = makeFilter_BuffAndAbility((buff) => buff.BeforeTurnEventFilter(whosTurn, this),
            (ability, whosAbility) => ability.BeforeTurnEventFilter(whosAbility, whosTurn, this));
        return filters;

    }

    public List<ParamFilter<int>> AfterTurnEventFilter(BattleUnit whosTurn)
    {
        List<ParamFilter<int>> filters = new List<ParamFilter<int>>();
        filters = makeFilter_BuffAndAbility((buff) => buff.AfterTurnEventFilter(whosTurn, this),
            (ability, whosAbility) => ability.AfterTurnEventFilter(whosAbility, whosTurn, this));
        return filters;

    }
    public List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>> BeforeActCmdEventFilter((BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit) cmdData)
    {
        List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>> filters = new List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>>();
        filters = makeFilter_BuffAndAbility((buff) => buff.BeforeActCmdEventFilter(cmdData, this),
            (ability, whosAbility) => ability.BeforeActCmdEventFilter(whosAbility, cmdData, this));
        filters.AddRange(cmdData.skill.skillSubst.BeforeActCmdEventFilter(cmdData, this));
        filters = filters.OrderByDescending(x => x.priority).ToList();
        return filters;

    }

    public List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>> AfterActCmdEventFilter((BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit) cmdData)
    {
        List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>> filters = new List<ParamFilter<(BattleUnit sUnit, SkillIdentity skill, BattleUnit oUnit)>>();
        filters = makeFilter_BuffAndAbility((buff) => buff.AfterActCmdEventFilter(cmdData, this),
            (ability, whosAbility) => ability.AfterActCmdEventFilter(whosAbility, cmdData, this));
        filters.AddRange(cmdData.skill.skillSubst.BeforeActCmdEventFilter(cmdData, this));
        filters = filters.OrderByDescending(x => x.priority).ToList();
        return filters;

    }

    public List<ParamFilter<int>> AfterOneTimeApplyEventFilter(List<AcSkillEfc> acSkillEfcs)
    {
        return makeFilter_BuffAndAbility((buff) => buff.AfterOneTimeApplyEventFilter( acSkillEfcs, this),
            (ability, whosAbility) => ability.AfterOneTimeApplyEventFilter(whosAbility, acSkillEfcs, this));
    }



    public BattleField() 
    {
        _party1 = new List<BattleUnit>();
        _party2 = new List<BattleUnit>();

    }



    //便利関数--------------------------------------------------------------------------------------------------
    public Type1 makeFilteredValue<Type1>( List<ParamFilter<Type1>> filters, Type1 value) 
    {
        Type1 ReturnValue=value;
        foreach (ParamFilter<Type1> aFilter in filters) 
        {
            ReturnValue = aFilter.fun(value,filters);
        }
        return ReturnValue;
    }

    private List<ParamFilter<T>> makeFilter_BuffAndAbility<T>(Func<BuffParam, List<ParamFilter<T>>> buffFilterSel, Func<AbilityIdentity, BattleUnit, List<ParamFilter<T>>> identityFilterSel)
    {
        List<ParamFilter<T>> filters = new List<ParamFilter<T>>();
        foreach (BattleUnit aUnit in party1)
        {
            foreach (BuffParam aBuff in aUnit.buffParams)
            {
                filters.AddRange(buffFilterSel(aBuff));
            }
        }
        foreach (BattleUnit aUnit in party2)
        {
            foreach (BuffParam aBuff in aUnit.buffParams)
            {
                filters.AddRange(buffFilterSel(aBuff));
            }
        }
        foreach (BattleUnit aUnit in party1)
        {
            filters.AddRange(identityFilterSel(aUnit.status.ability, aUnit));
        }
        foreach (BattleUnit aUnit in party2)
        {
            filters.AddRange(identityFilterSel(aUnit.status.ability, aUnit));
        }
        filters = filters.OrderByDescending(x => x.priority).ToList();
        return filters;
    }
}
