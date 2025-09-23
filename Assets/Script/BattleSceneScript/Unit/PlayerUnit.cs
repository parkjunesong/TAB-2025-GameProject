using UnityEngine;
public class PlayerUnit : Unit
{
    public override void Init(UnitData Data)
    {
        Team = "Player";
        UnitData data = Instantiate<UnitData>(Data);
        this.Data = data;

        Status = new Unit_Status(data);
        Skill  = new Unit_Skill(this);
        name   = data.Name;
        
    }
    public override void TurnStart() { }
    public override void TurnEnd() { }
}

