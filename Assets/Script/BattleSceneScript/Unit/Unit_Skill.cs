using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Skill
{
    public Unit Caster;
    public List<Skill_Base> SkillList = new();
    public Skill_Base[] assignedSkills = new Skill_Base[4];

    public Unit_Skill(Unit caster)
    {
        Caster = caster;
    }

    public void UseSkillNo(int i)
    {
        if (i < 0 || i > 3) return;
        assignedSkills[i].Execute(Caster);
    }
    public void assignSKillNo(int i, int no)
    {
        if (i < 0 || i > 3) return;
        if (no > SkillList.Count) return;
        assignedSkills[i] = SkillList[no];
    }
}