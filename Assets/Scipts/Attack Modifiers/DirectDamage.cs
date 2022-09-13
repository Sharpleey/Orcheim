using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectDamage : MonoBehaviour ,IModifier
{
    #region Serialize fields
    [SerializeField] private string _name = "����";
    [SerializeField] private string _description = "������� ���������� ���� ��� ���������";

    [SerializeField] [Range(1, 300)] private int _averageDamage = 25;
    [SerializeField] [Range(0f, 0.5f)] private float _offsetDamage = 0.25f;
    [SerializeField] private TypeDamage _typeDamage = TypeDamage.Physical;
    #endregion Serialize fields

    #region Properties
    public string Name { get => _name; private set => _name = value; }
    public string Description { get => _description; private set => _description = value; }
    /// <summary>
    /// �������� ������� ����. �������� ������ ��� �������� ������������ ��� � ����������
    /// </summary>
    public int AverageDamage
    {
        get
        {
            return _averageDamage;
        }
        set
        {
            if (value < 0)
            {
                _averageDamage = 0;
                return;
            }
            _averageDamage = value;
        }
    }
    /// <summary>
    /// ������� ����� 
    /// </summary>
    public float OffsetDamage
    {
        get
        {
            return _offsetDamage;
        }
        private set
        {
            if (value < 0f)
            {
                _offsetDamage = 0f;
                return;
            }
            if (value > 0.5f)
            {
                _offsetDamage = 0.5f;
                return;
            }
            _offsetDamage = value;
        }
    }
    /// <summary>
    /// ��� �����
    /// </summary>
    public TypeDamage TypeDamage { get => _typeDamage; private set => _typeDamage = value; }
    /// <summary>
    /// ������� (������������) ������� ����. ���������� ��� ���������� ����� ��� ����������� �������� (��� ���������� ������������ Penetration) ��� ������ �����
    /// </summary>
    public int CurrentAverageDamage
    {
        get
        {
            return _currentAverageDamage;
        }
        set
        {
            if (value < 0)
            {
                _currentAverageDamage = 0;
                return;
            }
            _currentAverageDamage = value;
        }
    }
    /// <summary>
    /// ���� � ������ ��������
    /// </summary>
    public int ActualDamage
    {
        get
        {
            int range = (int)(CurrentAverageDamage * OffsetDamage);
            return Random.Range(CurrentAverageDamage - range, CurrentAverageDamage + range);
        }
    }
    #endregion Properties

    #region Private fields
    private int _currentAverageDamage;
    #endregion Private fields

    #region Mono
    private void Awake()
    {
        Name = _name;
        Description = _description;
        AverageDamage = _averageDamage;
        CurrentAverageDamage = _averageDamage;
        OffsetDamage = _offsetDamage;
        TypeDamage = _typeDamage;
    }
    #endregion Mono
}
