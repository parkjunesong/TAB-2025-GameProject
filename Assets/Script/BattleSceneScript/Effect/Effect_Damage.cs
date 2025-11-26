using System.Collections;
using System.Collections.Generic;
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

    public override void Execute(Unit caster)
    {
        Unit target = SetTarget(caster);
        float damage = getDamage(caster, target);
        target.OnDamaged(damage);

        VFXManager.Instance.HitVFX(HitVFX, target.transform.position);
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
