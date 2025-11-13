using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot
{
    public ItemData item; //인벤 슬롯 안의 아이템
    public int quantity; //아이템 수량
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<InventorySlot> Inventory = new();
    public List<ItemData> TestItems;
    public int maxSlots = 100;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        foreach (ItemData item in TestItems)
            AddItem(item);

        DontDestroyOnLoad(this);
    }

    private Predicate<InventorySlot> FindItem(ItemData targetItem)
    {
        return delegate (InventorySlot slot) { return slot.item == targetItem; };
    }
    public List<InventorySlot> FindItemsByCategory(ItemCategory category)
    {
        var list = Inventory.FindAll(slot => slot.item.Category == category);
        return list;
    }

    public void AddItem(ItemData newItem)
    {
        var slot = Inventory.Find(FindItem(newItem));

        // 이미 해당 아이템이 존재할 경우
        if (slot != null) slot.quantity += 1;
        // 아이템이 새로 추가된 경우
        else if (Inventory.Count < maxSlots) Inventory.Add(new InventorySlot { item = newItem, quantity = 1 });
        // 인벤토리 가득 참
        else return; 
    }

    public void RemoveItem(ItemData item)
    {
        var slot = Inventory.Find(FindItem(item)); //아이템이 존재하는 슬롯

        if (slot != null && slot.quantity >= 1) //아이템 수가 충분하면
        {
            slot.quantity -= 1;
            if (slot.quantity == 0) Inventory.Remove(slot);        
        }
    }
}