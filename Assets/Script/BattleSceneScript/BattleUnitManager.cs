using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitManager : MonoBehaviour
{
    public List<UnitData> PlayerUnitData = new();
    public List<UnitData> EnemyUnitData = new();

    void Awake()
    {
        DontDestroyOnLoad(this); 
    }
}