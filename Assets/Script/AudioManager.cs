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
    [SerializeField] private AudioClip useItem;
    [SerializeField] private AudioClip throwBall;
    [SerializeField] private AudioClip battleRun;
    [SerializeField] private AudioClip damageWeak;
    [SerializeField] private AudioClip damageNormal;
    [SerializeField] private AudioClip damageSuper;

    [SerializeField] private AudioClip doorExit;
    [SerializeField] private AudioClip menuOpen;
    [SerializeField] private AudioClip cursor;
    [SerializeField] private AudioClip exclaim;
    [SerializeField] private AudioClip grass;

    [SerializeField] private AudioClip DIALGA;
    [SerializeField] private AudioClip GARCHOMP;
    [SerializeField] private AudioClip PACHIRISU;
    [SerializeField] private AudioClip PIKACHU;


    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;
        bgmSource.volume = 0.8f;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
        sfxSource.spatialBlend = 0f;
        sfxSource.volume = 0.8f;
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

    public void PlayUseItem() => PlaySfx(useItem);
    public void PlayThrowBall() => PlaySfx(throwBall);
    public void PlayBattleRun() => PlaySfx(battleRun);
    public void PlayDamageWeak() => PlaySfx(damageWeak);
    public void PlayDamageNormal() => PlaySfx(damageNormal);
    public void PlayDamageSuper() => PlaySfx(damageSuper);
    public void PlayDoorExit() => PlaySfx(doorExit);
    public void PlayMenuOpen() => PlaySfx(menuOpen);
    public void PlayCursor() => PlaySfx(cursor);
    public void PlayExclaim() => PlaySfx(exclaim);
    public void PlayGrass() => PlaySfx(grass);

    public void PlayPokemon(string name)
    {
        if (name == "디아루가") PlaySfx(DIALGA);
        if (name == "한카리아스") PlaySfx(GARCHOMP);
        if (name == "파치리스") PlaySfx(PACHIRISU);
        if (name == "피카츄") PlaySfx(PIKACHU);
    }
}
