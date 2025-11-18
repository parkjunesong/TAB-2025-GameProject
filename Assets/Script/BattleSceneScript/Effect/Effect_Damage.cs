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
        target.OnDamaged(getDamage(caster, target));
    }
    private float getDamage(Unit caster, Unit target)
    {     
        float damage = caster.Status.AT * Value * (2 * caster.Status.Level / 5f + 2);

        if (Skill_Type.Type == caster.Status.Type) damage *= 1.5f; // �ڼӺ���
        if (Random.Range(0, 100) < 10) damage *= 2; // �޼�
        // Ư��
        // ����

         if (caster.EquippedItem != null &&
        caster.EquippedItem.IsDamageBoostItem)
    {
        damage *= caster.EquippedItem.DamageMultiplier;   // 생명의 구슬 = 1.3f
    }

        // Ÿ�� ��
        foreach (Type type in Skill_Type.Very_Effective_Type)
            if (type == target.Status.Type) damage *= 2;
        foreach (Type type in Skill_Type.Not_Very_Effective_Type)
            if (type == target.Status.Type) damage *= 0.5f;
        foreach (Type type in Skill_Type.Not_Effective_Type)
            if (type == target.Status.Type) damage *= 0;

        return damage;
    }
}
