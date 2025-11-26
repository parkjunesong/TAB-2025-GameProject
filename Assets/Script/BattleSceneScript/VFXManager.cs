using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }
    public void HitVFX(GameObject vfx, Vector2 position)
    {
        if (vfx == null) return;

        var hit = Instantiate(vfx, position, Quaternion.identity);
        Destroy(hit, 1f);
    }
}
