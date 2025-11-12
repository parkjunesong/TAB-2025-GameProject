using UnityEngine;
using System.Collections;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    public GameObject UiScreen;
    public GameObject MainButtons;
    public GameObject FightButtons;
    public GameObject PokemonButtons;
    public GameObject BagButtons;
    private float hoverTimer;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Update()
    {
        if (Input.mousePosition.y <= Screen.height * 0.1f)
        {
            hoverTimer += Time.deltaTime;

            if (hoverTimer >= 0.5f && !UiScreen.activeSelf)
            {
                MainButton();
                ChangeUiScreenActiveState(true);
            }
        }
        else if (Input.mousePosition.y >= Screen.height * 0.9f)
        {
            hoverTimer += Time.deltaTime;

            if (hoverTimer >= 0.5f && UiScreen.activeSelf)
            {
                MainButton();
                ChangeUiScreenActiveState(false);
                DialogueManager.Instance.TextPanel.SetActive(true);
            }
        }
        else hoverTimer = 0f;
    }
    public void ChangeUiScreenActiveState(bool isActive)
    {
        UiScreen.SetActive(isActive);
    }
    public void MainButton()
    {
        MainButtons.SetActive(true);
        FightButtons.SetActive(false);
        PokemonButtons.SetActive(false);
        BagButtons.SetActive(false);
        DialogueManager.Instance.EndDialogue();

    }
    public void FightButton()
    {
        MainButtons.SetActive(false);
        FightButtons.SetActive(true);
    }
    public void BagButton()
    {
        MainButtons.SetActive(false);
        BagButtons.SetActive(true);
        GetComponent<BagManager>().ViewItems();
    }
    public void RunButton()
    {
        Debug.Log("r");
    }
    public void PokemonButton()
    {
        MainButtons.SetActive(false);
        PokemonButtons.SetActive(true);
    }
}