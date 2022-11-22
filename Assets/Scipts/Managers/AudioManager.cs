using System;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    [SerializeField] private AmbientSound[] _ambientSounds;
    [SerializeField] private MusicSound[] _musicSounds;
    [SerializeField] private SFXSound[] _sfxSounds;
    [SerializeField] private AudioSource _ambientSource, _musicSource, _sfxSource;

    public ManagerStatus Status { get; private set; }

    public static class Event
    {
        #region Events for manager
        /// <summary>
        /// �������� ��������������� ������������ ������� ������
        /// </summary>
        public const string PLAY_AMBIENT = "PLAY_AMBIENT";
        /// <summary>
        /// �������� ��������������� ������������ ������
        /// </summary>
        public const string PLAY_MUSIC = "PLAY_MUSIC";
        /// <summary>
        /// �������� ��������������� ������������� ��������� �������
        /// </summary>
        public const string PLAY_SFX = "PLAY_SFX";
        /// <summary>
        /// �������� ��������������� ��������� ������� ������
        /// </summary>
        public const string PLAY_RANDOM_AMBIENT = "PLAY_RANDOM_AMBIENT";
        /// <summary>
        /// �������� ��������������� ��������� ������
        /// </summary>
        public const string PLAY_RANDOM_MUSIC = "PLAY_RANDOM_MUSIC";
        /// <summary>
        /// ������� ��� ��������� ������� ������
        /// </summary>
        public const string STOP_AMBIENT = "STOP_AMBIENT";
        /// <summary>
        /// ������� ��� ���������� ������ ����
        /// </summary>
        public const string STOP_MUSIC = "STOP_MUSIC";
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
        Messenger<AmbientAudioClip>.AddListener(Event.PLAY_AMBIENT, PlayAmbient);
        Messenger<MusicAudioClip>.AddListener(Event.PLAY_MUSIC, PlayMusic);
        Messenger<SFXAudioClip>.AddListener(Event.PLAY_SFX, PlaySFX);
        Messenger.AddListener(Event.STOP_AMBIENT, StopAmbient);
        Messenger.AddListener(Event.STOP_MUSIC, StopMusic);
        Messenger.AddListener(Event.STOP_ALL_SOUND_SOURCE, StopAllSoundSource);
        Messenger.AddListener(GameEvent.GAME_OVER, StopAmbient);
    }

    private void OnDestroy()
    {
        Messenger<AmbientAudioClip>.RemoveListener(Event.PLAY_AMBIENT, PlayAmbient);
        Messenger<MusicAudioClip>.RemoveListener(Event.PLAY_MUSIC, PlayMusic);
        Messenger<SFXAudioClip>.RemoveListener(Event.PLAY_SFX, PlaySFX);
        Messenger.RemoveListener(Event.STOP_AMBIENT, StopAmbient);
        Messenger.RemoveListener(Event.STOP_MUSIC, StopMusic);
        Messenger.RemoveListener(Event.STOP_ALL_SOUND_SOURCE, StopAllSoundSource);
        Messenger.RemoveListener(GameEvent.GAME_OVER, StopAmbient);
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
    private void PlayAmbient(AmbientAudioClip nameAmbient)
    {
        Sound sound = Array.Find(_ambientSounds, x => x.name == nameAmbient);

        if (sound == null)
        {
            Debug.Log("Ambient not found!");
            return;
        }

        _ambientSource.clip = sound.audioClip;
        _ambientSource.volume = sound.volume;
        _ambientSource.loop = sound.isLoop;
        _ambientSource.Play();
    }

    /// <summary>
    /// ����� ��� ��������������� ���������� ������ �� ������� _musicSounds
    /// </summary>
    /// <param name="nameAmbient">�������� �����, ������� ������� �� ������� � �������������</param>
    private void PlayMusic(MusicAudioClip nameMusic)
    {
        Sound sound = Array.Find(_musicSounds, x => x.name == nameMusic);

        if (sound == null)
        {
            Debug.Log("Music not found!");
            return;
        }

        _ambientSource.clip = sound.audioClip;
        _ambientSource.volume = sound.volume;
        _ambientSource.loop = sound.isLoop;
        _ambientSource.Play();
    }

    /// <summary>
    /// ����� ��� ��������������� ����������� ��������� ������� �� ������� _sfxSounds
    /// </summary>
    /// <param name="nameAmbient">�������� �����, ������� ������� �� ������� � �������������</param>
    private void PlaySFX(SFXAudioClip nameSFX)
    {
        Sound sound = Array.Find(_sfxSounds, x => x.name == nameSFX);

        if (sound == null)
        {
            Debug.Log("SFX not found!");
            return;
        }

        _sfxSource.clip = sound.audioClip;
        _sfxSource.volume = sound.volume;
        //musicSource.loop = sound.isLoop;
        _sfxSource.PlayOneShot(sound.audioClip);
    }

    /// <summary>
    /// ����� ��� ��������� ������� ������
    /// </summary>
    private void StopAmbient()
    {
        _ambientSource.Stop();
    }

    /// <summary>
    /// ����� ��� ��������� ������ ����
    /// </summary>
    private void StopMusic()
    {
        _musicSource.Stop();
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
}
