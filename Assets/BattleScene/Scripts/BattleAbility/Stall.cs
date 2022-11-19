using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "ScriptableObject/BattleAbility/Stall", fileName = "Stall")]
public class Stall : AbilityIdentity//あとだし
{
    [SerializeField]private int _filterPriority=-100;
    public virtual List<ParamFilter<(int spd, int actPriority)>> SPDAndPriorityFilter_WhenSort(BattleUnit whosAbility, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        List<ParamFilter<(int spd, int actPriority)>> ReturnValue = new List<ParamFilter<(int spd, int actPriority)>>();
        ReturnValue.Add(new ParamFilter<(int spd, int actPriority)>(
            ((int spd, int actPriority) value,List<ParamFilter<(int spd, int actPriority)>> filterList)=>
            {
                int ReturnSPD= Mathf.Abs( value.spd);
                ReturnSPD = ReturnSPD - 100000;
                return (ReturnSPD, value.actPriority);
            },_filterPriority,this));
        return ReturnValue;
    }
}
