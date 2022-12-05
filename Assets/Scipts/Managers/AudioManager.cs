using System;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour, IGameManager
{
    [SerializeField] private Sound[] _sounds;

    [SerializeField] private AudioSource _ambientSource, _musicSource, _sfxSource;

    public ManagerStatus Status { get; private set; }

    private void Awake()
    {
        GameSceneEventManager.OnGameMapStarded.AddListener(EventHandler_GameMapStarted);
        WaveEventManager.OnPreparingForWave.AddListener(EventHandler_PrepareForWave);
        WaveEventManager.OnWaveIsComing.AddListener(EventHandler_WaveIsComing);
        WaveEventManager.OnWaveIsOver.AddListener(EventHandler_WaveIsOver);
        GlobalGameEventManager.OnPauseGame.AddListener(PauseAllAudioSource);
        GlobalGameEventManager.OnGameOver.AddListener(StopAllSoundSource);
        GameSceneEventManager.OnSceneLoadingStarted.AddListener(StopAllSoundSource);
    }

    public void Startup()
    {
        Debug.Log("Audio manager starting...");

        // any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
        Status = ManagerStatus.Started;
    }

    /// <summary>
    /// ћетод дл€ воспроизведени€ конкретной фоновой музыки из массива _ambientSounds
    /// </summary>
    /// <param name="nameAmbient">Ќазвание звука, который выберем из массива и воспроизводем</param>
    public void PlaySound(SoundType soundType, string nameSound, float delay = 0)
    {
        // ѕолучаем звук из массива с определенным названием и типом
        Sound sound = Array.Find(_sounds, x => x.soundType == soundType && x.name == nameSound);

        if (sound == null)
        {
            Debug.Log("Sound not found!");
            return;
        }

        // ѕолучаем источник звука дл€ звука определенного типа
        AudioSource audioSource = GetAudioSource(soundType);

        if (audioSource == null)
        {
            Debug.Log("Sound source for this sound type is not assigned!");
            return;
        }

        // ”станавливаем параметры дл€ источника звука
        SetAudioSourceParameters(audioSource, sound);

        // ѕроигрываем звук
        StartPlayAudioSource(audioSource, delay);
    }

    /// <summary>
    /// ћетод дл€ воспроизведени€ конкретной музыки из массива _musicSounds
    /// </summary>
    /// <param name="nameAmbient">Ќазвание звука, который выберем из массива и воспроизводем</param>
    public void PlayRandomSound(SoundType soundType, float delay = 0)
    {
        // ѕолучаем массив звуков определенного типа
        Sound[] sounds = Array.FindAll(_sounds, x => x.soundType == soundType);

        if (sounds == null)
        {
            Debug.Log("This type of sound was not found!");
            return;
        }

        // ¬ыбираем случайный звук из массива
        int randSound = UnityEngine.Random.Range(0, sounds.Length);
        Sound sound = sounds[randSound];

        // ѕолучаем источник звука дл€ звука определенного типа
        AudioSource audioSource = GetAudioSource(soundType);

        if(audioSource == null)
        {
            Debug.Log("Sound source for this sound type is not assigned!");
            return;
        }

        // ”станавливаем параметры дл€ источника звука
        SetAudioSourceParameters(audioSource, sound);

        // ѕроигрываем звук
        StartPlayAudioSource(audioSource, delay);
    }

    /// <summary>
    /// ћетод дл€ остановки истоника звука дл€ определенного типа звука
    /// </summary>
    private void StopSound(SoundType soundType)
    {
        // ѕолучаем источник звука дл€ звука определенного типа, который хотим остановить
        AudioSource audioSource = GetAudioSource(soundType);

        if (audioSource == null)
        {
            Debug.Log("Sound source for this sound type is not assigned!");
            return;
        }

        // ќстанавливаем проигрывание источника звука
        StopPlayAudioSource(audioSource);
    }

    /// <summary>
    /// ћетод дл€ остановки всех источников звука AudioManager-а
    /// </summary>
    public void StopAllSoundSource()
    {
        _ambientSource.Stop();
        _musicSource.Stop();
        _sfxSource.Stop();
    }

    /// <summary>
    /// ћетод возвращает источник звука дл€ типа звука, который будем использовать
    /// </summary>
    /// <param name="soundType">“ип звука</param>
    /// <returns>»сточник дл€ данного типа звука</returns>
    private AudioSource GetAudioSource(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.AmbientMusic:
                return _ambientSource;
            case SoundType.CombatMusic:
                return _musicSource;
            case SoundType.Sfx:
                return _sfxSource;
            case SoundType.MainMenuTheme:
                return _musicSource;
            default:
                return null;
        }
    }

    /// <summary>
    /// ”станавливаем параметры дл€ источника звука
    /// </summary>
    /// <param name="audioSource">»сточник звука, который будем настраивать</param>
    /// <param name="sound">ќбъект класса звука, из которого берем параметры дл€ настройки</param>
    private void SetAudioSourceParameters(AudioSource audioSource, Sound sound)
    {
        audioSource.clip = sound.audioClip;
        audioSource.volume = sound.volume;
        audioSource.loop = sound.isLoop;
    }

    /// <summary>
    /// «апустить проигрывание источника звука
    /// </summary>
    /// <param name="audioSource">»сточник звука, который будем запускать</param>
    private void StartPlayAudioSource(AudioSource audioSource, float delay)
    {
        audioSource.PlayDelayed(delay);
    }

    /// <summary>
    /// ќстановить проигрывание источника звука
    /// </summary>
    /// <param name="audioSource">»сточник звука, который будем останавливать</param>
    private void StopPlayAudioSource(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    /// <summary>
    /// ћетод дл€ постановки паузы или сн€ти€ с паузы источников звука
    /// </summary>
    /// <param name="isPause"></param>
    private void PauseAllAudioSource(bool isPause)
    {
        if (isPause)
        {
            if(_ambientSource.isPlaying)
                _ambientSource.Pause();
            if (_musicSource.isPlaying)
                _musicSource.Pause();
            if (_sfxSource.isPlaying)
                _sfxSource.Pause();
        }
        else
        {
            _ambientSource.UnPause();
            _musicSource.UnPause();
            _sfxSource.UnPause();
        }
       
    }

    #region Event handlers
    private void EventHandler_GameMapStarted()
    {
        PlaySound(SoundType.Sfx, "new_message", 5);
        //PlayRandomSound(SoundType.AmbientMusic, 0);
    }

    private void EventHandler_PrepareForWave(int wave)
    {
        PlaySound(SoundType.Sfx, "new_message", 0);
    }

    private void EventHandler_WaveIsComing(int wave)
    {
        PlaySound(SoundType.Sfx, "alarm_horn", 0);
        PlayRandomSound(SoundType.CombatMusic, 8);
    }

    private void EventHandler_WaveIsOver()
    {
        PlaySound(SoundType.Sfx, "wave_is_over", 0);
        StopSound(SoundType.CombatMusic);
    }
    #endregion
}
