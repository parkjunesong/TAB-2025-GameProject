using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }
    public void HitVFX(GameObject vfx, Unit target)
    {
        if (vfx == null) return;

        var hit = Instantiate(vfx, target.transform.position, Quaternion.identity);
        Destroy(hit, 1f);
    }
    public void HitBlink(Unit target)
    {
        StartCoroutine(blink(target.transform.GetComponent<SpriteRenderer>()));
    }
    IEnumerator blink(SpriteRenderer renderer)
    {
        for (int i = 0; i < 3; i++)
        {
            renderer.color = new Color(0, 0, 0, 0.5f);
            yield return new WaitForSeconds(0.05f);

            renderer.color = new Color(1, 1, 1, 0f);
            yield return new WaitForSeconds(0.05f);

            renderer.color = new Color(1, 1, 1, 1f);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
