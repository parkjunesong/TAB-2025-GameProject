using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Scratch", menuName = "Scriptable Object/Skill/001_Scratch")]

public class _001_Scratch : Skill_Base
{
    public override void SetEffect()
    {
        Effect_Damage effect = new Effect_Damage(40, 100, 0, Skill_Type);
        EffectList.Add(effect);
    }
    public override void Execute(Unit caster)
    {
        EffectList[0].Execute(caster);
        DialogueManager.Instance.StartDialogue(new List<string> { Skill_Name +" АјАн!"}, DialogueEndCase.always);
    }
}
    