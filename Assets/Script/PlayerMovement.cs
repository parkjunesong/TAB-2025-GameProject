using UnityEngine;
using System.Collections;

public class PlayerMovement : MovingObject
{
    public bool canMove = true;
    private bool isInGrass = false;


    protected override void Awake()
    {
        base.Awake(); // �θ�(MovingObject)�� Awake ����
        gameObject.tag = "PlayerMe";
    }
    void Update()
    {
        if (!canMove) return;
        if (isMoving) return; // �̵� �߿� �Է� ����

        Vector2 input = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) input = Vector2.up;
        else if (Input.GetKey(KeyCode.S)) input = Vector2.down;
        else if (Input.GetKey(KeyCode.A)) input = Vector2.left;
        else if (Input.GetKey(KeyCode.D)) input = Vector2.right;

        if (input != Vector2.zero) StartCoroutine(MoveStep(input));
        if (input != Vector2.zero)
        {
            // ⭐ 한 칸 움직이기 직전에, 지금 수풀 안이면 풀 소리 재생
            if (isInGrass && AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayGrass();
            }

            StartCoroutine(MoveStep(input));
        }
    }
     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Grass"))
        {
            isInGrass = true;

            // 첫 칸 진입 소리도 내고 싶으면 여기에도 한 번 더:
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayGrass();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Grass"))
        {
            isInGrass = false;
        }
    }
}
