using UnityEngine;

/// <summary>
/// Сереализуемый класс набора звуков. Определяет набор звуковых клипов, тип набора  и шанс воспроизведения звука из данного набора
/// </summary>
[System.Serializable]
public class EnemySoundPack
{
    /// <summary>
    /// Тип набора звуков
    /// </summary>
    [SerializeField] private EnemySoundType _enemySoundType;

    /// <summary>
    /// Шанс воспроизвежения звука
    /// </summary>
    [SerializeField, Range(0, 100)] private int _soundPlaybackProbability = 90;

    /// <summary>
    /// Набор звуков данного пака
    /// </summary>
    [SerializeField] private AudioClip[] _audioClips;

    /// <summary>
    /// Тип набора звуков
    /// </summary>
    public EnemySoundType EnemySoundType => _enemySoundType;
    
    /// <summary>
    /// Массив аудиоклипов
    /// </summary>
    public AudioClip[] AudioClips => _audioClips;
    
    /// <summary>
    /// Шанс воспроизведения звука из данного набора
    /// </summary>
    public int SoundPlaybackProbability => _soundPlaybackProbability;

}