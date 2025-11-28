using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource bgmSource;
    [SerializeField] private AudioClip fieldBGM;
    [SerializeField] private AudioClip trainerBattle;
    [SerializeField] private AudioClip victoryTrainer;
    [SerializeField] private AudioClip wildBattle;
    [SerializeField] private AudioClip victoryWild;

    private AudioSource sfxSource;
    [SerializeField] private AudioClip battleUseItem;
    [SerializeField] private AudioClip throwBall;
    [SerializeField] private AudioClip battleRun;
    [SerializeField] private AudioClip damageWeak;
    [SerializeField] private AudioClip damageNormal;
    [SerializeField] private AudioClip damageSuper;

    [SerializeField] private AudioClip doorEnter;
    [SerializeField] private AudioClip doorExit;  
    [SerializeField] private AudioClip menuOpen;
    [SerializeField] private AudioClip cursor;
    [SerializeField] private AudioClip exclaim;
    [SerializeField] private AudioClip grass;
    [SerializeField] private AudioClip bump;
    

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
        sfxSource.spatialBlend = 0f;
    }
    private void Start()
    {
        PlayBgm(fieldBGM);
    }

    public void PlayBgm(AudioClip clip)
    {
        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.Play();
    }
    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);  
    }
    public void PlayFieldBgm() => PlayBgm(fieldBGM);
    public void PlayTrainerBattle() => PlayBgm(trainerBattle);
    public void PlayVictoryTrainer() => PlayBgm(victoryTrainer);
    public void PlayWildBattle() => PlayBgm(wildBattle);
    public void PlayVictoryWild() => PlayBgm(victoryWild);

    public void PlayBattleUseItem() => PlaySfx(battleUseItem);
    public void PlayThrowBall() => PlaySfx(throwBall);
    public void PlayBattleRun() => PlaySfx(battleRun);
    public void PlayDamageWeak() => PlaySfx(damageWeak);
    public void PlayDamageNormal() => PlaySfx(damageNormal);
    public void PlayDamageSuper() => PlaySfx(damageSuper);
    public void PlayDoorEnter() => PlaySfx(doorEnter);
    public void PlayDoorExit() => PlaySfx(doorExit);
    public void PlayMenuOpen() => PlaySfx(menuOpen);
    public void PlayCursor() => PlaySfx(cursor);
    public void PlayExclaim() => PlaySfx(exclaim);
    public void PlayGrass() => PlaySfx(grass);
    public void PlayBump() => PlaySfx(bump);
}
