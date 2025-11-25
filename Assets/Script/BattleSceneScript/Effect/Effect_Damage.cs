using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Damage : Effect_Base
{
    private TypeData Skill_Type;
    public Effect_Damage(int val, int acc, int pri, TypeData type, bool isself = false) : base(val, acc, pri, isself) 
    {
        Skill_Type = type;
    }

    public override void Execute(Unit caster)
    {
        Unit target = SetTarget(caster);
        float damage = getDamage(caster, target);
        target.OnDamaged(damage);  
        
        if (SkillEffectManager.Instance != null && target != null)
    {
        // 살짝 위로 띄우고 싶으면 + Vector3.up * 0.5f 같은 식으로 오프셋 줄 수도 있음
        SkillEffectManager.Instance.PlayHitEffect(target.transform.position);
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
