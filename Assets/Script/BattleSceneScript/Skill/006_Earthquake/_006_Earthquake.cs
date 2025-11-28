using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Earthquake", menuName = "Scriptable Object/Skill/006_Earthquake")]

public class _006_Earthquake : Skill_Base
{
    public override void SetEffect()
    {
        Effect_Damage effect = new Effect_Damage(100, 100, 0, Skill_Type, Skill_VFX);
        EffectList.Add(effect);
    }
    public override void Execute(Unit caster)
    {
        EffectList[0].Execute(caster);
        SkillMessage(caster);
    }
}
    