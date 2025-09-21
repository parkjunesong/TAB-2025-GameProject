using UnityEngine;

public class PlayerUnit : Unit
{
    public override void Init(UnitData Data)
    {
        Team = "Player";


        UnitData data = Instantiate(Data);
        Status = new Unit_Status(data);
        Skill  = new Unit_Skill(this);

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null && data != null)
        {
            if (data.BackSprite != null)          sr.sprite = data.BackSprite;          // 필드명이 Back인 경우
            else if (data.BackSprite != null) sr.sprite = data.BackSprite;    // 필드명이 BackSprite인 경우
        }
    }

    public override void TurnStart() { }
    public override void TurnEnd() { }
}

