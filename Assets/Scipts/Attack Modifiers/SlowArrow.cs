using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowArrow : MonoBehaviour, IModifier
{
    #region Serialize Fields
    [SerializeField] string _name = "«‡ÏÂ‰Îˇ˛˘‡ˇ ÒÚÂÎ‡ ÒÚÂÎ‡";
    [SerializeField] string _description = "«‡ÏÂ‰ÎˇÂÚ ÔÓÚË‚ÌËÍ‡ Ò ÌÂÍÓÚÓ˚Ï ¯‡ÌÒÓÏ";

    [SerializeField] int _proc—hance = 25;
    [SerializeField] float _slowdown = 0.25f;
    [SerializeField] int _duration = 3;
    #endregion Serialize Fields

    #region Properties
    public string Name { get => _name; private set => _name = value; }
    public string Description { get => _description; private set => _description = value; }
    public int Proc—hance
    {
        get
        {
            return _proc—hance;
        }
        set
        {
            if (value < 10)
            {
                _proc—hance = 10;
                return;
            }
            if (value > 100)
            {
                _proc—hance = 100;
                return;
            }
            _proc—hance = value;
        }
    }
    public int Duration
    {
        get
        {
            return _duration;
        }
        set
        {
            if (value <= 0)
            {
                _duration = 1;
                return;
            }
            _duration = value;
        }
    }
    public float Slowdown
    {
        get
        {
            return _slowdown;
        }
        set
        {
            if (value <= 0.0f)
            {
                _slowdown = 0.1f;
                return;
            }
            if (value > 0.9f)
            {
                _slowdown = 0.9f;
                return;
            }
            _slowdown = value;
        }
    }
    #endregion Properties

    #region Methods
    private void Awake()
    {
        Name = _name;
        Description = _description;
        Proc—hance = _proc—hance;
        Slowdown = _slowdown;
        Duration = _duration;
    }
    private void OnTriggerEnter(Collider hitCollider)
    {

    }
    #endregion Methods
}
