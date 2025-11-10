using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemCategory { Pokeballs, Recovery }

public class BagData
{
    public ItemCategory Category;
    public string Name;

    public BagData(ItemCategory category, string name)
    {
        Category = category;
        Name = name;
    }
}

public class BagManager : MonoBehaviour
{
    public int Index;
    public GameObject itemSlotPrefab;
    private BagData[] Bag = new BagData[2];

    void Awake()
    {
        Index = 0;
        Bag[0] = new BagData(ItemCategory.Pokeballs, "몬스터볼");
        Bag[1] = new BagData(ItemCategory.Recovery, "회복");
        DontDestroyOnLoad(this);
    }

    public void ViewItems()
    {
        var content = UiManager.Instance.BagButtons.transform.GetChild(0);
        var invenSlot = content.GetChild(2).GetChild(1).GetChild(0);
        var invenList = InventoryManager.Instance.FindItemsByCategory((ItemCategory)Index);

        content.GetChild(1).GetChild(2).GetComponent<Text>().text = Bag[Index].Name;
        content.GetChild(3).GetChild(0).GetComponent<Text>().text = invenList[0].item.Text;

        // 기존 슬롯 제거
        foreach (Transform child in invenSlot) Destroy(child.gameObject);
        foreach (var item in invenList)
        {
            GameObject slotObj = Instantiate(itemSlotPrefab, invenSlot);
            slotObj.transform.GetChild(0).GetComponent<Image>().sprite = item.item.Icon;
            slotObj.transform.GetChild(1).GetComponent<Text>().text = item.item.Name;
            slotObj.transform.GetChild(2).GetComponent<Text>().text = "X " + item.quantity;
            slotObj.SetActive(true);
        }         
    }
}