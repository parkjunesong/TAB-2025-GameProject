using UnityEngine;
public enum BagCategory { Tools, Recovery, Pokeballs, TMs }

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public BagCategory Category;
}