using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetration : MonoBehaviour, IModifier
{
    #region Serialize fields
    [SerializeField] private string _name = "Пробивание";
    [SerializeField] private string _description = "Позволяет пробивать нескольких противников c некоторым шансом, с каждым пробитием урон уменьшается";

    [SerializeField] [Range(2, 10)] private int _maxTargetPenetration = 2;
    [SerializeField] [Range(0.5f, 0.1f)] private float _damageDecrease = 0.5f;
    #endregion Serialize fields

    #region Properties
    public string Name { get => _name; private set => _name = value; }
    public string Description { get => _description; private set => _description = value; }
    public int MaxTargetPenetration
    {
        get
        {
            return _maxTargetPenetration;
        }
        set
        {
            if (value < 2)
            {
                _maxTargetPenetration = 2;
                return;
            }
            if (value > 10)
            {
                _maxTargetPenetration = 10;
                return;
            }
            _maxTargetPenetration = value;
        }
    }
    public float DamageDecrease
    {
        get
        {
            return _damageDecrease;
        }
        set
        {
            if (value < 0.1f)
            {
                _damageDecrease = 0.1f;
                return;
            }
            if (value > 0.5f)
            {
                _damageDecrease = 0.5f;
                return;
            }
            _damageDecrease = value;
        }
    }
    public int CurrentPenetration
    {
        get
        {
            return _currentPenetration;
        }
        set
        {
            if (value < 0)
            {
                _currentPenetration = 0;
                return;
            }
            if (value > MaxTargetPenetration)
            {
                _currentPenetration = MaxTargetPenetration;
                return;
            }
            _currentPenetration = value;
        }
    }
    #endregion Properties

    #region Private fields
    private int _currentPenetration = 0;
    #endregion Private fields

    #region Mono
    private void Awake()
    {
        Name = _name;
        Description = _description;
        MaxTargetPenetration = _maxTargetPenetration;
        DamageDecrease = _damageDecrease;
    }
    #endregion Mono
}
