using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mjolnir : MonoBehaviour, IModifier
{
    #region Serialize Fields
    [SerializeField] string _name = "�������";
    [SerializeField] string _description = "� ��������� ������ ����� ��������� ������, ������� ������� ����������� ���� �� �������";

    [SerializeField] int _proc�hance = 25;
    [SerializeField] int _damage = 20;
    [SerializeField] int _maxTargetMjolnir = 3;
    [SerializeField] float _radius = 2.5f;
    #endregion Serialize Fields

    #region Properties
    public string Name { get => _name; private set => _name = value; }
    public string Description { get => _description; private set => _description = value; }
    public int Proc�hance
    {
        get
        {
            return _proc�hance;
        }
        set
        {
            if (value < 10)
            {
                _proc�hance = 10;
                return;
            }
            if (value > 100)
            {
                _proc�hance = 100;
                return;
            }
            _proc�hance = value;
        }
    }
    public int Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            if (value < 0)
            {
                _damage = 0;
                return;
            }
            _damage = value;
        }
    }
    public int MaxTargetMjolnir
    {
        get
        {
            return _maxTargetMjolnir;
        }
        set
        {
            if (value < 2)
            {
                _maxTargetMjolnir = 2;
                return;
            }
            _maxTargetMjolnir = value;
        }
    }
    public float Radius
    {
        get
        {
            return _radius;
        }
        set
        {
            if (value < 1)
            {
                _radius = 1;
                return;
            }
            _radius = value;
        }
    }
    #endregion Properties

    #region Methods
    private void Awake()
    {
        Name = _name;
        Description = _description;
        Proc�hance = _proc�hance;
        Damage = _damage;
        MaxTargetMjolnir = _maxTargetMjolnir;
        Radius = _radius;
    }
    private void OnTriggerEnter(Collider hitCollider)
    {

    }
    #endregion Methods
}
