using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "DragonPulse", menuName = "Scriptable Object/Skill/007_DragonPulse")]

public class _007_DragonPulse : Skill_Base
{
    public override void SetEffect()
    {
        Effect_Damage effect = new Effect_Damage(85, 100, 0, Skill_Type);
        EffectList.Add(effect);
    }
    public override async void Execute(Unit caster)
    {
        VFXManager.Instance.HitVFX(Skill_VFX, BattleTarget.getTarget(caster));
        AudioManager.Instance.PlaySfx(Skill_SFX);
        SkillMessage(caster);

        await Task.Delay(2000);

        EffectList[0].Execute(caster);
    }
}