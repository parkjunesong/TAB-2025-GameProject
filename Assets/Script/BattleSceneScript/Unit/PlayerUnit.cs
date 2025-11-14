using System.Collections;
using UnityEngine;

public class PlayerUnit : Unit
{
    public override void Init(UnitData data)
    {
        isDead = false;
        Team = "Player";
        Data = Instantiate(data);
        Status = new Unit_Status(Data);
        Skill = new Unit_Skill(Data, this);
        GetComponent<SpriteRenderer>().sprite = data.BackSprite;

        transform.position = new Vector2(-4, -9.5f);
        transform.localScale = new Vector3(7, 8, 1);
        gameObject.SetActive(false);
        name = Team + " " + Data.Name;
    }
    public override void TurnStart() { }
    public override void TurnEnd() { }
    public override void OnDied()
    {
        isDead = true;
        gameObject.SetActive(false);
        BattleManager.Instance.OnUnitDied(BattleManager.Instance.PlayerUnits);
    }
}

