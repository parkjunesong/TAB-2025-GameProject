using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Damage : Effect_Base
{
    public Effect_Damage(float value, Type type) : base(value, type) { }

    public override void Execute(Unit caster)
    {
        //Target.OnDamaged(getDamage(caster));
    }
    public float getDamage(Unit caster)
    {
        float damage = Value * caster.Status.AT;
        if (Random.Range(0, 100) < 10) damage *= 1.5f; //±Þ¼Ò

        return damage;
    }
}
