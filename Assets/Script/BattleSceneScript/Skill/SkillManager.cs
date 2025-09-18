using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{   //스킬 사용 관련 처리
    public GameObject[] skillUi = new GameObject[4];

    void Awake()
    {
        foreach(Unit unit in BattleManager.Instance.allUnits())
            for (int i = 0; i < 4; i++)
                unit.Skill.assignedSkills[i].SetEffect();      
    }

    public void UseSkillNo(int i, Unit caster) //버튼 ui를 통해 접근
    {
        caster.Skill.UseSkillNo(i);
    }
}
