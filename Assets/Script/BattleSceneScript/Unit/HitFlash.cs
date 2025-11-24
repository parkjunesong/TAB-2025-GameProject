using System.Collections;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer; // 깜빡일 스프라이트
    [SerializeField] private float totalDuration = 0.3f;    // 전체 깜빡이는 시간
    [SerializeField] private float interval = 0.05f;        // 깜빡이는 간격

    private Coroutine flashRoutine;

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

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        while (elapsed < totalDuration)
        {
            visible = !visible;
            spriteRenderer.enabled = visible;

            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }

        spriteRenderer.enabled = true; // 마지막에는 보이게
        flashRoutine = null;
    }
}
