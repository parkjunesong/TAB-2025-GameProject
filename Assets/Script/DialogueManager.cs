using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject TextPanel;
    private Text TextField;
    private int index;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        TextField = TextPanel.GetComponentInChildren<Text>();
    }

    public void StartDialogue(List<string> Dialogue)
    {
        index = 0;
        TextPanel.SetActive(true);    
        StartCoroutine(StartTalk(Dialogue));
    }
    IEnumerator StartTalk(List<string> Dialogue)
    {
        TextField.text = "";
        yield return StartCoroutine(Typing(Dialogue[index]));
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));

        if (index < Dialogue.Count - 1)
        {
            index++;
            StartCoroutine(StartTalk(Dialogue));
        }
        else TextPanel.SetActive(false);
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