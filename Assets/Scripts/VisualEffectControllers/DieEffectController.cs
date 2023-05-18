using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Конроллер эффекта смерти. Анимация эффекта начинается полсе активации этого компонента
/// </summary>
public class DieEffectController : MonoBehaviour
{
    #region Serialize fields
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _dutarionDieEffect = 2;
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
        _renderers = _enemy.GetComponentsInChildren<Renderer>();
        _particleSystem = GetComponent<ParticleSystem>();

        var main = _particleSystem.main;
        main.duration = _dutarionDieEffect;
    }
    
    private void OnEnable()
    {
        _particleSystem.Play();
    }
    #endregion Mono

    #region Private methods
    private void Update()
    {
        if (_timer < _dutarionDieEffect)
        {
            _timer += Time.deltaTime;
        }

        foreach (var renderer in _renderers)
            renderer.material.SetFloat(_shaderProperty, fadeIn.Evaluate(Mathf.InverseLerp(0, _dutarionDieEffect, _timer)));

    }
    #endregion Private methods
}
