using UnityEngine;

[CreateAssetMenu(menuName = "ManagerConfig/AudioManagerConfig", fileName = "AudioManagerConfig", order = 0)]
public class AudioManagerConfig : ScriptableObject
{
    [SerializeField] private Sound[] _sounds;

    public Sound[] Sounds => _sounds;
}
