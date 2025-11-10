using UnityEngine;
using UnityEngine.UI;

public class BattleUiManager : MonoBehaviour
{
    public Transform PlayerHpbar;
    public Transform EnemyHpbar;
    public GameObject Uiscreen;

    void Update()
    {
        if (Uiscreen.activeSelf)
        {
            PlayerHpbar.gameObject.SetActive(false);
            EnemyHpbar.gameObject.SetActive(false);
        }
        else
        {
            PlayerHpbar.gameObject.SetActive(true);
            EnemyHpbar.gameObject.SetActive(true);
        }
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
