using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTester : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private AudioSource _audioSource;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            PlayRandomSound();
        }
    }

    private void PlayRandomSound()
    {
        int randSound = Random.Range(0, _audioClips.Length);
        _audioSource.PlayOneShot(_audioClips[randSound]);
    }
}
