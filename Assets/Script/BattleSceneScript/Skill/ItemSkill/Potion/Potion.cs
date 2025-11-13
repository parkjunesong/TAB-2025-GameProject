using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Potion", menuName = "Scriptable Object/Skill/Potion")]

public class Potion : Skill_Base
{
    public override void SetEffect()
    {
        Effect_Heal effect = new Effect_Heal(20, 100, 0, Skill_Type, true);
        EffectList.Add(effect);
    }
    public override void Execute(Unit caster)
    {
        EffectList[0].Execute(caster);
    }
}
    