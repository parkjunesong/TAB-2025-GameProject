using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitManager : MonoBehaviour
{
    // Field Scene -> Battle Scene
    // �÷��̾�� ������ ���ϸ� ��Ƽ ������ ���޿� ��ũ��Ʈ

    public List<Unit> PlayerUnits = new();
    public List<Unit> EnemyUnits = new();

    void Awake()
    {
        DontDestroyOnLoad(this); // �� ��ȯ �� ����
    }
}