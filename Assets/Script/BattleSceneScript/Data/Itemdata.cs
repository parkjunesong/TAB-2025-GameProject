using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public string Name;
    public Sprite Icon;

    [Header("Battle")]
    public bool IsDamageBoostItem = false;   // 기술 위력 증가 아이템인가?
    public float DamageMultiplier = 1f;      // 생명의 구슬이면 1.3f

    public bool CausesHpRecoil = false;      // (HP 감소 아이템인지 – 준성이가 쓸 부분)
    public float HpRecoilRatio = 0f;         // 예: 0.1f = 최대 HP 10%
}
