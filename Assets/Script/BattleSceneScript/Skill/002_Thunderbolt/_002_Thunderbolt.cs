using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Thunderbolt", menuName = "Scriptable Object/Skill/002_Thunderbolt")]

public class _002_Thunderbolt : Skill_Base
{
    public override void SetEffect()
    {
        Effect_Damage effect = new Effect_Damage(90, 100, 0, Skill_Type, Skill_VFX);
        EffectList.Add(effect);
    }
    public override void Execute(Unit caster)
    {
        EffectList[0].Execute(caster);
        SkillMessage(caster);
    }
}
    