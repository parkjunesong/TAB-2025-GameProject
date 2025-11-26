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
        if (AudioManager.Instance != null)
        {
            Debug.Log("BattleManager: PlayBattleBgm 호출");
            AudioManager.Instance.PlayBattleBgm();
        }
        else
        {
            Debug.Log("BattleManager: AudioManager.Instance 가 null");
        }
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
            DialogueManager.Instance.StartDialogue(new List<string> { PlayerUnits[0].Data.Name+"��(��) ������ �ұ�?" });
            yield return new WaitUntil(() => isPlayerActioned);
            GetComponent<PokemonEntryManager>().UpdateUi();
            GetComponent<BattleUiManager>().UpdateUi();
            yield return new WaitForSeconds(5f);


            yield return StartCoroutine(EnemyUnits[0].GetComponent<EnemyUnit>().Action());
            GetComponent<PokemonEntryManager>().UpdateUi();
            GetComponent<BattleUiManager>().UpdateUi();
            yield return new WaitForSeconds(5f);

        }
        else
        {
            yield return StartCoroutine(EnemyUnits[0].GetComponent<EnemyUnit>().Action());
            GetComponent<PokemonEntryManager>().UpdateUi();
            GetComponent<BattleUiManager>().UpdateUi();
            yield return new WaitForSeconds(5f);

            DialogueManager.Instance.StartDialogue(new List<string> { PlayerUnits[0].Data.Name + "��(��) ������ �ұ�?" });
            yield return new WaitUntil(() => isPlayerActioned);
            GetComponent<PokemonEntryManager>().UpdateUi();
            GetComponent<BattleUiManager>().UpdateUi();
            yield return new WaitForSeconds(5f);

        }
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
        }
    }

    public void OnUnitDied(List<Unit> targetList)
{
    // 🔹 1) 적 팀에서 누군가 죽었을 때, 살아 있는 적이 1마리면 마지막 BGM으로 교체
    if (targetList == EnemyUnits)   // 지금 죽은 유닛이 적 팀일 때만 체크
    {
        int aliveCount = 0;
        foreach (var u in EnemyUnits)
        {
            if (u != null && !u.isDead)
                aliveCount++;
        }

        if (aliveCount == 1)   // 적 포켓몬이 1마리만 남았으면
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayLastBattleBgm();
            }
        }
    }

    // 🔹 2) 원래 있던 순서 정리 + 교체 로직
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