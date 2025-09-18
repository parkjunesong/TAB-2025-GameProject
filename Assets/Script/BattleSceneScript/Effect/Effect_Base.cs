using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect_Base
{
    public Type Type;
    public int Value;
    public int Accuracy;
    public int PP;
    public int Priority;
    public bool IsSelf;

    public Effect_Base(Type type, int val, int acc, int pp, int pri, bool isSelf = false)
    {       
        Type = type;
        Value = val;
        Accuracy = acc;
        PP = pp;
        Priority = pri;
        IsSelf = isSelf;
    }
    public abstract void Execute(Unit caster);  
    
    public Unit SetTarget(Unit caster)
    {
        if (IsSelf) return caster;
        else
        {
            if (caster.Team == "Player") return BattleManager.Instance.FrontEnemyUnit;
            if(caster.Team == "Enemy") return BattleManager.Instance.FrontPlayerUnit;
            return null;
        }     
    }
}
