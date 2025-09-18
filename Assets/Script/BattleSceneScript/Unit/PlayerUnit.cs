using UnityEngine;

public class PlayerUnit : Unit
{
    public override void Init(UnitData Data)
    {
        Team = "Player";
        UnitData data = Instantiate(Data);
        Status = new Unit_Status(data);
        Skill = new Unit_Skill(this);
    }
    public override void TurnStart()
    {

    }
    public override void TurnEnd()
    {

    }
}
