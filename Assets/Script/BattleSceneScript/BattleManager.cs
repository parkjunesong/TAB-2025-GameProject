using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public GameObject UnitPrefab;

    public List<Unit> PlayerUnits = new();
    public List<Unit> EnemyUnits = new();
    public Unit FrontPlayerUnit;
    public Unit FrontEnemyUnit;

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
        FrontPlayerUnit = PlayerUnits[0];
        FrontPlayerUnit.gameObject.SetActive(true);
        FrontEnemyUnit = EnemyUnits[0];
        FrontEnemyUnit.gameObject.SetActive(true);


        TurnStart();
    }

    public void TurnStart()
    {
        FrontPlayerUnit.TurnStart();
        FrontEnemyUnit.TurnStart();
        StartCoroutine(ActionStart());
    }
    IEnumerator ActionStart()
    {
        if (FrontPlayerUnit.Status.SP >= FrontEnemyUnit.Status.SP)
        {
            yield return StartCoroutine(FrontPlayerUnit.Action());
            yield return StartCoroutine(FrontEnemyUnit.Action());
        }
        else
        {
            yield return StartCoroutine(FrontEnemyUnit.Action());
            yield return StartCoroutine(FrontPlayerUnit.Action());
        }
        TurnEnd();
    }
    public void TurnEnd()
    {
        FrontPlayerUnit.TurnEnd();
        FrontEnemyUnit.TurnEnd();
        TurnStart();
    }  
     

    public List<Unit> allUnits()
    {
        List<Unit> units = new();
        foreach (Unit unit in PlayerUnits) units.Add(unit);
        foreach (Unit unit in EnemyUnits) units.Add(unit);
        return units;
    }
}