using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject TextPanel;
    private Text TextField;
    private int index;
    private float timer;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        TextField = TextPanel.GetComponentInChildren<Text>();
    }

    public void StartDialogue(List<string> Dialogue, bool always = true)
    {
        index = 0;
        TextPanel.SetActive(true);    
        StartCoroutine(StartTalk(Dialogue, always));
    }
    public void EndDialogue()
    {
        TextPanel.SetActive(false);
    }

    IEnumerator StartTalk(List<string> Dialogue, bool always)
    {
        timer = 0;
        TextField.text = "";
        yield return StartCoroutine(Typing(Dialogue[index]));

        if (always) yield break; 
        else yield return new WaitUntil(() =>
        {
            timer += Time.deltaTime;
            return Input.GetKeyDown(KeyCode.Mouse0) || timer >= 5f;
        });

        if (index < Dialogue.Count - 1)
        {
            index++;
            StartCoroutine(StartTalk(Dialogue, always));
        }
        else EndDialogue();
    }

    IEnumerator Typing(string text)
    {
        foreach (char letter in text.ToCharArray())
        {
            TextField.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }
}