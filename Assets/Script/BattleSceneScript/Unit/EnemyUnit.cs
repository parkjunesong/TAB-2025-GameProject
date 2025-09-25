using UnityEngine;
public class EnemyUnit : Unit
{
    public override void Init(UnitData data)
    {
        Team = "Enemy";
        Data = Instantiate(data);
        Status = new Unit_Status(Data);
        Skill  = new Unit_Skill(this);
        GetComponent<SpriteRenderer>().sprite = data.FrontSprite;

        transform.position = new Vector2(5, 2);
        transform.localScale = new Vector3(6, 8, 1);
        gameObject.SetActive(false);
    }
    public override void TurnStart() { }
    public override void TurnEnd() { }
}
