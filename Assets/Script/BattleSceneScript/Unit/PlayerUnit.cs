using System.Collections;
using UnityEngine;

public class PlayerUnit : Unit
{
    public override void Init(UnitData data)
    {
        Team = "Player";
        Data = Instantiate(data);
        Status = new Unit_Status(Data);
        Skill = new Unit_Skill(Data, this);
        GetComponent<SpriteRenderer>().sprite = data.BackSprite;

        transform.position = new Vector2(-3.5f, -2);
        transform.localScale = new Vector3(8, 10, 1);
        gameObject.SetActive(false);
    }
    public override void TurnStart() { }
    public override void TurnEnd() { }

    public override IEnumerator Action()
    {
        Debug.Log("player action");

        // 스킬, 가방, 교체, 도망
        yield return new WaitForSeconds(1f);
    }
}

