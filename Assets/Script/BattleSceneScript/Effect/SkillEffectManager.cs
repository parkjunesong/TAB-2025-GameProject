using UnityEngine;

public class SkillEffectManager : MonoBehaviour
{
    public static SkillEffectManager Instance;

    [Header("기본 타격 이펙트 프리팹")]
    [SerializeField] private GameObject hitEffectPrefab;   // FireEffect1 같은 거 넣을 자리

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // 위치만 받아서 타격 이펙트 재생
   public void PlayHitEffect(Vector3 position)
{
    if (hitEffectPrefab == null) return;

    GameObject effect = Instantiate(
        hitEffectPrefab,
        position,
        Quaternion.identity
    );

    // 1초 뒤에 자동 삭제
    Destroy(effect, 1f);
}
    // 필요하면, 다른 이펙트를 직접 지정해서 쓸 수도 있음
    public void PlayHitEffect(GameObject effectPrefab, Vector3 position)
    {
        if (effectPrefab == null) return;

        Instantiate(effectPrefab, position, Quaternion.identity);
    }
}
