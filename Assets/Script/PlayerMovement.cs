using UnityEngine;
using System.Collections;

public class PlayerMovement : MovingObject
{
    public bool canMove = true;

    protected override void Awake()
    {
        base.Awake(); // 부모(MovingObject)의 Awake 실행
        gameObject.tag = "PlayerMe";
    }
    void Update()
    {
        if (!canMove) return;
        if (isMoving) return; // 이동 중엔 입력 무시

        Vector2 input = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) input = Vector2.up;
        else if (Input.GetKey(KeyCode.S)) input = Vector2.down;
        else if (Input.GetKey(KeyCode.A)) input = Vector2.left;
        else if (Input.GetKey(KeyCode.D)) input = Vector2.right;

        if (input != Vector2.zero) StartCoroutine(MoveStep(input));
    }   
}
