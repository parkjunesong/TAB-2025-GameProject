using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill_Base : ScriptableObject
{
    public TypeData Skill_Type;
    public int Skill_MaxPP;
    public string Skill_Name;
    public Sprite Skill_Icon;
    public AudioSource Skill_Sound;
    protected List<Effect_Base> EffectList = new();

    public virtual bool IsAvailable(Unit caster) { return true; } // 특수 발동조건 체크
    public abstract void SetEffect();
    public abstract void Execute(Unit caster);
    public virtual void SkillMessage(Unit caster)
    {
        var textList = new List<string>();

        if (caster.Team == "Enemy")
            textList.Add("상대 " + caster.Data.Name + "의 \n" + Skill_Name + "!");
        else if(caster.Team == "Player")
            textList.Add(caster.Data.Name + "의 \n" + Skill_Name + "!");

        DialogueManager.Instance.StartDialogue(textList);
    }
}