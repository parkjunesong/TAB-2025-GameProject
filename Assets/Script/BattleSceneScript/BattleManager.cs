using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public GameObject UnitPrefab;

    public List<Unit> PlayerUnits = new();
    public List<Unit> EnemyUnits = new();

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;     
    }
    void Start()
    {
        BattleStart();
    }

    public void BattleStart()
    {
        BattleUnitManager BUM = GameObject.Find("BattleUnitManager").GetComponent<BattleUnitManager>();
        foreach (UnitData data in BUM.PlayerUnitData)
        {
            Unit unit = Instantiate(UnitPrefab).AddComponent<PlayerUnit>();
            unit.Init(data);
            PlayerUnits.Add(unit);
        }
        foreach (UnitData data in BUM.EnemyUnitData)
        {
            Unit unit = Instantiate(UnitPrefab).AddComponent<EnemyUnit>();
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
        //StartCoroutine(ActionStart());
    }
    IEnumerator ActionStart()
    {
        if (PlayerUnits[0].Status.SP >= EnemyUnits[0].Status.SP)
        {
            yield return StartCoroutine(PlayerUnits[0].Action());
            yield return StartCoroutine(EnemyUnits[0].Action());
        }
        else
        {
            yield return StartCoroutine(EnemyUnits[0].Action());
            yield return StartCoroutine(PlayerUnits[0].Action());
        }
        TurnEnd();
    }
    public void TurnEnd()
    {
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
    


    public void ChangeUnit(int index, List<Unit> targetList)
    {      
        Unit targetUnit = targetList[index];
        if (targetUnit.isDead || targetUnit == targetList[0]) return;

        targetList[index] = targetList[0];
        targetList[0] = targetUnit;
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