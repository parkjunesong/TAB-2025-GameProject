using UnityEngine;
using System.Collections;
using UnityEditor;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    public GameObject UiScreen;
    public GameObject MainButtons;
    public GameObject FightButtons;
    public GameObject PokemonButtons;
    public GameObject BagButtons;
    public GameObject ToolsContent;     
    public GameObject RecoveryContent;  
    public GameObject PokeballsContent; 
    public GameObject TMsContent;
    
    private float hoverTimer;
    

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Update()
    {
        if (Input.mousePosition.y <= Screen.height * 0.2f)
        {
            hoverTimer += Time.deltaTime;

            if (hoverTimer >= 0.5f && !UiScreen.activeSelf)
            {
                MainButton();
                ChangeUiScreenActiveState(true);
            }
        }
        else if (Input.mousePosition.y >= Screen.height * 0.8f)
        {
            hoverTimer += Time.deltaTime;

            if (hoverTimer >= 0.5f && UiScreen.activeSelf)
            {
                MainButton();
                ChangeUiScreenActiveState(false);
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
    }
    public void ShowTools()
    {
        ToolsContent.SetActive(true);
    }

    public void ShowRecovery()
    {
        RecoveryContent.SetActive(true);
    }

    public void ShowPokeballs()
    {
        PokeballsContent.SetActive(true);
    }

    public void ShowTMs()
    {
        TMsContent.SetActive(true);
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