using UnityEngine;

public class SceneAudioSourceController : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        GameSceneEventManager.OnGamePause.AddListener(Pause);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Pause(bool isPause)
    {
        if (isPause && _audioSource.isPlaying)
            _audioSource.Pause();
        else
            _audioSource.UnPause();
    }
}
