using UnityEngine;
using System.Collections;

public class door : MonoBehaviour
{
    public float x = 65;
    public float y = -5;
    private IEnumerator OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("PlayerMe"))
        {
            other.GetComponent<Animator>().SetBool("IsMoving", false);
            var pm = other.GetComponent<PlayerMovement>();       
            pm.StopAllCoroutines();
            pm.canMove = false;

            ScreenFader.Instance.StartCoroutine(ScreenFader.Instance.FadeOutIn());
            yield return new WaitForSeconds(1f);

            other.transform.position = new Vector2(x, y);
            yield return new WaitForSeconds(1.5f);                 
            pm.canMove = true;
            pm.StartCoroutine(pm.MoveStep(Vector2.zero)); 
            if (AudioManager.Instance != null)
            AudioManager.Instance.PlayDoorOpenSfx();     
        }
    }
}
