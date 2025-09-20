using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitManager : MonoBehaviour
{
    // Field Scene -> Battle Scene
    // �÷��̾�� ������ ���ϸ� ��Ƽ ������ ���޿� ��ũ��Ʈ

    public List<UnitData> PlayerUnitData = new();
    public List<UnitData> EnemyUnitData = new();

    void Awake()
    {
        DontDestroyOnLoad(this); // �� ��ȯ �� ����
    }
}