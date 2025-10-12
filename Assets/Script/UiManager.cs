using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI; 

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
    public GameObject ItemIconPrefab;   // 64x64 같은 Image 프리팹
    public Sprite DefaultBallIcon;
    public GameObject TMsContent;

    
    
    private float hoverTimer;


    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;


    }

    public void CloseToMain()
    {
        if (UiScreen) UiScreen.SetActive(true);
        if (MainButtons) MainButtons.SetActive(true);
        if (BagButtons) BagButtons.SetActive(false);
        if (PokemonButtons) PokemonButtons.SetActive(false);
        if (FightButtons) FightButtons.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseToMain();
        }
    }
    public void CloseBagPanel()
    {
        if (BagButtons) BagButtons.SetActive(false);
        if (MainButtons) MainButtons.SetActive(true);
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
        RefreshPokeballsView();
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
    private Transform GetScrollContent(GameObject scrollRoot)
    {
        if (scrollRoot == null) return null;
        var viewport = scrollRoot.transform.Find("Viewport");
        if (viewport == null) return null;
        var content = viewport.Find("Content");
        return content;
    }
    private void RefreshPokeballsView()
    {
        if (ItemIconPrefab == null) return;
        if (InventoryManager.Instance == null) return;

        // PokeballsContent는 GameObject(스크롤 뷰 루트)
        Transform parent = GetScrollContent(PokeballsContent);
        if (parent == null) return;

        // 1) 기존 아이콘 제거
        for (int i = parent.childCount - 1; i >= 0; i--)
            Destroy(parent.GetChild(i).gameObject);

        // 2) 총 개수와 아이콘 선택
        int total = 0;
        Sprite icon = null;

        foreach (var slot in InventoryManager.Instance.Inventory)
        {
            if (slot != null && slot.item != null &&
                slot.item.Category == ItemCategory.Pokeballs)
            {
                if (slot.quantity > 0) total += slot.quantity;
                if (icon == null && slot.item.Icon != null) icon = slot.item.Icon;
            }
        }
        if (icon == null) icon = DefaultBallIcon;

        // 3) total 개수만큼 생성
        for (int i = 0; i < total; i++)
        {
            var go = Instantiate(ItemIconPrefab, parent);
            var img = go.GetComponent<UnityEngine.UI.Image>();
            if (img != null) img.sprite = icon;
        }
    }

    

}