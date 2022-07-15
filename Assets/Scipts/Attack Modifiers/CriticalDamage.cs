using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalDamage : MonoBehaviour, IModifier
{
    #region Serialize Fields
    [SerializeField] private string _name = " ËÚË˜ÂÒÍËÈ ÛÓÌ";
    [SerializeField] private string _description = "œÓÁ‚ÓÎˇÂÚ Ò ÌÂÍÓÚÓ˚Ï ¯‡ÌÒÓÏ Ì‡ÌÓÒËÚÚ¸ Û‚ÂÎË˜ÂÌÌ˚È ÛÓÌ";

    [SerializeField] [Range(10, 100)] private int _proc—hance = 10;
    [SerializeField] [Range(1.5f, 10f)]  private float _critMultiplierDamage = 1.5f;
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
    public float CritMultiplierDamage
    {
        get
        {
            return _critMultiplierDamage;
        }
        set
        {
            if (value < 1.5f)
            {
                _critMultiplierDamage = 1.5f;
                return;
            }
            if (value > 10.0f)
            {
                _critMultiplierDamage = 10.0f;
                return;
            }
            _critMultiplierDamage = value;
        }
    }
    #endregion Properties

    #region Methods
    private void Awake()
    {
        Name = _name;
        Description = _description;
        Proc—hance = _proc—hance;
        CritMultiplierDamage = _critMultiplierDamage;
    }
    private void OnTriggerEnter(Collider hitCollider)
    {

    }
    #endregion Methods
}
