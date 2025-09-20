using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitManager : MonoBehaviour
{
    // Field Scene -> Battle Scene
    // 플레이어와 상대방의 포켓몬 파티 데이터 전달용 스크립트

    public List<UnitData> PlayerUnitData = new();
    public List<UnitData> EnemyUnitData = new();

    void Awake()
    {
        DontDestroyOnLoad(this); // 씬 전환 시 유지
    }
}