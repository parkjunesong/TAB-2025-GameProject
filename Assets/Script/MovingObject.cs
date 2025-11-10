using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingObject : MonoBehaviour
{
    protected static List<MovingObject> allMovingObjects = new();

    protected float stepDistance = 1f;  // 이동 거리
    protected float moveSpeed = 4f;     // 이동 속도
    protected bool isMoving = false;   // 이동 중 여부
    protected Rigidbody2D rb;
    //public Animator animator; 


    void Awake() // 씬 전환 후 세팅값 사라지는 문제
    {
        gameObject.tag = "MovingObject";
        gameObject.AddComponent<BoxCollider2D>();
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.freezeRotation = true;

        //animator.SetFloat("MoveX", 0);
        //animator.SetFloat("MoveY", -1);
        //animator.SetBool("IsMoving", false);

        if (!allMovingObjects.Contains(this)) allMovingObjects.Add(this);
    }

    public IEnumerator MoveStep(Vector2 direction)
    {
        isMoving = true;
        Vector2 startPos = rb.position;
        Vector2 targetPos = startPos + direction.normalized * stepDistance;

        // MovingObject가 목표 칸에 있음: 잠시 멈춤
        foreach (var obj in allMovingObjects)
        {
            if (obj == this) continue;
            if (Vector2.Distance(obj.rb.position, targetPos) < 0.1f) 
            {
                isMoving = false;
                yield return new WaitForSeconds(1f);
                yield break;
            }
        }
        // Object가 목표 칸에 있음: 이동 취소
        Collider2D hit = Physics2D.OverlapBox(targetPos, Vector2.one * 0.8f, 0f);
        if (hit != null && hit.CompareTag("Object"))
        {           
            isMoving = false;
            yield break;
        }

        while ((targetPos - rb.position).sqrMagnitude > 0.001f)
        {
            Vector2 newPos = Vector2.MoveTowards(rb.position, targetPos, moveSpeed * Time.fixedDeltaTime);          
            rb.MovePosition(newPos);
            yield return new WaitForFixedUpdate();
        }

        rb.MovePosition(targetPos); // 위치 보정
        isMoving = false;
    }
}
