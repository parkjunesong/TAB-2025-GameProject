using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public ItemCategory Category;
    public string Text;

    [Header("Battle")]
    public bool IsDamageBoostItem = false;   
    public float DamageMultiplier = 1f;      // 생명의 구슬이면 1.3f

    public bool CausesHpRecoil = false; 
    public float HpRecoilRatio = 0f;         // 예: 0.1f = 최대 HP 10%
}