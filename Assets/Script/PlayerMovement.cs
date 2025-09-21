using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetBool("IsMoving", movement.sqrMagnitude > 0);
    }

    void FixedUpdate()
    {
        if (movement.sqrMagnitude > 0)
        {
            rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
        }
    }
}
