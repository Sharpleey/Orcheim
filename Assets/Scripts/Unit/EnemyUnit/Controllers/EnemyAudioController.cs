using System;
using UnityEngine;

/// <summary>
/// ���������� ������ ����������. ���������� ������ ������, ������� �� ����� �������������� ��� ������������ ��������.
/// ��������� ������������� ���� ������������� ���� (EnemySoundType), ������� ����� ���������� ��������� �������.
/// </summary>
public class EnemyAudioController : MonoBehaviour
{
    /// <summary>
    /// �������� ������ � ����������
    /// </summary>
    [SerializeField] private AudioSource _enemyAudioSource;

    /// <summary>
    /// ������ ������ � ����� ����������
    /// </summary>
    [SerializeField] private EnemySoundPack[] _enemySoundPacks;


    private void Awake()
    {
        GameSceneEventManager.OnGamePause.AddListener(Pause);
    }

    /// <summary>
    /// ������������� ��������� ���� ������������� ���� c ��������� ������������. ����������� ������������ � ������ EnemySoundPack
    /// </summary>
    /// <param name="enemySoundType">��� ����� EnemySoundType, ������� �� ����� �������������</param>
    public void PlayRandomSoundWithProbability(EnemySoundType enemySoundType)
    {
        // ���� ��� ��������� ������
        if (!_enemyAudioSource)
        {
            Debug.Log("Enemy Audio Source not found!");
            return;
        }

        // �������� ����� ������������� ���� �� ������� �������
        EnemySoundPack enemySoundPack = Array.Find(_enemySoundPacks, x => x.EnemySoundType == enemySoundType);

        // ���� � ������� ��� ������ ������������� ����
        if (enemySoundPack == null)
        {
            Debug.Log("Enemy Sound Pack not found!");
            return;
        }

        // ���� �� ����� ���� ������������� ���� ������� ����
        if (!IsPlay(enemySoundPack.SoundPlaybackProbability))
            return;

        // �������� ��������� ���� �� ������
        int randSound = UnityEngine.Random.Range(0, enemySoundPack.AudioClips.Length);

        // ���� �������� ���� �����, �������� ���������������
        if (_enemyAudioSource.isPlaying)
            return;

        // ������������� ����
        Play(enemySoundPack.AudioClips[randSound]);
    }

    private void Play(AudioClip audioClip)
    {
        _enemyAudioSource.clip = audioClip;
        _enemyAudioSource.Play();
    }

    public void Pause(bool isPause)
    {
        if (isPause && _enemyAudioSource.isPlaying)
            _enemyAudioSource.Pause();
        else
            _enemyAudioSource.UnPause();
    }

    /// <summary>
    /// ����� �� ���� ������������� ���� ������� ���� 
    /// </summary>
    /// <param name="chance"></param>
    /// <returns></returns>
    private bool IsPlay(int chance)
    {
        int valueChance = UnityEngine.Random.Range(0, 100);

        if (valueChance <= chance)
            return true;

        return false;
    }
}
