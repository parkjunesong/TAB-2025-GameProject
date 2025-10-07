using NUnit.Framework.Interfaces;
using System.Collections;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MovingObject : MonoBehaviour {


    public BoxCollider2D boxCollider;
    public LayerMask layerMask; // �浹������ ����� �Ұ����� ���̾ �������ִ� ��

    public float Speed;
    protected Vector3 vector; // �θ� �ڽ� ���踸 ���� ����
    public int WalkCount;
    protected int CurrentWalkCount;
    public Animator animator;

    protected bool npcCanMove = true;

    protected void Move(string _dir, int _frequency)
    {
        StartCoroutine(MoveCoroutine(_dir, _frequency));
    }

    IEnumerator MoveCoroutine(string _dir, int _frequency)
    {
        npcCanMove = false;
        vector.Set(0, 0, vector.z);
        switch (_dir)
        {
            case "UP":
                vector.y = 1f;
                break;
            case "DOWN":
                vector.y = -1f;
                break;
            case "RIGHT":
                vector.x = 1f;
                break;
            case "LEFT":
                vector.x = -1f;
                break;
        }
        
        animator.SetFloat("MoveX", vector.x);
        animator.SetFloat("MoveY", vector.y);

        while (true)
        {
            bool CheckColisionFlag = CheckColision();
            if (CheckColisionFlag)
            {
                animator.SetBool("IsMoving", false);
                yield return new WaitForSeconds(1f); // �÷��̾ �տ� ������ ����
            }
            else
            {
                break; // �÷��̾ ������ ������
            }
        }

        animator.SetBool("IsMoving", true);

        boxCollider.offset = new Vector2(vector.x * 0.01f * Speed * WalkCount, vector.y * 0.01f * Speed * WalkCount);

        while (CurrentWalkCount < WalkCount)
        {

            // PlayerManager�� �ִ� while ���� �����Դµ� run�� ���õȰͰ� if�� ����
            // if�� ���� ������ ���� case������ ������ x�� y ���� �ϳ��� 0�� �Ǳ⿡ �밢�� �̵��� �ȵǱ� ����
            transform.Translate(vector.x * Speed, vector.y * Speed, 0);
            CurrentWalkCount++;
            if (CurrentWalkCount == 12)
            {
                boxCollider.offset = Vector2.zero;
            }
            yield return new WaitForSeconds(0.01f);
        }
        CurrentWalkCount = 0;
        if(_frequency != 5)
        {
            animator.SetBool("IsMoving", false);
        }
        animator.SetBool("IsMoving", false);
        npcCanMove = true;
    }
    protected bool CheckColision()
    {
        RaycastHit2D hit;
        // A������ B������ ������
        // A�������� B������ ���� �������� �������
        // ������ B������ �����Ѵٸ� hit == Null
        // ���ع��� �����ٸ� hit == ���ع�

        Vector2 start = transform.position; // A����. ĳ������ ���� ��ġ ��
        Vector2 end = start + new Vector2(vector.x * Speed * WalkCount, vector.y * Speed * WalkCount); // B����. ĳ���Ͱ� �̵��ϰ��� �ϴ� ��ġ ��
                                                                                                       // vector.x = 1, speed = 2.4�� ������, walkcount�� 20���� ���������Ƿ� ��������� �츮�� �̵��ϰ��� �ߴ� ��ġ�� 48�ȼ�

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, layerMask);
        boxCollider.enabled = true;

        // hit���� ĳ���Ͱ� �ڽ��� ���� �ڽ��� �浹�ϰ� �ǹǷ� ��� �÷��̾��� boxCollider�� ���� �������� ��� �ٽ� �ڽ��� Ŵ

        if (hit.transform != null)
        {
            return true;
        }
        return false;
        // ���� �÷��̾� �տ� ��ü�� ������ break�� ���ԵǾ� �ִ� �ݺ������� ��������
    }
}
