using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{   //��ų ��� ���� ó��
    public GameObject[] skillUi = new GameObject[4];

    public void UseSkillNo(int i, Unit caster) //��ư ui�� ���� ����
    {
        caster.Skill.UseSkillNo(i);
    }
}
