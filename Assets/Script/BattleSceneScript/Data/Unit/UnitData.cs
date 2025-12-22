using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : MonoBehaviour
{
    public string Name;
    public Type Type;
    public int _AT, _SP, _HP, _DF;
    public Sprite PartyIcon;
    public RuntimeAnimatorController FrontAnimator;
    public RuntimeAnimatorController BackAnimator;
    public List<Skill_Base> SkillList = new();
    public ItemData Item;

}
