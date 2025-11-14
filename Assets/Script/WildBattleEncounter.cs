using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WildBattleEncounter : MonoBehaviour
{
    private IEnumerator Encounter()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0);

        if (Random.value < 0.1f) SceneManager.LoadScene("Battle Scene");
        StartCoroutine(Encounter());
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerMe")) StartCoroutine(Encounter());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerMe")) StopCoroutine(Encounter());    
    }   
}