using UnityEngine;

public abstract class Sound
{
    public AudioClip audioClip;
    [Range(0,1f)] public float volume = 1f;
    public bool isLoop;
}
