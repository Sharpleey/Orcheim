using UnityEngine;

public class BowAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] _bowStringLoadSounds;
    [SerializeField] private AudioClip[] _bowStringLoadedSounds;
    [SerializeField] private AudioClip[] _bowStringUnloadSounds;
    [SerializeField] private AudioClip[] _bowShotSounds;
    [SerializeField] private AudioClip[] _bowHitSoudns;

    [SerializeField] private AudioSource _bowAudioSource;

    public void PlayShot()
    {
        int randSound = UnityEngine.Random.Range(0, _bowShotSounds.Length);
        _bowAudioSource.PlayOneShot(_bowShotSounds[randSound]);
    }

    public void PlayHit()
    {
        int randSound = UnityEngine.Random.Range(0, _bowHitSoudns.Length);
        _bowAudioSource.PlayOneShot(_bowHitSoudns[randSound]);
    }

    public void PlayStringLoad()
    {
        int randSound = UnityEngine.Random.Range(0, _bowStringLoadSounds.Length);
        _bowAudioSource.PlayOneShot(_bowStringLoadSounds[randSound]);
    }
    public void PlayStringUnload()
    {
        int randSound = UnityEngine.Random.Range(0, _bowStringUnloadSounds.Length);
        _bowAudioSource.PlayOneShot(_bowStringUnloadSounds[randSound]);
    }
}
