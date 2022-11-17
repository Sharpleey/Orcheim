using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource; 

    public ManagerStatus Status { get; private set; }

    private void Awake()
    {
        Messenger<MusicSound>.AddListener(GlobalGameEvent.PLAY_AMBIENT, PlayMusic);
        Messenger.AddListener(GlobalGameEvent.GAME_OVER, StopMusic);
    }

    private void OnDestroy()
    {
        Messenger<MusicSound>.RemoveListener(GlobalGameEvent.PLAY_AMBIENT, PlayMusic);
        Messenger.RemoveListener(GlobalGameEvent.GAME_OVER, StopMusic);
    }

    public void Startup()
    {
        Debug.Log("Audio manager starting...");

        // any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
        Status = ManagerStatus.Started;
    }

    private void PlayMusic(MusicSound nameMusic)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == nameMusic);

        if (sound == null)
        {
            Debug.Log("Music not found!");
            return;
        }

        musicSource.clip = sound.audioClip;
        musicSource.volume = sound.volume;
        musicSource.loop = sound.isLoop;
        musicSource.Play();
    }

    private void PlaySFX(MusicSound nameSound)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == nameSound);

        if (sound == null)
        {
            Debug.Log("Sound not found!");
            return;
        }

        //musicSource.clip = sound.audioClip;
        musicSource.volume = sound.volume;
        //musicSource.loop = sound.isLoop;
        musicSource.PlayOneShot(sound.audioClip);
    }

    private void StopMusic()
    {
        musicSource.Stop();
    }


    //private void PlayAudioClip(Sound[] sounds, AudioSource audioSource, string nameAudio)
    //{
    //    Sound sound = Array.Find(sounds, x => x.name == nameAudio);

    //    if (sound == null)
    //    {
    //        Debug.Log("Sound not found!");
    //        return;
    //    }

    //    audioSource.clip = sound.audioClip;
    //    musicSource.volume = sound.volume;
    //    musicSource.loop = sound.isLoop;
    //    musicSource.Play();
    //}


}
