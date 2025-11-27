using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    // -------- BGM --------
    private AudioSource bgmSource;
    public List<AudioClip> BgmList = new();

    // -------- SFX --------
    [Header("SFX")]
    [SerializeField] private AudioSource sfxSource;   // Inspector에서 안 넣으면 Awake에서 자동 추가
    public List<AudioClip> SFXList = new();           // 쓰면 되고, 안 써도 됨

    [SerializeField] private AudioClip hitSfx;
    [SerializeField] private AudioClip selectSfx;

    [SerializeField] private AudioClip levelUpSfx;
    [SerializeField] private AudioClip healSfx;
    [SerializeField] private AudioClip catchSfx;
    [SerializeField] private AudioClip itemGetSfx;
    [SerializeField] private AudioClip lowHpSfx;
    [SerializeField] private AudioClip battleWinSfx;
    [SerializeField] private AudioClip doorOpenSfx;   
    [SerializeField] private AudioClip walkSfx;       
    [SerializeField] private AudioClip grassWalkSfx;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);

        // BGM 소스 준비
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;

        // SFX 소스 준비
        if (sfxSource == null)
            sfxSource = gameObject.AddComponent<AudioSource>();

        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
        sfxSource.spatialBlend = 0f; // 2D 사운드
    }

    private void Start()
    {
        PlayBgm(0);   // 처음 시작 BGM (필요 없으면 지워도 됨)
    }

    // -------- BGM 재생 --------
    public void PlayBgm(int no)
    {
        if (no < 0 || no >= BgmList.Count) return;

        bgmSource.Stop();
        bgmSource.clip = BgmList[no];
        bgmSource.Play();
    }

    // -------- SFX 재생 --------
    public void PlaySfx(AudioClip clip)
{
    if (clip == null || sfxSource == null) return;

    Debug.Log("[AudioManager] SFX 재생: " + clip.name);
    sfxSource.PlayOneShot(clip);  

}


    public void PlayHitSfx()        => PlaySfx(hitSfx);
    public void PlaySelectSfx()     => PlaySfx(selectSfx);

    public void PlayLevelUpSfx()    => PlaySfx(levelUpSfx);
    public void PlayHealSfx()       => PlaySfx(healSfx);
    public void PlayCatchSfx()      => PlaySfx(catchSfx);
    public void PlayItemGetSfx()    => PlaySfx(itemGetSfx);
    public void PlayLowHpSfx()      => PlaySfx(lowHpSfx);
    public void PlayBattleWinSfx()  => PlaySfx(battleWinSfx);
    public void PlayDoorOpenSfx()   => PlaySfx(doorOpenSfx);
    public void PlayWalkSfx()       => PlaySfx(walkSfx);
    public void PlayGrassWalkSfx()  => PlaySfx(grassWalkSfx);
}
