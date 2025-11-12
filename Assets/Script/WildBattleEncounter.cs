using UnityEngine;
using UnityEngine.SceneManagement;

public class WildBattleEncounter : MonoBehaviour
{
    [Header("전투 발생 확률 (0~1)")]
    [Range(0f, 1f)] public float encounterChance = 0.1f;  // 10% 확률

    [Header("전투씬 이름")]
    public string battleSceneName = "Battle Scene";

    private bool isInGrass = false;  // 플레이어가 수풀에 있는지 여부
    private float moveTimer = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerMe"))
        {
            Debug.Log("플레이어가 pocketgrass에 들어옴");
            isInGrass = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerMe"))
        {
            Debug.Log("플레이어가 pocketgrass에서 나감");
            isInGrass = false;
        }
    }

    private void Update()
    {
        // 플레이어가 수풀 위에 있고, 이동 중이라면
        if (isInGrass && PlayerIsMoving())
        {
            moveTimer += Time.deltaTime;

            // 1초마다 확률 계산
            if (moveTimer >= 1f)
            {
                moveTimer = 0f;

                if (Random.value < encounterChance)
                {
                    Debug.Log("야생 포켓몬 조우! 배틀 시작!");
                    // 전투 시작
                    SceneManager.LoadScene(battleSceneName);
                }
            }
        }
        else
        {
            moveTimer = 0f;
        }
    }

    private bool PlayerIsMoving()
    {
        // 키 입력으로 체크
        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
    }
}
