using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource bgmSource;

    public List<AudioClip> BgmList = new();
    public List<AudioClip> SFXList = new();

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        DontDestroyOnLoad(gameObject);
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;
    }
    public void PlayBgm(int no)
    {
        bgmSource.Stop();       
        bgmSource.clip = BgmList[no];   
        bgmSource.Play();        
    }
}
