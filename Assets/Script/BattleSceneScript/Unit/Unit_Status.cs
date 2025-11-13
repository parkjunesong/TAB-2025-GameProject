using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Status
{
    public int Level;
    public Type Type;
    public int AT, SP, HP, DF;
    public int maxHP;

    public Unit_Status(UnitData data, int level = 1)
    {
        Level = level;
        Type = data.Type;
        AT = (int)((data._AT * 2) * (Level / 100f)) + 10;
        SP = (int)((data._SP * 2) * (Level / 100f)) + 10;
        HP = (int)((data._HP * 2 + 100) * (Level / 100f)) + 10;
        DF = (int)((data._DF * 2) * (Level / 100f)) + 10;
        maxHP = HP;
    }

    public void OnDamaged(float damage)
    {
        HP -= (int)((damage / DF) / 50f + 2);
    }
    public void OnHealed(float value)
    {
        HP += (int)(value);
        if (HP >= maxHP) HP = maxHP;
    }
}