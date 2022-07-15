using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetration : MonoBehaviour, IModifier
{
    #region Serialize Fields
    [SerializeField] private string _name = "œÓ·Ë‚‡ÌËÂ";
    [SerializeField] private string _description = "œÓÁ‚ÓÎˇÂÚ ÔÓ·Ë‚‡Ú¸ ÌÂÒÍÓÎ¸ÍËı ÔÓÚË‚ÌËÍÓ‚ c ÌÂÍÓÚÓ˚Ï ¯‡ÌÒÓÏ, Ò Í‡Ê‰˚Ï ÔÓ·ËÚËÂÏ ÛÓÌ ÛÏÂÌ¸¯‡ÂÚÒˇ";

    [SerializeField] [Range(10, 100)] private int _proc—hance = 10;
    [SerializeField] [Range(2, 10)] private int _maxTargetPenetration = 2;
    [SerializeField] [Range(0.5f, 0.1f)] private float _damageDecrease = 0.5f;
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
    #endregion Properties
    
    #region Methods
    private void Awake()
    {
        Name = _name;
        Description = _description;
        Proc—hance = _proc—hance;
        MaxTargetPenetration = _maxTargetPenetration;
        DamageDecrease = _damageDecrease;
    }
    private void OnTriggerEnter(Collider hitCollider)
    {

    }
    #endregion Methods
}
