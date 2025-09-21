using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveTime = 2f;   
    public float waitTime = 2f;  

    private float moveCounter;
    private float waitCounter;
    private Vector2 moveDirection;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        waitCounter = waitTime;
        moveCounter = moveTime;
        ChooseDirection();
    }

    void Update()
    {
        if (moveCounter > 0)
        {
            moveCounter -= Time.deltaTime;
            rb.linearVelocity = moveDirection * moveSpeed;

            if (moveCounter <= 0)
            {
                waitCounter = waitTime;
            }
        }
        else if (waitCounter > 0)
        {
            waitCounter -= Time.deltaTime;
            rb.linearVelocity = Vector2.zero;

            if (waitCounter <= 0)
            {
                moveCounter = moveTime;
                ChooseDirection();
            }
        }
    }

    void ChooseDirection()
    {
        int dir = Random.Range(0, 4);
        switch (dir)
        {
            case 0: moveDirection = Vector2.up; break;
            case 1: moveDirection = Vector2.down; break;
            case 2: moveDirection = Vector2.left; break;
            case 3: moveDirection = Vector2.right; break;
        }
    }
}
