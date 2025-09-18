using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Status
{
    public string Name;
    public Type Type;
    public int AT, SP, HP, DF;
    private int maxHP;

    public Unit_Status(UnitData data)
    {
        Name = data.Name;
        Type = data.Type;
        AT = data.AT;
        SP = data.SP;
        HP = data.HP;
        maxHP = HP;
        DF = data.DF;
    }

    public void OnDamaged(float damage)
    {
        //타입상성 계산

        HP -= (int)(damage * 100 / (100 + DF));       
    }
}