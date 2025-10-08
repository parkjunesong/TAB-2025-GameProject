using UnityEngine;
using System.Collections;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    public GameObject UiScreen;
    public GameObject MainButtons;
    public GameObject FightButtons;
    public GameObject PokemonButtons;
    private float hoverTimer;
    [Header("Field UI Refs")]
    [SerializeField] private Canvas canvas;              
    [SerializeField] private RectTransform contextMenu;   
    [SerializeField] private RectTransform bagPanel;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        if (contextMenu != null) contextMenu.gameObject.SetActive(false);
        
        if (bagPanel != null) bagPanel.gameObject.SetActive(false);
    }

    void Update()
    {
        if (UiScreen == null) return; 
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
        if (UiScreen != null) UiScreen.SetActive(isActive);
    }
    public void MainButton()
    {
        if (MainButtons != null)   MainButtons.SetActive(true);
        if (FightButtons != null)  FightButtons.SetActive(false);
        if (PokemonButtons != null) PokemonButtons.SetActive(false);
    }
    public void FightButton()
    {
        if (MainButtons != null)  MainButtons.SetActive(false);
        if (FightButtons != null) FightButtons.SetActive(true);
    }
    public void BagButton()   { Debug.Log("b"); }
    public void RunButton()   { Debug.Log("r"); }
    public void PokemonButton()
    {
        if (MainButtons != null)     MainButtons.SetActive(false);
        if (PokemonButtons != null)  PokemonButtons.SetActive(true);
    }

    public void ShowContextMenuAt(Vector2 screenPos)
    {
        if (contextMenu == null || canvas == null) return;
        PositionAtScreen(contextMenu, screenPos);
        contextMenu.gameObject.SetActive(true);
        if (bagPanel != null) bagPanel.gameObject.SetActive(false);
    }

    public void HideContextMenuIfOpen()
    {
        if (contextMenu != null) contextMenu.gameObject.SetActive(false);
    }

    public void OpenBag()
    {
        if (contextMenu != null) contextMenu.gameObject.SetActive(false);
        if (bagPanel != null)    bagPanel.gameObject.SetActive(true);
    }

    public void CloseBag()
    {
        if (bagPanel != null) bagPanel.gameObject.SetActive(false);
    }

    private void PositionAtScreen(RectTransform rt, Vector2 screenPos)
    {
        RectTransform canvasRect = (RectTransform)canvas.transform;

        Camera cam = null;
        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            if (canvas.worldCamera != null) cam = canvas.worldCamera;
            else cam = Camera.main;
        }

        Vector2 anchored;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            cam,
            out anchored
        );


        anchored.x += 8f;
        anchored.y += 12f;

        Vector2 half = rt.sizeDelta * 0.5f;
        Vector2 min = canvasRect.rect.min + half;
        Vector2 max = canvasRect.rect.max - half;

        if (anchored.x < min.x) anchored.x = min.x;
        if (anchored.x > max.x) anchored.x = max.x;
        if (anchored.y < min.y) anchored.y = min.y;
        if (anchored.y > max.y) anchored.y = max.y;

        rt.anchoredPosition = anchored;
    }
}