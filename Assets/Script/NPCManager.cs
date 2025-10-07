using System.Collections;
using UnityEngine;

[System.Serializable]
public class NPCMove
{
    public bool NPCmove;

    public string[] direction; // npc�� ������ ���� ����

    [Range(1, 5)]
    [Tooltip("1 = õõ��, 2 = ���� õõ��, 3 = ����, 4 = ������, 5 = ����������")] 
    public int frequency; // npc�� ������ �������� �󸶳� ���� �ӵ��� ������ ���ΰ�.
}

public class NPCManager : MovingObject
{
    [SerializeField]
    public NPCMove npc;
    void Start()
    {
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
        animator.SetBool("IsMoving", false);
        StartCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {
        if(npc.direction.Length != 0)
        {
            for (int i = 0; i < npc.direction.Length; i++)
            {
                switch (npc.frequency)
                {
                    case 1:
                        yield return new WaitForSeconds(4f);
                        break;
                    case 2:
                        yield return new WaitForSeconds(3f);
                        break;
                    case 3:
                        yield return new WaitForSeconds(2f);
                        break;
                    case 4:
                        yield return new WaitForSeconds(1f);
                        break;
                    case 5:
                        break;
                }

                yield return new WaitUntil(() => npcCanMove);
                base.Move(npc.direction[i], npc.frequency);
                // �������� �̵� ����

                if (i == npc.direction.Length - 1)
                {
                    i = -1;
                }
            }
        }
    }
}
