using System;
using UnityEngine;

/// <summary>
/// �������� �������� �� ��������������� ������� ������, ������ � �������� �������� ����
/// </summary>
public class AudioManager : MonoBehaviour
{
    #region Serialize fields

    [SerializeField] private AudioManagerConfig _config;

    [Header("Audio sources")]
    [SerializeField] private AudioSource _ambientSource;
    [SerializeField] private AudioSource _musicSource, _mainMenuMusicSource, _sfxSource;

    #endregion Serialize fields

    #region Mono

    private void Awake()
    {
        AddListeners();
    }

    #endregion Mono

    #region Private methods

    private void AddListeners()
    {
        GameSceneEventManager.OnGameMapStarded.AddListener(EventHandler_GameMapStarted);
        GameSceneEventManager.OnGamePause.AddListener(PauseAllAudioSource);
        WaveEventManager.OnPreparingForWave.AddListener(EventHandler_PrepareForWave);
        WaveEventManager.OnWaveIsComing.AddListener(EventHandler_WaveIsComing);
        WaveEventManager.OnWaveIsOver.AddListener(EventHandler_WaveIsOver);
        GlobalGameEventManager.OnGameOver.AddListener(StopAllSoundSource);
        GameSceneEventManager.OnSceneLoadingStarted.AddListener(StopAllSoundSource);
    }

    /// <summary>
    /// ����� ��� ��������� �������� ����� ��� ������������� ���� �����
    /// </summary>
    private void StopSound(SoundType soundType)
    {
        // �������� �������� ����� ��� ����� ������������� ����, ������� ����� ����������
        AudioSource audioSource = GetAudioSource(soundType);

        if (audioSource == null)
        {
            Debug.Log("Sound source for this sound type is not assigned!");
            return;
        }

        // ������������� ������������ ��������� �����
        StopPlayAudioSource(audioSource);
    }

    /// <summary>
    /// ����� ���������� �������� ����� ��� ���� �����, ������� ����� ������������
    /// </summary>
    /// <param name="soundType">��� �����</param>
    /// <returns>�������� ��� ������� ���� �����</returns>
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
                return _mainMenuMusicSource;
            default:
                return null;
        }
    }

    /// <summary>
    /// ������������� ��������� ��� ��������� �����
    /// </summary>
    /// <param name="audioSource">�������� �����, ������� ����� �����������</param>
    /// <param name="sound">������ ������ �����, �� �������� ����� ��������� ��� ���������</param>
    private void SetAudioSourceParameters(AudioSource audioSource, Sound sound)
    {
        audioSource.clip = sound.audioClip;
        audioSource.volume = sound.volume;
        audioSource.loop = sound.isLoop;
    }

    /// <summary>
    /// ��������� ������������ ��������� ����� � ���������
    /// </summary>
    /// <param name="audioSource">�������� �����, ������� ����� ���������</param>
    private void StartPlayAudioSource(AudioSource audioSource, float delay)
    {
        audioSource.PlayDelayed(delay);
    }

    /// <summary>
    /// ���������� ������������ ��������� �����
    /// </summary>
    /// <param name="audioSource">�������� �����, ������� ����� �������������</param>
    private void StopPlayAudioSource(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    /// <summary>
    /// ����� ��� ���������� ����� ��� ������ � ����� ���������� �����
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
            if (_mainMenuMusicSource.isPlaying)
                _mainMenuMusicSource.Pause();
            if (_sfxSource.isPlaying)
                _sfxSource.Pause();
        }
        else
        {
            _ambientSource.UnPause();
            _musicSource.UnPause();
            _mainMenuMusicSource.UnPause();
            _sfxSource.UnPause();
        }
       
    }

    #endregion Private methods

    #region Public methods

    /// <summary>
    /// ����� ��� ��������� ���� ���������� ����� AudioManager-�
    /// </summary>
    public void StopAllSoundSource()
    {
        _ambientSource.Stop();
        _musicSource.Stop();
        _mainMenuMusicSource.Stop();
        _sfxSource.Stop();
    }

    /// <summary>
    /// ����� ��� ��������������� ���������� ������ �� ������� _musicSounds
    /// </summary>
    /// <param name="nameAmbient">�������� �����, ������� ������� �� ������� � �������������</param>
    public void PlayRandomSound(SoundType soundType, float delay = 0)
    {
        // �������� ������ ������ ������������� ����
        Sound[] sounds = Array.FindAll(_config.Sounds, x => x.soundType == soundType);

        if (sounds == null)
        {
            Debug.Log("This type of sound was not found!");
            return;
        }

        // �������� ��������� ���� �� �������
        int randSound = UnityEngine.Random.Range(0, sounds.Length);
        Sound sound = sounds[randSound];

        // �������� �������� ����� ��� ����� ������������� ����
        AudioSource audioSource = GetAudioSource(soundType);

        if (audioSource == null)
        {
            Debug.Log("Sound source for this sound type " + soundType.ToString() + " is not assigned!");
            return;
        }

        // ������������� ��������� ��� ��������� �����
        SetAudioSourceParameters(audioSource, sound);

        // ����������� ����
        StartPlayAudioSource(audioSource, delay);
    }

    /// <summary>
    /// ����� ��� ��������������� ���������� ������� ������ �� ������� _ambientSounds
    /// </summary>
    /// <param name="nameAmbient">�������� �����, ������� ������� �� ������� � �������������</param>
    public void PlaySound(SoundType soundType, string nameSound, float delay = 0)
    {
        // �������� ���� �� ������� � ������������ ��������� � �����
        Sound sound = Array.Find(_config.Sounds, x => x.soundType == soundType && x.name == nameSound);

        if (sound == null)
        {
            Debug.Log("Sound not found!");
            return;
        }

        // �������� �������� ����� ��� ����� ������������� ����
        AudioSource audioSource = GetAudioSource(soundType);

        if (audioSource == null)
        {
            Debug.Log("Sound source for this sound type is not assigned!");
            return;
        }

        // ������������� ��������� ��� ��������� �����
        SetAudioSourceParameters(audioSource, sound);

        // ����������� ����
        StartPlayAudioSource(audioSource, delay);
    }

    #endregion Public methods

    #region Event handlers
    private void EventHandler_GameMapStarted()
    {
        PlaySound(SoundType.Sfx, "new_message", 5);
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

    private void EventHandler_WaveIsOver(int wave)
    {
        PlaySound(SoundType.Sfx, "wave_is_over", 0);
        StopSound(SoundType.CombatMusic);
    }
    #endregion
}
