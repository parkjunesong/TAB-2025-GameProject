using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    [TextArea(2, 5)]
    public string[] dialogueLines;

    public float interactionDistance = 1.2f;
    private Transform player;
    private bool isTalking = false;

    private NPCMovement npcMove;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerMe").transform;
        npcMove = GetComponent<NPCMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isTalking) return;

        float dist = Vector2.Distance(player.position, transform.position);

        if (dist <= interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                StartDialogue();
            }
        }
    }

    void StartDialogue()
    {
        isTalking = true;

        if (npcMove != null)
            npcMove.StopMoving();

        FacePlayer();

        player.GetComponent<PlayerMovement>().canMove = false;

        DialogueManager.Instance.StartDialogue(
            new System.Collections.Generic.List<string>(dialogueLines),
            false
        );

        StartCoroutine(WaitForDialogueEnd());
    }

    void FacePlayer()
    {
        Vector2 dir = (player.position - transform.position);

        if (animator == null) return;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
            {
                animator.SetFloat("MoveX", 1);
                animator.SetFloat("MoveY", 0);
            }
            else
            {
                animator.SetFloat("MoveX", -1);
                animator.SetFloat("MoveY", 0);
            }
        }
        else
        {
            if (dir.y > 0)
            {
                animator.SetFloat("MoveX", 0);
                animator.SetFloat("MoveY", 1);
            }
            else
            {
                animator.SetFloat("MoveX", 0);
                animator.SetFloat("MoveY", -1);
            }
        }

        animator.SetBool("IsMoving", false);
    }

    System.Collections.IEnumerator WaitForDialogueEnd()
    {
        while (DialogueManager.Instance.TextPanel.activeSelf)
            yield return null;

        if (npcMove != null)
            npcMove.ResumeMoving();

        player.GetComponent<PlayerMovement>().canMove = true;

        isTalking = false;
    }
}
