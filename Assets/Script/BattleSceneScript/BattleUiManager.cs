using UnityEngine;
using UnityEngine.UI;

public class BattleUiManager : MonoBehaviour
{
    public static BattleUiManager Instance;

    public Transform PlayerHpbar;
    public Transform EnemyHpbar;
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
        if (BattleManager.Instance.isPlayerActioned) return;

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
        PlayerHpbar.gameObject.SetActive(!isActive);
        EnemyHpbar.gameObject.SetActive(!isActive);
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
    public void UpdateUi()
    {
        var content = PlayerHpbar.GetChild(0);
        content.GetChild(2).GetComponent<Text>().text = BattleManager.Instance.PlayerUnits[0].Data.Name;
        content.GetChild(3).GetComponent<Text>().text = "Lv. " + BattleManager.Instance.PlayerUnits[0].Status.Level;
        content.GetChild(4).GetComponent<Text>().text = BattleManager.Instance.PlayerUnits[0].Status.HP + " / " + BattleManager.Instance.PlayerUnits[0].Status.maxHP;
        content.GetChild(0).GetChild(1).GetComponent<Slider>().value = (float)BattleManager.Instance.PlayerUnits[0].Status.HP / BattleManager.Instance.PlayerUnits[0].Status.maxHP;

        content = EnemyHpbar.GetChild(0);
        content.GetChild(1).GetComponent<Text>().text = BattleManager.Instance.EnemyUnits[0].Data.Name;
        content.GetChild(2).GetComponent<Text>().text = "Lv. " + BattleManager.Instance.EnemyUnits[0].Status.Level;
        content.GetChild(0).GetChild(1).GetComponent<Slider>().value = (float)BattleManager.Instance.EnemyUnits[0].Status.HP / BattleManager.Instance.EnemyUnits[0].Status.maxHP;
    }
}
