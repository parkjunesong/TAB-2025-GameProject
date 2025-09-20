using UnityEngine;

public class PlayerUnit : Unit
{
    public override void Init(UnitData data)
    {
        Team = "Player";

        this.Data = Instantiate(data);

        // 스탯과 스킬 초기화
        Status = new Unit_Status(this.Data);
        Skill  = new Unit_Skill(this);

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null && this.Data != null && this.Data.Back != null)
            sr.sprite = this.Data.Back;
    }

    public override void TurnStart() { }
    public override void TurnEnd() { }
}
