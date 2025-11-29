using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect_Base
{
    public int Value;
    public int Accuracy;
    public int Priority;
    public bool IsSelf;

    public Effect_Base(int val, int acc, int pri, bool isSelf = false)
    {       
        Value = val;
        Accuracy = acc;
        Priority = pri;
        IsSelf = isSelf;
    }
    public abstract void Execute(Unit caster);    
}
