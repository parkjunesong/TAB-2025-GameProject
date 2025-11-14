using UnityEngine;
using System.Collections;

public class UiManager : MonoBehaviour
{
    public GameObject UiScreen;
    public GameObject MainButtons;
    public GameObject PokemonButtons;
    public GameObject BagButtons;
    private float hoverTimer;   

    void Update()
    {
        if (Input.mousePosition.y <= Screen.height * 0.1f)
        {
            hoverTimer += Time.deltaTime;

            if (hoverTimer >= 0.5f && !UiScreen.activeSelf)
            {
                MainButton();
                UiScreen.SetActive(true);
            }
        }
        else if (Input.mousePosition.y >= Screen.height * 0.9f)
        {
            hoverTimer += Time.deltaTime;

            if (hoverTimer >= 0.5f && UiScreen.activeSelf)
            {
                MainButton();
                UiScreen.SetActive(false);
            }
        }
        else hoverTimer = 0f;
    }
    public void MainButton()
    {
        MainButtons.SetActive(true);
        PokemonButtons.SetActive(false);
        BagButtons.SetActive(false);
    }
    public void BagButton()
    {
        MainButtons.SetActive(false);
        BagButtons.SetActive(true);
        GetComponent<BagManager>().ViewItems();
    }
    public void PokemonButton()
    {
        MainButtons.SetActive(false);
        PokemonButtons.SetActive(true);
    }
}