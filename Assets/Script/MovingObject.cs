using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingObject : MonoBehaviour
{
    protected static List<MovingObject> allMovingObjects = new();

    protected float stepDistance = 1f;  // 이동 칸(1칸씩)
    protected float moveSpeed = 4f;     // 이동 속도
    protected bool isMoving = false;

    protected Rigidbody2D rb;
    protected Animator animator;

    protected virtual void Awake()
    {
        // 기본 컴포넌트 세팅
        if (GetComponent<BoxCollider2D>() == null)
            gameObject.AddComponent<BoxCollider2D>();

        rb = GetComponent<Rigidbody2D>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.freezeRotation = true;

        animator = GetComponent<Animator>();

        // 기본값 지정(Idle_Down 상태로 시작)
        if (animator != null)
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", -1);
            animator.SetBool("IsMoving", false);
        }

        if (!allMovingObjects.Contains(this))
            allMovingObjects.Add(this);
    }

    public IEnumerator MoveStep(Vector2 direction)
    {
        isMoving = true;

        // 이동 애니메이션 적용
        if (animator != null)
        {
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
            animator.SetBool("IsMoving", true);
        }

        Vector2 startPos = rb.position;
        Vector2 targetPos = startPos + direction.normalized * stepDistance;

        // 다른 MovingObject가 있을 경우 충돌 처리
        foreach (var obj in allMovingObjects)
        {
            if (obj == this) continue;
            if (Vector2.Distance(obj.rb.position, targetPos) < 0.1f)
            {
                isMoving = false;
                if (animator != null) animator.SetBool("IsMoving", false);
                yield break;
            }
        }

        // Object 태그와 충돌하면 멈춤
        Collider2D hit = Physics2D.OverlapBox(targetPos, Vector2.one * 0.8f, 0f);
        if (hit != null && hit.CompareTag("Object"))
        {
            isMoving = false;
            if (animator != null) animator.SetBool("IsMoving", false);
            yield break;
        }

        if (hit != null && hit.CompareTag("NPC"))
        {
            isMoving = false;
            if (animator != null) animator.SetBool("IsMoving", false);
            yield break;
        }

        // 실제 이동 수행
        while ((targetPos - rb.position).sqrMagnitude > 0.001f)
        {
            Vector2 newPos = Vector2.MoveTowards(rb.position, targetPos, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            yield return new WaitForFixedUpdate();
        }

        rb.MovePosition(targetPos);

        if (animator != null)
            animator.SetBool("IsMoving", false);

        isMoving = false;
    }
}
