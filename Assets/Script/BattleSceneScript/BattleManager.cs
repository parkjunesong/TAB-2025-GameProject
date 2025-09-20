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
    public Unit FrontPlayerUnit; // 플레이어 선봉 포켓몬
    public Unit FrontEnemyUnit; // 적 선봉 포켓몬

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;     
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
        FrontEnemyUnit = EnemyUnits[0];

        // FrontPlayerUnit, FrontEnemyUnit의 이미지를 화면에 표시하는 작업 필요
        // FrontPlayerUnit.Data.BackSprite, FrontEnemyUnit.Data.FrontSprite로 접근 가능




        TurnStart();
    }

    public void TurnStart()
    {
        FrontPlayerUnit.TurnStart();
        FrontEnemyUnit.TurnStart();

        ActionStart();
    }
    public void ActionStart()
    {
        // 선봉 포켓몬 스피드 비교해서 행동 순서 배정
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