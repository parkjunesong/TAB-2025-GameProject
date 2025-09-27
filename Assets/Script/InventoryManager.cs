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
    public int maxSlots = 20;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        DontDestroyOnLoad(this);
    }

    private Predicate<InventorySlot> FindItem(ItemData targetItem)
    {
        return delegate (InventorySlot slot) { return slot.item == targetItem; };
    }

    public void AddItem(ItemData newItem)
    {
        var slot = Inventory.Find(FindItem(newItem)); //아이템이 존재하는 슬롯

        if (slot != null) //이미 아이템이 존재하면
        {
            slot.quantity += 1; 
        }
        else if (Inventory.Count < maxSlots) //새 아이템이면
        {
            Inventory.Add(new InventorySlot { item = newItem, quantity = 1 });
        }
        else return; // 인벤토리 가득 참
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