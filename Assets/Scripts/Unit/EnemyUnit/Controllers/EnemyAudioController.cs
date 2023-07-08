using System;
using UnityEngine;

/// <summary>
///  онтроллер звуков противника. ќпредел€ет наборы звуков, которые мы будем воспроизводить при определенных событи€х.
/// ѕозвол€ет воспроизвести звук определенного типа (EnemySoundType), который будет выбиратьс€ случайным образом.
/// </summary>
public class EnemyAudioController : MonoBehaviour
{
    /// <summary>
    /// »сточник звуков у противника
    /// </summary>
    [SerializeField] private AudioSource _enemyAudioSource;

    /// <summary>
    /// Ќаборы звуков к этому противнику
    /// </summary>
    [SerializeField] private EnemySoundPack[] _enemySoundPacks;


    private void Awake()
    {
        GameSceneEventManager.OnGamePause.AddListener(Pause);
    }

    /// <summary>
    /// ¬оспроизвести случайный звук определенного типа c некоторой веро€тностью. ¬еро€тность определ€етс€ в классе EnemySoundPack
    /// </summary>
    /// <param name="enemySoundType">“ип звука EnemySoundType, который мы хотим воспроизвести</param>
    public void PlayRandomSoundWithProbability(EnemySoundType enemySoundType)
    {
        // ≈сли нет источника звуков
        if (!_enemyAudioSource)
        {
            Debug.Log("Enemy Audio Source not found!");
            return;
        }

        // ѕолучаем набор определенного типа из массива наборов
        EnemySoundPack enemySoundPack = Array.Find(_enemySoundPacks, x => x.EnemySoundType == enemySoundType);

        // ≈сли в массиве нет набора определенного типа
        if (enemySoundPack == null)
        {
            Debug.Log("Enemy Sound Pack not found!");
            return;
        }

        // ≈сли не выпал шанс воспроизвести звук данного типа
        if (!IsPlay(enemySoundPack.SoundPlaybackProbability))
            return;

        // ¬ыбираем случайный звук из набора
        int randSound = UnityEngine.Random.Range(0, enemySoundPack.AudioClips.Length);

        // ≈сли источник звка зан€т, отмен€ем воспроизведение
        if (_enemyAudioSource.isPlaying)
            return;

        // ¬оспроизводим звук
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
    /// ¬ыпал ли шанс воспроизвести звук данного типа 
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
