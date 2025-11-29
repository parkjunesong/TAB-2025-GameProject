using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public GameObject[] skillUi = new GameObject[4];

    public void UpdateUi()
    {
        for (int i = 0; i < 4; i++)
        {
            SkillData skill = BattleManager.Instance.PlayerUnits[0].Skill.assignedSkills[i];
            Transform content = skillUi[i].transform.GetChild(0).GetChild(0);
            content.parent.GetComponent<Image>().color = skill.Skill.Skill_Type.Color;
            content.GetChild(1).GetComponent<Text>().text = skill.Skill.Skill_Name;
            content.GetChild(2).GetComponent<Text>().text = skill.PP + "/" + skill.Skill.Skill_MaxPP;
            content.GetChild(3).GetComponent<Image>().color = skill.Skill.Skill_Type.Color;
            content.GetChild(3).GetComponentInChildren<Text>().text = skill.Skill.Skill_Type.Name;
        }
    }

    public void UseSkillNo(int i)
    {
        BattleManager.Instance.PlayerUnits[0].Skill.UseSkillNo(i);
        BattleUiManager.Instance.ChangeUiScreenActiveState(false);

        BattleManager.Instance.isPlayerActioned = true;
    }  
}
