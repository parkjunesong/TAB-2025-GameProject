using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Heal : Effect_Base
{
    public Effect_Heal(int val, int acc, int pri, TypeData type, bool isself = false) : base(val, acc, pri, isself) { }

    public override void Execute(Unit caster)
    {
        Unit target = SetTarget(caster);
        target.OnHealed(Value);
    }
}