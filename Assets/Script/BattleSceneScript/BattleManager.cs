using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public List<Unit> PlayerUnits = new();
    public List<Unit> EnemyUnits = new();

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void BattleStart()
    {
        BattleUnitManager BUM = GameObject.Find("BattleUnitManager").GetComponent<BattleUnitManager>();
        PlayerUnits = BUM.PlayerUnits;
        EnemyUnits = BUM.EnemyUnits;
        
        TurnStart();
    }

    public void TurnStart()
    {
        for (int i = 0; i < PlayerUnits.Count; i++)
        {
            PlayerUnits[i].TurnStart();
        }
        for (int i = 0; i < EnemyUnits.Count; i++)
        {
            EnemyUnits[i].TurnStart();
        }
        ActionStart();

    }
    public void ActionStart()
    {
        // 선봉 포켓몬 스피드 비교해서 행동 순서 배정
    }
    public void TurnEnd()
    {
        for (int i = 0; i < PlayerUnits.Count; i++)
        {
            PlayerUnits[i].TurnEnd();
        }
        for (int i = 0; i < EnemyUnits.Count; i++)
        {
            EnemyUnits[i].TurnEnd();
        }
        TurnStart();
    }   
}