using UnityEngine;

public class ItemInventory : MonoBehaviour
{
       public ItemData[] slots = new ItemData[30]; 

    public bool AddItem(ItemData newItem)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = newItem;
                Debug.Log(newItem.itemName + " 을(를) 인벤토리에 넣었습니다!");
                return true;
            }
        }
        Debug.Log("인벤토리가 가득 찼습니다!");
        return false;
    }

    public void RemoveItem(int index)
    {
        if (index >= 0 && index < slots.Length && slots[index] != null)
        {
            Debug.Log(slots[index].itemName + " 을(를) 인벤토리에서 제거했습니다!");
            slots[index] = null;
        }
    }
}
