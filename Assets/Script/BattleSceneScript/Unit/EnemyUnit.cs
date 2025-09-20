using UnityEngine;

public class EnemyUnit : Unit
{
    public override void Init(UnitData Data)
    {
        UnitData data = Instantiate(Data);
        Status = new Unit_Status(data);
        Skill = new Unit_Skill(this);

        name = Status.Name;
    }
    public override void TurnStart()
    {

    }
    public override void TurnEnd()
    {

    }
}
