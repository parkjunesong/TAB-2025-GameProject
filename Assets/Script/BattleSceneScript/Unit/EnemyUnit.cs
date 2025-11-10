using System.Collections;
using UnityEngine;

public class EnemyUnit : Unit
{
    public override void Init(UnitData data)
    {
        isDead = false;
        Team = "Enemy";
        Data = Instantiate(data);
        Status = new Unit_Status(Data);
        Skill  = new Unit_Skill(Data, this);
        GetComponent<SpriteRenderer>().sprite = data.FrontSprite;

        transform.position = new Vector2(5, 2);
        transform.localScale = new Vector3(5, 5, 1);
        gameObject.SetActive(false);
        name = Team + " " + Data.Name;

    }
    public override void TurnStart() { }
    public override void TurnEnd() { }
    public IEnumerator Action()
    {
        Debug.Log("enemy action");

        Skill.UseSkillNo(Random.Range(0, 4));
        yield return new WaitForSeconds(1f);
    }
    public override void OnDied()
    {
        isDead = true;
        gameObject.SetActive(false);
        BattleManager.Instance.OnUnitDied(BattleManager.Instance.EnemyUnits);
    }
}
