using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Damage : Effect_Base
{
    public Effect_Damage(Type type, int val, int acc, int pp, int pri, bool isself = false) : base(type, val, acc, pp, pri, isself) { }

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
