using UnityEngine;
using UnityEngine.UI;

public class PokemonEntryManager : MonoBehaviour
{
    public GameObject[] pokemonUi = new GameObject[6];

    public void UpdateUi()
    {
        for (int i = 0; i < 6; i++)
        {
            Transform content = pokemonUi[i].transform.GetChild(1);

            if (i < BattleManager.Instance.PlayerUnits.Count)
            {
                content.GetChild(1).GetComponent<Image>().sprite = BattleManager.Instance.PlayerUnits[i].Data.FrontSprite;
                content.GetChild(2).GetComponent<Text>().text = BattleManager.Instance.PlayerUnits[i].Data.Name;
                content.GetChild(3).GetComponent<Text>().text = "Lv. " + BattleManager.Instance.PlayerUnits[i].Status.Level;
                content.GetChild(4).GetComponent<Text>().text =
                   BattleManager.Instance.PlayerUnits[i].Status.HP + " / " + BattleManager.Instance.PlayerUnits[i].Status.maxHP;
                content.GetChild(0).GetChild(1).GetComponent<Slider>().value =
                    (float)BattleManager.Instance.PlayerUnits[i].Status.HP / BattleManager.Instance.PlayerUnits[i].Status.maxHP;

            }
            else content.gameObject.SetActive(false);
        }
    }

    public void ChangePlayerUnit(int index)
    {
        if(index >= BattleManager.Instance.PlayerUnits.Count) return;

        Unit targetUnit = BattleManager.Instance.PlayerUnits[index];
        if (targetUnit.isDead || targetUnit == BattleManager.Instance.PlayerUnits[0]) return;

        BattleManager.Instance.PlayerUnits[0].gameObject.SetActive(false);
        BattleManager.Instance.PlayerUnits[index] = BattleManager.Instance.PlayerUnits[0];
        BattleManager.Instance.PlayerUnits[0] = targetUnit;
        BattleManager.Instance.PlayerUnits[0].gameObject.SetActive(true);

        UpdateUi();
        GetComponent<SkillManager>().UpdateUi();

        BattleUiManager.Instance.ChangeUiScreenActiveState(false);
        BattleManager.Instance.isPlayerActioned = true;
    }
}