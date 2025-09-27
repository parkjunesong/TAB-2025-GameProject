using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Object/UnitData")]
public class UnitData : ScriptableObject
{
    public string Name;
    public Type Type;
    public int _AT, _SP, _HP, _DF;
    public Sprite FrontSprite, BackSprite;
    public List<Skill_Base> SkillList = new();
}
