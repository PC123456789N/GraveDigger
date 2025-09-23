using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour
{
    [SerializeField] private PlayerController Player;

    [SerializeField] private AudioSource walkingAudio;
    [SerializeField] private AudioSource GunShotAudio;
    [SerializeField] private AudioSource GunCockAudio;
    [SerializeField] private AudioSource GunReloadAudio;
    [SerializeField] private AudioSource EnemyScreamAudio;
    [SerializeField] private AudioSource PlayerGruntAudio;
    [SerializeField] private AudioSource OpenMedicAudio;
    [SerializeField] private AudioSource InjectionAudio;
    [SerializeField] private AudioSource IronDoorAudio;
    [SerializeField] private AudioSource BulletsVFXAudio;
    [SerializeField] private AudioSource Bell1Audio;
    [SerializeField] private AudioSource Bells3Audio;

    private bool alreadyPlaying;
    // Start is called before the first frame update
    void Start()
    {
        alreadyPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.walking)
        {
            PlayAudio("Walking");
        }
        else
        {
            StopAudio("Walking");
        }
    }

    void PlayAudio(string audioname)
    {
        if (audioname == "Walking" && !alreadyPlaying)
        {
            walkingAudio.Play();
            alreadyPlaying = true;
        }
    }

    void StopAudio(string audioname)
    {
        if (audioname == "Walking" && alreadyPlaying)
        {
            walkingAudio.Stop();
            alreadyPlaying = false;
        }
    }

    public void PlayAudioShot()
    {
        GunShotAudio.PlayOneShot(GunShotAudio.clip, 1f);
    }

    public void PlayCockShot()
    {
        GunCockAudio.PlayOneShot(GunCockAudio.clip, 1f);
    }

    public void PlayReloadShot()
    {
        GunReloadAudio.PlayOneShot(GunReloadAudio.clip, 1f);
    }
    public void PlayEnemyScream()
    {
        EnemyScreamAudio.PlayOneShot(EnemyScreamAudio.clip, 1f);
    }
    public void PlayPlayerScream()
    {
        PlayerGruntAudio.PlayOneShot(PlayerGruntAudio.clip, 1f);
    }
    public void PlayMedicOpen()
    {
        OpenMedicAudio.PlayOneShot(OpenMedicAudio.clip, 1f);
    }
    public void PlayInjectionAudio()
    {
        InjectionAudio.PlayOneShot(InjectionAudio.clip, 1f);
    }
    public void PlayIronDoorAudio()
    {
        IronDoorAudio.PlayOneShot(IronDoorAudio.clip, 1f);
    }
    public void PlayBulletsVFXAudio()
    {
        BulletsVFXAudio.PlayOneShot(BulletsVFXAudio.clip, 1f);
    }
    public void Play1BellAudio()
    {
        Bell1Audio.PlayOneShot(Bell1Audio.clip, 1f);
    }
    public void Play3BellsAudio()
    {
        Bells3Audio.PlayOneShot(Bells3Audio.clip, 1f);
    }

    
}
