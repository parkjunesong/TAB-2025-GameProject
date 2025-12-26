using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                content.GetChild(1).GetComponent<Image>().sprite = BattleManager.Instance.PlayerUnits[i].Data.PartyIcon;
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

    public async void ChangePlayerUnit(int index)
    {
        if(index >= BattleManager.Instance.PlayerUnits.Count) return;

        Unit targetUnit = BattleManager.Instance.PlayerUnits[index];
        if (targetUnit.isDead || targetUnit == BattleManager.Instance.PlayerUnits[0]) return;

        DialogueManager.Instance.StartDialogue(new List<string> { BattleManager.Instance.PlayerUnits[0].Data.Name + ", 돌아와!" });
        BattleManager.Instance.PlayerUnits[0].gameObject.SetActive(false);
        BattleManager.Instance.PlayerUnits[index] = BattleManager.Instance.PlayerUnits[0];
        BattleManager.Instance.PlayerUnits[0] = targetUnit;
        BattleManager.Instance.PlayerUnits[0].gameObject.SetActive(true);

        await Task.Delay(3000);
        DialogueManager.Instance.StartDialogue(new List<string> { "가라, " + BattleManager.Instance.PlayerUnits[0].Data.Name + "!" });

        UpdateUi();
        GetComponent<SkillManager>().UpdateUi();

        BattleUiManager.Instance.ChangeUiScreenActiveState(false);
        BattleManager.Instance.isPlayerActioned = true;
    }
}