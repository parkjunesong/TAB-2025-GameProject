using UnityEngine;
using System.Collections;

public class door : MonoBehaviour
{
    public float x = 65;
    public float y = -5;
    public float teleportDelay = 0.1f;
    private IEnumerator OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.CompareTag("PlayerMe"))
        {
            other.transform.position = new Vector2(x, y);
            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            pm.StopAllCoroutines();
            pm.canMove = false;
            Animator anim = other.GetComponent<Animator>();
            if(anim != null)
            {
                anim.SetBool("IsMoving", false);
            }
            yield return new WaitForSeconds(teleportDelay);
            
            pm.canMove = true;
            pm.StartCoroutine(pm.MoveStep(Vector2.zero));
            
        }
    }
}
