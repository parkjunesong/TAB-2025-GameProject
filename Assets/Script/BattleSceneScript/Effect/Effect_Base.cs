using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect_Base
{
    public float Value;
    public Type Type;

    public Effect_Base(float value, Type type)
    {       
        Value = value;
        Type = type;       
    }
    public abstract void Execute(Unit caster);    
}
