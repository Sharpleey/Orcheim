using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] _bowStringLoadSounds;
    [SerializeField] private AudioClip[] _bowShotSounds;

    [SerializeField] private AudioSource _bowAudioSource;

    public void PlayShot()
    {
        int randSound = UnityEngine.Random.Range(0, _bowShotSounds.Length);
        _bowAudioSource.PlayOneShot(_bowShotSounds[randSound]);
    }

    public void PlayStringLoad()
    {
        int randSound = UnityEngine.Random.Range(0, _bowStringLoadSounds.Length);
        _bowAudioSource.PlayOneShot(_bowStringLoadSounds[randSound]);
    }
}
