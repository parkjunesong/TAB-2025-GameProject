using NUnit.Framework.Interfaces;
using System.Collections;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PlayerManager : MovingObject
{

    public float RunSpeed;
    private float ApplyRunSpeed;
    private bool ApplyRunFlag = false;

    private bool canMove = true;

    void Start()
    {
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
        animator.SetBool("IsMoving", false);
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                ApplyRunSpeed = RunSpeed;
                ApplyRunFlag = true;
            }
            else
            {
                ApplyRunSpeed = 0;
                ApplyRunFlag = false;
            }
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
            {
                vector.y = 0; // �ش� �ڵ� �κ��� ���� �ΰ��� Ű�� ���ÿ� �������� ��������Ʈ�� �̻��ϰ� �ٲ�� ���� ������!
            }

            animator.SetFloat("MoveX", vector.x);
            animator.SetFloat("MoveY", vector.y);

            bool CheckColisionFlag = base.CheckColision();
            if (CheckColisionFlag)
            {
                break; // �������� �ʰڴ�.
            }

            animator.SetBool("IsMoving", true);

            boxCollider.offset = new Vector2(vector.x * 0.01f * Speed * WalkCount, vector.y * 0.01f * Speed * WalkCount);

            while (CurrentWalkCount < WalkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (Speed + ApplyRunSpeed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (Speed + ApplyRunSpeed), 0);
                }
                if (ApplyRunFlag)
                {
                    CurrentWalkCount++;
                }
                CurrentWalkCount++;
                if (CurrentWalkCount == 12)
                {
                    boxCollider.offset = Vector2.zero;
                }
                yield return new WaitForSeconds(0.01f);
            }
            CurrentWalkCount = 0;

        }
        animator.SetBool("IsMoving", false);
        canMove = true;
    }
    void Update()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }

    }
}
