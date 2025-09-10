using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public UnitData Data;
    public Unit_Status Status;
    public Unit_Skill Skill;

    public abstract void Init();
    public abstract void TurnStart();
    public abstract void TurnEnd();
    
    public void OnDamaged(float damage)
    {
        Status.OnDamaged(damage);

        if (Status.HP <= 0) OnDied();
    }
    public void OnDied()
    {
        //BattleManager.Instance.OnUnitDied(this);
    }
}
