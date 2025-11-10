using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public ItemCategory Category;
    public string Text;
}