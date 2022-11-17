using UnityEngine;

[System.Serializable]
public class Sound
{
    public MusicSound name;
    public AudioClip audioClip;
    [Range(0,1f)] public float volume = 1f;
    public bool isLoop;
}
