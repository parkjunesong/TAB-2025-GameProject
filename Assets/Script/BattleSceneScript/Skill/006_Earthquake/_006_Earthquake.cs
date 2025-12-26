using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "Earthquake", menuName = "Scriptable Object/Skill/006_Earthquake")]

public class _006_Earthquake : Skill_Base
{
    public override void SetEffect()
    {
        Effect_Damage effect = new Effect_Damage(100, 100, 0, Skill_Type);
        EffectList.Add(effect);
    }
    public override async void Execute(Unit caster)
    {
        VFXManager.Instance.HitVFX(Skill_VFX, BattleTarget.getTarget(caster));
        AudioManager.Instance.PlaySfx(Skill_SFX);
        SkillMessage(caster);

        await Task.Delay(1000);

        EffectList[0].Execute(caster);
    }
}
    