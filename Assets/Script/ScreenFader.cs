using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance;

    public Image fadeImage;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void HitFlash()
    {
        StartCoroutine(Flash(1));   
    }


    public IEnumerator BattleEncount()
    {
        StartCoroutine(Flash(3));
        yield return new WaitForSeconds(0.7f);
        StartCoroutine(Fade(1f, 1f));
    }

    public IEnumerator FadeOutIn()
    {
        yield return Fade(1f, 1f);
        yield return new WaitForSeconds(0.3f);
        yield return Fade(0f, 1f);
    }
    public IEnumerator Flash(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return Fade(1f, 0.1f);
            yield return Fade(0f, 0.1f);
        }
    }

    private IEnumerator Fade(float targetAlpha, float duration)
    {
        float startAlpha = fadeImage.color.a;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);

            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, targetAlpha);
    }       
}