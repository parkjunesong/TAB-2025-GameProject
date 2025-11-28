using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "QuickAttack", menuName = "Scriptable Object/Skill/004_QuickAttack")]

public class _004_QuickAttack : Skill_Base
{
    public override void SetEffect()
    {
        Effect_Damage effect = new Effect_Damage(40, 100, 1, Skill_Type, Skill_VFX);
        EffectList.Add(effect);
    }
    public override void Execute(Unit caster)
    {
        EffectList[0].Execute(caster);
        SkillMessage(caster);
    }
}
    