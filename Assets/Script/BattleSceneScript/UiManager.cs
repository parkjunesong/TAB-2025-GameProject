using UnityEngine;
using System.Collections;

public class UiManager : MonoBehaviour
{
    public GameObject UiScreen;
    private float hoverTimer;

    void Update()
    {
        if (Input.mousePosition.y <= Screen.height * 0.2f)
        {
            hoverTimer += Time.deltaTime;

            if (hoverTimer >= 0.5f && !UiScreen.activeSelf) UiScreen.SetActive(true);
        }
        else if (Input.mousePosition.y >= Screen.height * 0.8f)
        {
            hoverTimer += Time.deltaTime;

            if (hoverTimer >= 0.5f && UiScreen.activeSelf) UiScreen.SetActive(false);
        }
        else hoverTimer = 0f;      
    }   

    public void FightButton()
    {
        Debug.Log("f");
    }
    public void BagButton()
    {
        Debug.Log("b");
    }
    public void RunButton()
    {
        Debug.Log("r");
    }
    public void PokemonButton()
    {
        Debug.Log("p");
    }
}