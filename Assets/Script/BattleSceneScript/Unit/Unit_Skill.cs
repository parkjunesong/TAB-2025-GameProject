using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData
{
    public int PP;
    public Skill_Base Skill;

    public SkillData(Skill_Base skill)
    {
        Skill = skill;
        PP = Skill.Skill_MaxPP;
    }
}

public class Unit_Skill
{
    public Unit Caster;
    public List<Skill_Base> SkillList = new();
    public SkillData[] assignedSkills = new SkillData[4];

    public Unit_Skill(UnitData data, Unit caster)
    {
        SkillList = data.SkillList;
        Caster = caster;

        for(int i = 0; i < 4; i++)
        {
            assignSKillNo(i, i);
        }   
    }

    public void UseSkillNo(int i)
    {
        if (i < 0 || i > 3) return;
        if (assignedSkills[i].PP <= 0) return;

        assignedSkills[i].Skill.Execute(Caster);
        assignedSkills[i].PP--;
    }
    public void assignSKillNo(int i, int no)
    {
        if (i < 0 || i > 3) return;
        if (no > SkillList.Count) return;

        assignedSkills[i] = new SkillData(SkillList[no]);
        assignedSkills[i].Skill.SetEffect();
    }
}