using UnityEngine;

public class EnemyUnit : Unit
{
    public override void Init(UnitData Data)
    {
        Team = "Enemy";

        UnitData data = Instantiate(Data);

        Status = new Unit_Status(data);
        Skill  = new Unit_Skill(this);

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null && data != null)
        {
            if (data.FrontSprite != null)            sr.sprite = data.FrontSprite;         
            else if (data.FrontSprite != null) sr.sprite = data.FrontSprite;    
        }
    }

    public override void TurnStart() { }
    public override void TurnEnd() { }
}

