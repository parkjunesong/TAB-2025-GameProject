using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public bool isDead;
    public string Team;
    public UnitData Data;
    public Unit_Status Status;
    public Unit_Skill Skill;

    public abstract void Init(UnitData Data);
    public abstract void TurnStart();
    public abstract void TurnEnd();
    public abstract IEnumerator Action();
    public abstract void OnDied();


    public void OnDamaged(float damage)
    {
        Status.OnDamaged(damage);

        if (Status.HP <= 0) OnDied();
    }
    

}