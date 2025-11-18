using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemCategory { Pokeballs, Recovery, Tools }

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
    private BagData[] Bag = new BagData[3];

    void Awake()
    {
        Index = 0;
        Bag[0] = new BagData(ItemCategory.Pokeballs, "몬스터볼");
        Bag[1] = new BagData(ItemCategory.Recovery, "회복");
        Bag[2] = new BagData(ItemCategory.Recovery, "도구");
    }

    public void ViewItems()
    {
        var content = GameObject.Find("BagButtons").transform.GetChild(0);
        var invenSlot = content.GetChild(2).GetChild(1).GetChild(0);
        var invenList = InventoryManager.Instance.FindItemsByCategory((ItemCategory)Index);

        content.GetChild(1).GetChild(0).GetComponent<Text>().text = Bag[Index].Name;
        content.GetChild(3).GetChild(0).GetComponent<Text>().text = invenList[0].item.Text;

        // 기존 슬롯 제거
        foreach (Transform child in invenSlot) Destroy(child.gameObject);
        for(int i = 0; i< invenList.Count; i++)
        {
            GameObject slotObj = Instantiate(itemSlotPrefab, invenSlot);
            slotObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 450 - 100 * i, 0);
            slotObj.transform.GetChild(0).GetComponent<Image>().sprite = invenList[i].item.Icon;
            slotObj.transform.GetChild(1).GetComponent<Text>().text = invenList[i].item.Name;
            slotObj.transform.GetChild(2).GetComponent<Text>().text = "X " + invenList[i].quantity;
            slotObj.SetActive(true);
        }       
    }
}