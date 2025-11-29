using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Effect_Damage : Effect_Base
{
    private TypeData Skill_Type;
    private GameObject HitVFX;

    public Effect_Damage(int val, int acc, int pri, TypeData type, GameObject vfx, bool isself = false) : base(val, acc, pri, isself) 
    {
        Skill_Type = type;
        HitVFX = vfx;
    }

    public override async void Execute(Unit caster)
{
    Unit target = SetTarget(caster);
    float damage = getDamage(caster, target);

    // 🔹 1) 타입 상성 배율만 따로 계산 (SFX 용도)
    float multiplier = 1f;

    foreach (Type type in Skill_Type.Very_Effective_Type)
        if (type == target.Status.Type) multiplier *= 2f;

    foreach (Type type in Skill_Type.Not_Very_Effective_Type)
        if (type == target.Status.Type) multiplier *= 0.5f;

    foreach (Type type in Skill_Type.Not_Effective_Type)
        if (type == target.Status.Type) multiplier = 0f;

    // 🔹 2) 이펙트 & 블링크
    VFXManager.Instance.HitVFX(HitVFX, target);
    await Task.Delay(1000);
    VFXManager.Instance.HitBlink(target);

    // 🔹 3) 실제 데미지 적용
    target.OnDamaged(damage);

    // 🔹 4) SFX 재생 (상성에 따라 선택)
    if (AudioManager.Instance != null)
    {
        if (multiplier == 0f)
        {
            // "효과가 없다!"일 때는 약한 데미지음으로 처리하거나, 아예 소리 안 내도 됨
            AudioManager.Instance.PlayDamageWeak();
        }
        else if (multiplier > 1.01f)
        {
            AudioManager.Instance.PlayDamageSuper();   // 매우 효과적
        }
        else if (multiplier < 0.99f)
        {
            AudioManager.Instance.PlayDamageWeak();    // 별로 효과적이지 않음
        }
        else
        {
            AudioManager.Instance.PlayDamageNormal();  // 보통
        }
    }
}


    private float getDamage(Unit caster, Unit target)
    {     
        float damage = caster.Status.AT * Value * (2 * caster.Status.Level / 5f + 2);

        foreach (Type type in Skill_Type.Very_Effective_Type)
            if (type == target.Status.Type) damage *= 2;
        foreach (Type type in Skill_Type.Not_Very_Effective_Type)
            if (type == target.Status.Type) damage *= 0.5f;
        foreach (Type type in Skill_Type.Not_Effective_Type)
            if (type == target.Status.Type) return 0;

        if (Skill_Type.Type == caster.Status.Type) damage *= 1.5f;
        if (Random.Range(0, 100) < 10) damage *= 2; 

        if (caster.Data.Item != null)
        {
            var itemdata = caster.Data.Item.GetComponent<ItemData>();
        }
        return damage;
    }
}
