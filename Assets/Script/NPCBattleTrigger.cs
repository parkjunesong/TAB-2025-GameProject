using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NPCBattleTrigger : MonoBehaviour
{
    public enum FacingDirection { Up, Down, Left, Right }
    public FacingDirection facing = FacingDirection.Right;

    [Header("시야 설정")]
    public float visionDistance = 2.8f;        // 레이 길이
    public LayerMask visionMask;             // 감지할 대상 (Player만 포함)
    public string battleSceneName = "Battle Scene";

    private bool hasTriggered = false;
    private BoxCollider2D npcCollider;

    void Start()
    {
        npcCollider = GetComponent<BoxCollider2D>();
        if (npcCollider == null)
            npcCollider = gameObject.AddComponent<BoxCollider2D>();

        // visionMask가 비어 있다면 Everything으로 임시 설정
        if (visionMask == 0)
            visionMask = Physics2D.DefaultRaycastLayers;
    }

    void Update()
    {
        if (hasTriggered) return;

        Vector2 dir = DirectionToVector(facing);

        // Ray 시작점을 NPC Collider 바로 앞쪽으로 보정
        Vector2 start = (Vector2)transform.position + dir * (npcCollider.size.y / 2f + 0.1f);

        // Raycast 수행 (NPC 자신은 무시)
        RaycastHit2D hit = Physics2D.Raycast(start, dir, visionDistance, visionMask);

        // 시각화용 선 (빨간 = 감지됨, 파란 = 없음)
        Color rayColor = (hit.collider != null) ? Color.red : Color.blue;
        Debug.DrawRay(start, dir * visionDistance, rayColor);

        if (hit.collider != null)
        {
            // Player 감지 시 전투
            if (hit.collider.CompareTag("PlayerMe"))
            {
                hasTriggered = true;
                hit.collider.GetComponent<PlayerMovement>().canMove = false; // 해당 부분을 통하여 배틀에 걸렸을때 플레이어가 움직이지 못하도록 함
                StartCoroutine(StartBattle());
            }
        }
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

    private IEnumerator StartBattle() // 화면 암전효과 추가 예정
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(battleSceneName);
    }
}
