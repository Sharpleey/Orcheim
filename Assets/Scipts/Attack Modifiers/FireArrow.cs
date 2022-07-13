using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrow : MonoBehaviour, IModifier
{
    #region Serialize Fields
    [SerializeField] string _name = "Œ„ÌÂÌÌ‡ˇ ÒÚÂÎ‡";
    [SerializeField] string _description = "œÓ‰ÊË„‡ÂÚ ÔÓÚË‚ÌËÍ‡ Ò ÌÂÍÓÚÓ˚Ï ¯‡ÌÒÓÏ, Ì‡ÌÓÒˇ ÛÓÌ ‚ ÒÂÍÛÌ‰Û";

    [SerializeField] int _proc—hance = 25;
    [SerializeField] int _damagePerSecond = 10;
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
    public int DamagePerSecond
    {
        get
        {
            return _damagePerSecond;
        }
        set
        {
            if (value <= 0)
            {
                _damagePerSecond = 1;
                return;
            }
            _damagePerSecond = value;
        }
    }
    #endregion Properties

    #region Methods
    private void Awake()
    {
        Name = _name;
        Description = _description;
        Proc—hance = _proc—hance;
        DamagePerSecond = _damagePerSecond;
        Duration = _duration;
    }
    private void OnTriggerEnter(Collider hitCollider)
    {

    }
    #endregion Methods
}
