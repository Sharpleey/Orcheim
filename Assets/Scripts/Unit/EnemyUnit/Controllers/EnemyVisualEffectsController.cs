using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisualEffectsController : MonoBehaviour
{
    #region Serialize fields
    
    [SerializeField] private ParticleSystem _flameParicleSystem;
    
    #endregion Serialize fields
    
    #region Private fields
    
    private List<ParticleSystem> _activeEffects = new List<ParticleSystem>();
    private Dictionary<Type, ParticleSystem> _effectParticleSystems = new Dictionary<Type, ParticleSystem>();

    #endregion Private fields

    private void Awake()
    {
        InitEffects();
    }

    private void Start()
    {
        DisableAllEffects();
    }

    private void InitEffects()
    {
        _effectParticleSystems[typeof(Flame)] = _flameParicleSystem;
    }
    
    public void DisableAllEffects(bool isOnlyActive = false)
    {
        foreach (ParticleSystem ps in _effectParticleSystems.Values)
        {
            if (isOnlyActive && ps.isPlaying)
                ps.Stop();
            else
                ps.Stop();
        }
    }

    public void EnableEffect<T>(bool active) where T : Effect
    {
        ParticleSystem particleSystem;

        if (_effectParticleSystems.TryGetValue(typeof(T), out particleSystem))
        {
            if (active)
            {
                particleSystem.Play();
                _activeEffects.Add(particleSystem);
            }
            else
            {
                particleSystem.Stop();
                _activeEffects.Remove(particleSystem);
            }
        }
    }
}
