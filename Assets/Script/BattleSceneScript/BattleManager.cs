using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

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
        BattleStart();  // 씬 시작 시 자동으로 배틀 시작
    }

    public void BattleStart()
    {
        // 1) 유닛 리스트 가져오기
        var bumGO = GameObject.Find("BattleUnitManager");
        if (bumGO == null) { Debug.LogError("BattleUnitManager 오브젝트를 찾을 수 없음"); return; }
        var BUM = bumGO.GetComponent<BattleUnitManager>();
        if (BUM == null) { Debug.LogError("BattleUnitManager 컴포넌트를 찾을 수 없음"); return; }

        PlayerUnits = BUM.PlayerUnits;
        EnemyUnits  = BUM.EnemyUnits;

        // 2) 리스트 비어있는지 체크
        if (PlayerUnits == null || PlayerUnits.Count == 0) { Debug.LogError("PlayerUnits 리스트가 비어있음"); return; }
        if (EnemyUnits  == null || EnemyUnits.Count  == 0) { Debug.LogError("EnemyUnits 리스트가 비어있음"); return; }

        // 3) 프론트 유닛 지정
        FrontPlayerUnit = PlayerUnits[0];
        FrontEnemyUnit  = EnemyUnits[0];

        // 4) 각 유닛 초기화(스프라이트/스탯 세팅)
        if (FrontPlayerUnit?.Data != null) FrontPlayerUnit.Init(FrontPlayerUnit.Data);
        if (FrontEnemyUnit?.Data  != null) FrontEnemyUnit.Init(FrontEnemyUnit.Data);

        // 5) 턴 시작
        TurnStart();
    }

    public void TurnStart()
    {
        FrontPlayerUnit?.TurnStart();
        FrontEnemyUnit?.TurnStart();
        ActionStart();
    }

    public void ActionStart()
    {
        // 행동 선택/처리 로직 (추가 예정)
    }

    public void TurnEnd()
    {
        FrontPlayerUnit?.TurnEnd();
        FrontEnemyUnit?.TurnEnd();
        TurnStart();
    }

    public List<Unit> allUnits()
    {
        var units = new List<Unit>();
        if (PlayerUnits != null) units.AddRange(PlayerUnits);
        if (EnemyUnits  != null) units.AddRange(EnemyUnits);
        return units;
    }
}
