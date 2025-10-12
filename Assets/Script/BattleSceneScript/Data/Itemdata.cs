using UnityEngine;
public enum ItemCategory { Tools, Recovery, Pokeballs, TMs }

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public ItemCategory Category;
}