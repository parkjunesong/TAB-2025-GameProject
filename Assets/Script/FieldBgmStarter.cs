using UnityEngine;

public class FieldBgmStarter : MonoBehaviour
{
    void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayFieldBgm();
        }
    }
}

