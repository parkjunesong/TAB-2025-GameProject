using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public List<Unit> PlayerUnits = new();
    public List<Unit> EnemyUnits = new();
    public bool isPlayerActioned;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;     
    }
    void Start()
    {
        BattleStart();
        GetComponent<SkillManager>().UpdateUi();
        GetComponent<PokemonEntryManager>().UpdateUi();
        GetComponent<BattleUiManager>().UpdateUi();
    }
    public void BattleStart()
    {
        BattleUnitManager BUM = GameObject.Find("DataManager").GetComponent<BattleUnitManager>();
        foreach (UnitData data in BUM.PlayerUnitData)
        {
            Unit unit = Instantiate(data.gameObject).AddComponent<PlayerUnit>();
            unit.Init(data);
            PlayerUnits.Add(unit);
        }
        foreach (UnitData data in BUM.EnemyUnitData)
        {
            Unit unit = Instantiate(data.gameObject).AddComponent<EnemyUnit>();
            unit.Init(data);
            EnemyUnits.Add(unit);           
        }
        PlayerUnits[0].gameObject.SetActive(true);
        EnemyUnits[0].gameObject.SetActive(true);
        TurnStart();
    }

    public void TurnStart()
    {
        PlayerUnits[0].TurnStart();
        EnemyUnits[0].TurnStart();
        StartCoroutine(ActionStart());
    }
    IEnumerator ActionStart()
    {
        isPlayerActioned = false;
        if (PlayerUnits[0].Status.SP >= EnemyUnits[0].Status.SP)
        {
            DialogueManager.Instance.StartDialogue(new List<string> { PlayerUnits[0].Data.Name+"은(는) 무엇을 할까?" });
            yield return new WaitUntil(() => isPlayerActioned);
            GetComponent<PokemonEntryManager>().UpdateUi();
            GetComponent<BattleUiManager>().UpdateUi();
            yield return new WaitForSeconds(4f);

            yield return StartCoroutine(EnemyUnits[0].GetComponent<EnemyUnit>().Action());
            GetComponent<PokemonEntryManager>().UpdateUi();
            GetComponent<BattleUiManager>().UpdateUi();
            yield return new WaitForSeconds(4f);
        }
        else
        {
            yield return StartCoroutine(EnemyUnits[0].GetComponent<EnemyUnit>().Action());
            GetComponent<PokemonEntryManager>().UpdateUi();
            GetComponent<BattleUiManager>().UpdateUi();
            yield return new WaitForSeconds(4f);

            DialogueManager.Instance.StartDialogue(new List<string> { PlayerUnits[0].Data.Name + "은(는) 무엇을 할까?" });
            yield return new WaitUntil(() => isPlayerActioned);
            GetComponent<PokemonEntryManager>().UpdateUi();
            GetComponent<BattleUiManager>().UpdateUi();
            yield return new WaitForSeconds(4f);
        }
        GetComponent<PokemonEntryManager>().UpdateUi();
        GetComponent<BattleUiManager>().UpdateUi();
        TurnEnd();
    }
    public void TurnEnd()
    {
        DialogueManager.Instance.EndDialogue();
        PlayerUnits[0].TurnEnd();
        EnemyUnits[0].TurnEnd();
        TurnStart();
    }  
    public void BattleEnd(string team)
    {
        if (team == "Player")
        {
            Debug.Log("Enemy Win");
        }
        else if (team == "Enemy")
        {
            Debug.Log("Player Win");
            AudioManager.Instance.PlayVictoryTrainer();
        }
    }
    public void OnUnitDied(List<Unit> targetList)
    {
        Unit tmp = null;
        bool isAllDead = true;

        foreach (Unit unit in targetList)
        {
            if (!unit.isDead)
            {
                isAllDead = false;
                tmp = unit;
                break;
            }
        }
        if (isAllDead) BattleEnd(targetList[0].Team);
        else
        {
            targetList.Add(targetList[0]);
            targetList.RemoveAt(0);
            targetList.Remove(tmp);
            targetList.Insert(0, tmp);
            targetList[0].gameObject.SetActive(true);
            GetComponent<PokemonEntryManager>().UpdateUi();
        }
    }
    public List<Unit> allUnits()
    {
        List<Unit> units = new();
        foreach (Unit unit in PlayerUnits) units.Add(unit);
        foreach (Unit unit in EnemyUnits) units.Add(unit);
        return units;
    }
}