using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEffectController : MonoBehaviour
{
    #region Serialize fields
    [SerializeField] private float _dieEffectTime = 2;
    [SerializeField] private AnimationCurve fadeIn;
    #endregion Serialize fields

    #region Private fields
    private ParticleSystem _particleSystem;
    private Renderer[] _renderers;

    private float _timer = 0;
    private int _shaderProperty;
    #endregion Private fields

    #region Mono
    private void Awake()
    {
        _shaderProperty = Shader.PropertyToID("_cutoff");
        _renderers = GetComponentsInChildren<Renderer>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();

        var main = _particleSystem.main;
        main.duration = _dieEffectTime;
    }
    
    private void OnEnable()
    {
        _particleSystem.Play();
    }
    #endregion Mono

    #region Private methods
    private void Update()
    {
        if (_timer < _dieEffectTime)
        {
            _timer += Time.deltaTime;
        }

        foreach (var renderer in _renderers)
            renderer.material.SetFloat(_shaderProperty, fadeIn.Evaluate(Mathf.InverseLerp(0, _dieEffectTime, _timer)));

    }
    #endregion Private methods
}
