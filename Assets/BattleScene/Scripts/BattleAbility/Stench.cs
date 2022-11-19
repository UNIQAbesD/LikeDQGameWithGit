using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/BattleAbility/Stench", fileName = "Stench")]
public class Stench : AbilityIdentity//あくしゅう
{
    [SerializeField] private int _filterPriority = -100;
    public override List<ParamFilter<AcSkillEfc>> AcSkillEfcFilter(BattleUnit whosAbility, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<AcSkillEfc>>();
    }

    public override List<ParamFilter<int>> AfterOneTimeApplyEventFilter(BattleUnit whosAbility, List<AcSkillEfc> acSkillEfcs, BattleField bf)
    {

        foreach (var aSkillEfc in acSkillEfcs) 
        {
            if (aSkillEfc.sUnit==whosAbility&aSkillEfc.hpDamage>0) 
            {

            }
        }
        return new List<ParamFilter<int>>();
    }
}
