using UnityEngine;

public class PlayerUnit : Unit
{
    public override void Init(UnitData data)
    {
        Team = "Player";

        // 필요 없으면 그냥 this.Data = data; 해도 됨
        this.Data = Instantiate(data);

        // 스탯과 스킬 초기화
        Status = new Unit_Status(this.Data);
        Skill = new Unit_Skill(this);

        // 플레이어 유닛은 Back 스프라이트 사용
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null && this.Data != null && this.Data.BackSprite != null)
            sr.sprite = this.Data.BackSprite;
    }

    public override void TurnStart() { }
    public override void TurnEnd() { }
    
}
