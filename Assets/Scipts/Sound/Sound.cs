using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name = "sound_name";
    public SoundType soundType;
    public AudioClip audioClip;
    public bool isLoop;
    [Range(0,1f)] public float volume = 1f;
}
