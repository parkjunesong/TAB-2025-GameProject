using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type { None, Normal, Fire, Water, Grass, Electric, Ice, Fighting, Poison, Ground, Flying, Psychic, Bug, Rock, Ghost, Dragon, Dark, Steel, Fairy };

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Object/UnitData")]
public class UnitData : ScriptableObject
{
    public string Name;
    public Type Type;
    public int AT, SP, HP, DF;
    public Sprite Front, Back;
    public List<Skill_Base> SkillList = new();
}
