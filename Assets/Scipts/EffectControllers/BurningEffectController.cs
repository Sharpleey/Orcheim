using UnityEngine;

/// <summary>
/// ���������� ������� �������. �������� �� ������������ ������� �������
/// </summary>
public class BurningEffectController : MonoBehaviour
{
    #region Private fields
    private ParticleSystem _particleSystem;
    #endregion Private fields

    #region Mono
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.Stop();
    }
    private void OnEnable()
    {
        _particleSystem.Play();
    }
    private void OnDisable()
    {
        _particleSystem.Stop();
    }
    #endregion Mono
}
