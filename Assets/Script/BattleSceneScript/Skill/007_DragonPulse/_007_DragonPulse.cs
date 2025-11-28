using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DragonPulse", menuName = "Scriptable Object/Skill/007_DragonPulse")]

public class _007_DragonPulse : Skill_Base
{
    public override void SetEffect()
    {
        Effect_Damage effect = new Effect_Damage(85, 100, 0, Skill_Type, Skill_VFX);
        EffectList.Add(effect);
    }
    public override void Execute(Unit caster)
    {
        EffectList[0].Execute(caster);
        SkillMessage(caster);
    }
}
    