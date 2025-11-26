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
    public abstract void OnDied();

    public void OnDamaged(float value)
{
    // 1. HP 감소
    Status.OnDamaged(value);

    // 2. 스프라이트 깜빡 (여기가 핵심)
    var flash = GetComponent<SpriteHitFlash>();
    if (flash != null)
    {
        flash.PlayFlash();   // ← 맞은 Unit의 SpriteHitFlash 실행
    }

    // 3. 죽었으면 처리
    if (Status.HP <= 0)
        OnDied();
}
    
    public void OnHealed(float value)
    {
        Status.OnHealed(value);
    }
}