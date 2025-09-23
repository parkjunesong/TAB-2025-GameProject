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
    public Unit FrontPlayerUnit; // �÷��̾� ���� ���ϸ�
    public Unit FrontEnemyUnit; // �� ���� ���ϸ�

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;     
    }
    void Start()
    {
        BattleStart(); 
    }
    private Unit CreateAndRegisterUnit(UnitData data, bool isPlayer)
    {
        GameObject go = UnitPrefab != null
            ? Instantiate(UnitPrefab)
            : new GameObject((data != null ? data.Name : "Unit"));

        Unit unit = isPlayer ? go.AddComponent<PlayerUnit>() : go.AddComponent<EnemyUnit>();
        unit.Init(data);

        if (isPlayer) PlayerUnits.Add(unit);
        else          EnemyUnits.Add(unit);

        return unit;
    }

    public void BattleStart()
    {
        BattleUnitManager BUM = GameObject.Find("BattleUnitManager").GetComponent<BattleUnitManager>();
        foreach (UnitData data in BUM.PlayerUnitData)
        {
            Unit unit = Instantiate(UnitPrefab).AddComponent<PlayerUnit>();
            unit.Init(data);
            PlayerUnits.Add(unit);
            var sr = unit.GetComponent<SpriteRenderer>();
            if (sr != null && data != null)
            {
                if (data.BackSprite != null)
                    sr.sprite = data.BackSprite;
            }
        }
        foreach (UnitData data in BUM.EnemyUnitData)
        {
            Unit unit = Instantiate(UnitPrefab).AddComponent<EnemyUnit>();
            unit.Init(data);
            EnemyUnits.Add(unit);
            var sr = unit.GetComponent<SpriteRenderer>();
            if (sr != null && data != null)
            {
                if (data.FrontSprite != null)
                    sr.sprite = data.FrontSprite;
            }
        }
        FrontPlayerUnit = PlayerUnits[0];
        FrontEnemyUnit = EnemyUnits[0];

        // FrontPlayerUnit, FrontEnemyUnit�� �̹����� ȭ�鿡 ǥ���ϴ� �۾� �ʿ�
        // FrontPlayerUnit.Data.BackSprite, FrontEnemyUnit.Data.FrontSprite�� ���� ����




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
        // ���� ���ϸ� ���ǵ� ���ؼ� �ൿ ���� ����
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