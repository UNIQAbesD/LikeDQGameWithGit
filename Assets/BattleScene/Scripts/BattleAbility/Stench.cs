using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/BattleAbility/Stench", fileName = "Stench")]
public class Stench : AbilityIdentity//‚ ‚­‚µ‚ã‚¤
{
    [SerializeField] private int _filterPriority = -100;
    public virtual List<ParamFilter<AcSkillEfc>> AcSkillEfcFilter(BattleUnit whosAbility, BattleUnit sUnit, SkillSubst useSkill, BattleUnit oUnit, BattleField bf)
    {
        return new List<ParamFilter<AcSkillEfc>>();
    }
}
