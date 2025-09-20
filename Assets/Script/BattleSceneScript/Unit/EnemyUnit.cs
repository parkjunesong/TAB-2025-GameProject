using UnityEngine;

public class EnemyUnit : Unit
{
    public override void Init(UnitData data)
    {
        Team = "Enemy";

        UnitData unitData = Instantiate(data);
        this.Data = unitData;

        Status = new Unit_Status(this.Data);
        Skill  = new Unit_Skill(this);


        var sr = GetComponent<SpriteRenderer>();
        if (sr != null && this.Data != null && this.Data.Front != null)
            sr.sprite = this.Data.Front;
    }

    public override void TurnStart()
    {
 
    }

    public override void TurnEnd()
    {

    }
}

