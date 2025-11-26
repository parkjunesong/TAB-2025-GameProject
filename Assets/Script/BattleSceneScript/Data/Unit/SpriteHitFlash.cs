using System.Collections;
using UnityEngine;

public class SpriteHitFlash : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float totalDuration = 0.3f;
    [SerializeField] private float interval = 0.05f;

    private Coroutine flashRoutine;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PlayFlash()
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        float elapsed = 0f;
        bool visible = true;

        while (elapsed < totalDuration)
        {
            visible = !visible;
            spriteRenderer.enabled = visible;

            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }
        spriteRenderer.enabled = true;
        flashRoutine = null;
    }
}

