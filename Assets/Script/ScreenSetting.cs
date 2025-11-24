using UnityEngine;

public class ScreenSetting : MonoBehaviour
{
    void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
    }
    private void Start()
    {
        ScreenFader.Instance.StartCoroutine(ScreenFader.Instance.FadeOutIn());
    }
}
