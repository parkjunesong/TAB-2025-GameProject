using UnityEngine;

public class EnemyUnit : Unit
{
    public override void Init(UnitData data)
    {
        Team = "Enemy";

        this.Data = Instantiate(data);

        Status = new Unit_Status(this.Data);
        Skill  = new Unit_Skill(this);

        // 적 유닛은 Front 스프라이트 사용
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null && this.Data != null && this.Data.FrontSprite != null)
            sr.sprite = this.Data.FrontSprite;
    }

    public override void TurnStart() { }
    public override void TurnEnd() { }
}

