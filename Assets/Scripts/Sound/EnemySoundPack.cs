using UnityEngine;

/// <summary>
/// ������������� ����� ������ ������. ���������� ����� �������� ������, ��� ������  � ���� ��������������� ����� �� ������� ������
/// </summary>
[System.Serializable]
public class EnemySoundPack
{
    /// <summary>
    /// ��� ������ ������
    /// </summary>
    [SerializeField] private EnemySoundType _enemySoundType;

    /// <summary>
    /// ���� ��������������� �����
    /// </summary>
    [SerializeField, Range(0, 100)] private int _soundPlaybackProbability = 90;

    /// <summary>
    /// ����� ������ ������� ����
    /// </summary>
    [SerializeField] private AudioClip[] _audioClips;

    /// <summary>
    /// ��� ������ ������
    /// </summary>
    public EnemySoundType EnemySoundType => _enemySoundType;
    
    /// <summary>
    /// ������ �����������
    /// </summary>
    public AudioClip[] AudioClips => _audioClips;
    
    /// <summary>
    /// ���� ��������������� ����� �� ������� ������
    /// </summary>
    public int SoundPlaybackProbability => _soundPlaybackProbability;

}