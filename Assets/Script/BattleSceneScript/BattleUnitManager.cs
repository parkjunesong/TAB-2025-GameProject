using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitManager : MonoBehaviour
{
    // Field Scene -> Battle Scene
    // �÷��̾�� ������ ���ϸ� ��Ƽ ������ ���޿� ��ũ��Ʈ

    public List<UnitData> PlayerUnitData = new();
    public List<UnitData> EnemyUnitData = new();
    public int PlayerSelectIndex = 0;
    public int EnemySelectIndex  = 0;

    public UnitData GetPlayerSelected() =>
        (PlayerUnitData != null && PlayerUnitData.Count > PlayerSelectIndex)
            ? PlayerUnitData[PlayerSelectIndex] : null;

    public UnitData GetEnemySelected() =>
        (EnemyUnitData != null && EnemyUnitData.Count > EnemySelectIndex)
            ? EnemyUnitData[EnemySelectIndex] : null;

    void Awake()
    {
        DontDestroyOnLoad(this); // �� ��ȯ �� ����
    }
}