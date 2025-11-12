using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum DialogueEndCase { mouse, timer, always }

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

    public void StartDialogue(List<string> Dialogue, DialogueEndCase EndCase)
    {
        index = 0;
        TextPanel.SetActive(true);    
        StartCoroutine(StartTalk(Dialogue, EndCase));
    }
    public void EndDialogue()
    {
        TextPanel.SetActive(false);
    }

    IEnumerator StartTalk(List<string> Dialogue, DialogueEndCase EndCase)
    {
        timer = 0;
        TextField.text = "";
        yield return StartCoroutine(Typing(Dialogue[index]));
        switch (EndCase)
        {
            case DialogueEndCase.mouse:
                {
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
                    break;
                }
            case DialogueEndCase.timer:
                {
                    yield return new WaitUntil(() =>
                    {
                        timer += Time.deltaTime;
                        return Input.GetKeyDown(KeyCode.Mouse0) || timer >= 10f;
                    });
                    break;
                }
            case DialogueEndCase.always:
                {
                    yield break;
                }
        }

        if (index < Dialogue.Count - 1)
        {
            index++;
            StartCoroutine(StartTalk(Dialogue, EndCase));
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