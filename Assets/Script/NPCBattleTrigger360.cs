using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NPCBattleTrigger360 : MonoBehaviour
{
    public enum FacingDirection { Up, Right, Down, Left }
    public FacingDirection facing = FacingDirection.Down;

    [Header("시야 설정")]
    public float visionDistance = 2.8f;
    public LayerMask visionMask;
    public string battleSceneName = "Battle Scene";

    private bool hasTriggered = false;
    private BoxCollider2D npcCollider;
    private Animator animator;

    void Start()
    {
        npcCollider = GetComponent<BoxCollider2D>();
        if (npcCollider == null)
            npcCollider = gameObject.AddComponent<BoxCollider2D>();

        animator = GetComponent<Animator>();

        // 기본 방향 적용
        ApplyFacingToAnimator();

        if (visionMask == 0)
            visionMask = Physics2D.DefaultRaycastLayers;

        //2초마다 방향 바꾸는 코루틴 실행
        StartCoroutine(FaceCycleRoutine());
    }

    void Update()
    {
        if (hasTriggered) return;

        Vector2 dir = DirectionToVector(facing);

        // Ray 시작지점 보정
        Vector2 start = (Vector2)transform.position + dir * (npcCollider.size.y / 2f + 0.1f);

        RaycastHit2D hit = Physics2D.Raycast(start, dir, visionDistance, visionMask);
        Debug.DrawRay(start, dir * visionDistance, hit.collider ? Color.red : Color.blue);

        if (hit.collider != null && hit.collider.CompareTag("PlayerMe"))
        {
            hasTriggered = true;
            hit.collider.GetComponent<PlayerMovement>().canMove = false;
            StartCoroutine(StartBattle());
        }
    }

    // 2초에 한 번 방향을 바꾸는 루틴
    IEnumerator FaceCycleRoutine()
    {
        while (!hasTriggered)
        {
            yield return new WaitForSeconds(2f);

            // Facing 방향을 순환
            facing = facing switch
            {
                FacingDirection.Up => FacingDirection.Right,
                FacingDirection.Right => FacingDirection.Down,
                FacingDirection.Down => FacingDirection.Left,
                FacingDirection.Left => FacingDirection.Up,
                _ => FacingDirection.Up
            };

            ApplyFacingToAnimator();
        }
    }

    //애니메이터 방향 전환
    private void ApplyFacingToAnimator()
    {
        if (animator == null) return;

        switch (facing)
        {
            case FacingDirection.Up:
                animator.SetFloat("MoveX", 0);
                animator.SetFloat("MoveY", 1);
                break;

            case FacingDirection.Right:
                animator.SetFloat("MoveX", 1);
                animator.SetFloat("MoveY", 0);
                break;

            case FacingDirection.Down:
                animator.SetFloat("MoveX", 0);
                animator.SetFloat("MoveY", -1);
                break;

            case FacingDirection.Left:
                animator.SetFloat("MoveX", -1);
                animator.SetFloat("MoveY", 0);
                break;
        }

        animator.SetBool("IsMoving", false);  // Idle 유지
    }

    private Vector2 DirectionToVector(FacingDirection dir)
    {
        return dir switch
        {
            FacingDirection.Up => Vector2.up,
            FacingDirection.Down => Vector2.down,
            FacingDirection.Left => Vector2.left,
            FacingDirection.Right => Vector2.right,
            _ => Vector2.zero,
        };
    }

    private IEnumerator StartBattle()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(battleSceneName);
    }
}
