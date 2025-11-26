using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource bgmSource;

    [SerializeField] private AudioClip fieldBgm;
    [SerializeField] private AudioClip battleBgm;
    [SerializeField] private AudioClip lastBattleBgm;

    private void Awake()
    {
        // 싱글톤
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);   // 🔴 씬 넘어가도 안 죽게

        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
        }

        bgmSource.loop = true;
        bgmSource.playOnAwake = false;   // 🔴 여기서 자동 재생 막기
    }

    private void PlayBgm(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("[AudioManager] PlayBgm: clip 이 null.");
            return;
        }

        if (bgmSource == null)
        {
            Debug.LogError("[AudioManager] bgmSource 가 없음.");
            return;
        }

        // 디버그용 로그
        Debug.Log($"[AudioManager] BGM 변경: {bgmSource.clip?.name} -> {clip.name}");

        bgmSource.Stop();       
        bgmSource.clip = clip;   
        bgmSource.Play();        
    }

    public void PlayFieldBgm()      => PlayBgm(fieldBgm);
    public void PlayBattleBgm()     => PlayBgm(battleBgm);
    public void PlayLastBattleBgm() => PlayBgm(lastBattleBgm);
}
