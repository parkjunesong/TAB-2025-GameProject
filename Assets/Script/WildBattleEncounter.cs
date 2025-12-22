using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WildBattleEncounter : MonoBehaviour
{
    public List<UnitData> wildUnits = new();
    private Coroutine encounter;
    private IEnumerator Encounter()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0);

            if (Random.value < 0.2f)
            {
                AudioManager.Instance.PlayWildBattle();
                GameObject.FindGameObjectWithTag("PlayerMe").GetComponent<PlayerMovement>().canMove = false;

                yield return ScreenFader.Instance.StartCoroutine(ScreenFader.Instance.BattleEncount());

                yield return new WaitForSeconds(3f);
                StartWildBattle();
            }
        }
    }
    private void StartWildBattle()
    {
        UnitData selected = wildUnits[Random.Range(0, wildUnits.Count)];

        var BUM = GameObject.Find("DataManager").GetComponent<BattleUnitManager>();
        BUM.EnemyUnitData.Clear();
        BUM.EnemyUnitData.Add(selected);

        SceneManager.LoadScene("Battle Scene");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerMe")) { encounter = StartCoroutine(Encounter()); }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerMe")) { StopCoroutine(encounter); encounter = null; } 
    }   
}