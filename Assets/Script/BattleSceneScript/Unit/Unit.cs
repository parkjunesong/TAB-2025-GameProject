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

    [Header("Hit Effect")]
    public GameObject hitEffectPrefab;

    public abstract void Init(UnitData Data);
    public abstract void TurnStart();
    public abstract void TurnEnd();
    public abstract void OnDied();

    public void OnDamaged(float value)
    {
        Status.OnDamaged(value);

        var flash = GetComponent<SpriteHitFlash>();
        if (flash != null)
        {
            flash.PlayFlash();
        }
        if (Data != null && Data.HitEffectPrefab != null)
    {
        Instantiate(
            Data.HitEffectPrefab,
            transform.position,           // 포켓몬 위치
            Quaternion.identity
        );
    }

        if (Status.HP <= 0) OnDied();
    }
    
    public void OnHealed(float value)
    {
        Status.OnHealed(value);
    }
}