using UnityEngine;
using UnityEngine.UIElements;
public class PlayerUnit : Unit
{
    public override void Init(UnitData data)
    {
        Team = "Player";
        Data = Instantiate(data);
        Status = new Unit_Status(Data);
        Skill = new Unit_Skill(this);
        GetComponent<SpriteRenderer>().sprite = data.BackSprite;

        transform.position = new Vector2(-3.5f, -2);
        transform.localScale = new Vector3(8, 10, 1);
        gameObject.SetActive(false);
    }
    public override void TurnStart() { }
    public override void TurnEnd() { }
}

