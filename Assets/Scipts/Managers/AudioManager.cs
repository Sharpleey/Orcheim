using System;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    [SerializeField] private Sound[] _sounds;

    [SerializeField] private AudioSource _ambientSource, _musicSource, _sfxSource;

    public ManagerStatus Status { get; private set; }

    public static class Event
    {
        #region Events for manager
        /// <summary>
        /// �������� ��������������� ������������ ������/����� ������������� ����
        /// </summary>
        public const string PLAY_SOUND = "PLAY_SOUND";

        /// <summary>
        /// �������� ��������������� ��������� ������/����� ������������� ����
        /// </summary>
        public const string PLAY_RANDOM_SOUND = "PLAY_RANDOM_SOUND";
        
        /// <summary>
        /// ������� ��� ��������� ������/����� ������������� ����
        /// </summary>
        public const string STOP_SOUND = "STOP_SOUND";

        /// <summary>
        /// ������� ��� ��������� ���� ���������� ����� AudioManager-�
        /// </summary>
        public const string STOP_ALL_SOUND_SOURCE = "STOP_ALL_SOUND_SOURCE";
        #endregion

        #region Events broadcast by manager

        #endregion
    }

    private void Awake()
    {
        Messenger<SoundType, string, float>.AddListener(Event.PLAY_SOUND, PlaySound);
        Messenger<SoundType, float>.AddListener(Event.PLAY_RANDOM_SOUND, PlayRandomSound);
        Messenger<SoundType>.AddListener(Event.STOP_SOUND, StopSound);
        Messenger.AddListener(Event.STOP_ALL_SOUND_SOURCE, StopAllSoundSource);

        Messenger.AddListener(GameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM, EventHandler_1);
        Messenger<int>.AddListener(WaveManager.Event.PREPARING_FOR_WAVE, EventHandler_2);
        Messenger<int>.AddListener(WaveManager.Event.WAVE_IN_COMMING, EventHandler_3);
        Messenger.AddListener(WaveManager.Event.WAVE_IS_OVER, EventHandler_4);
        Messenger.AddListener(GameEvent.GAME_OVER, StopAllSoundSource);

        Messenger<bool>.AddListener(GameSceneManager.Event.PAUSE_GAME, PauseAllAudioSource);
    }

    private void OnDestroy()
    {
        Messenger<SoundType, string, float>.RemoveListener(Event.PLAY_SOUND, PlaySound);
        Messenger<SoundType, float>.RemoveListener(Event.PLAY_RANDOM_SOUND, PlayRandomSound);
        Messenger<SoundType>.RemoveListener(Event.STOP_SOUND, StopSound);
        Messenger.RemoveListener(Event.STOP_ALL_SOUND_SOURCE, StopAllSoundSource);

        Messenger.RemoveListener(GameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM, EventHandler_1);
        Messenger<int>.RemoveListener(WaveManager.Event.PREPARING_FOR_WAVE, EventHandler_2);
        Messenger<int>.RemoveListener(WaveManager.Event.WAVE_IN_COMMING, EventHandler_3);
        Messenger.RemoveListener(WaveManager.Event.WAVE_IS_OVER, EventHandler_4);
        Messenger.RemoveListener(GameEvent.GAME_OVER, StopAllSoundSource);

        Messenger<bool>.RemoveListener(GameSceneManager.Event.PAUSE_GAME, PauseAllAudioSource);
    }

    public void Startup()
    {
        Debug.Log("Audio manager starting...");

        // any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
        Status = ManagerStatus.Started;
    }

    /// <summary>
    /// ����� ��� ��������������� ���������� ������� ������ �� ������� _ambientSounds
    /// </summary>
    /// <param name="nameAmbient">�������� �����, ������� ������� �� ������� � �������������</param>
    private void PlaySound(SoundType soundType, string nameSound, float delay = 0)
    {
        // �������� ���� �� ������� � ������������ ��������� � �����
        Sound sound = Array.Find(_sounds, x => x.soundType == soundType && x.name == nameSound);

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

    /// <summary>
    /// ����� ��� ��������������� ���������� ������ �� ������� _musicSounds
    /// </summary>
    /// <param name="nameAmbient">�������� �����, ������� ������� �� ������� � �������������</param>
    private void PlayRandomSound(SoundType soundType, float delay = 0)
    {
        // �������� ������ ������ ������������� ����
        Sound[] sounds = Array.FindAll(_sounds, x => x.soundType == soundType);

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

        if(audioSource == null)
        {
            Debug.Log("Sound source for this sound type is not assigned!");
            return;
        }

        // ������������� ��������� ��� ��������� �����
        SetAudioSourceParameters(audioSource, sound);

        // ����������� ����
        StartPlayAudioSource(audioSource, delay);
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
    /// ����� ��� ��������� ���� ���������� ����� AudioManager-�
    /// </summary>
    private void StopAllSoundSource()
    {
        _ambientSource.Stop();
        _musicSource.Stop();
        _sfxSource.Stop();
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
                return _musicSource;
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
    /// ��������� ������������ ��������� �����
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
    private void EventHandler_1()
    {
        PlaySound(SoundType.Sfx, "new_message", 5);
    }

    private void EventHandler_2(int wave)
    {
        PlaySound(SoundType.Sfx, "new_message", 0);
    }

    private void EventHandler_3(int wave)
    {
        PlaySound(SoundType.Sfx, "alarm_horn", 0);
        PlayRandomSound(SoundType.CombatMusic, 8);
    }

    private void EventHandler_4()
    {
        PlaySound(SoundType.Sfx, "wave_is_over", 0);
    }
    #endregion
}
