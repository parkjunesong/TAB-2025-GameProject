using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Damage : Effect_Base
{
    public Effect_Damage(int val, int acc, int pri, bool isself = false) : base(val, acc, pri, isself) { }

    public override void Execute(Unit caster)
    {
        SetTarget(caster).OnDamaged(getDamage(caster));
    }
    private float getDamage(Unit caster)
    {
        float damage = Value * caster.Status.AT;
        if (Random.Range(0, 100) < 10) damage *= 1.5f; //±Þ¼Ò

        return damage;
    }
}
