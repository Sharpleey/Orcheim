using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectDamage : MonoBehaviour ,IModifier
{
    #region Serialize fields
    [SerializeField] private string _name = "Урон";
    [SerializeField] private string _description = "Наносит противнику урон при попадании";

    [SerializeField] [Range(1, 300)] private int _averageDamage = 25;
    [SerializeField] private DamageType _typeDamage = DamageType.Physical;
    #endregion Serialize fields

    #region Properties
    public string Name { get => _name; private set => _name = value; }
    public string Description { get => _description; private set => _description = value; }
    
    /// <summary>
    /// Исходный средний урон. Изменять только при прокачке модификатора или в инспекторе
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
    /// Тип урона
    /// </summary>
    public DamageType TypeDamage { get => _typeDamage; private set => _typeDamage = value; }
    
    /// <summary>
    /// Текущий (Используемый) средний урон. Изменяется при уменьшении урона при последующем пробитии (при включенном модификаторе Penetration) или других модах
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
    /// Урон с учетом разброса
    /// </summary>
    public int ActualDamage
    {
        get
        {
            int range = (int)(CurrentAverageDamage * GeneralParameter.OFFSET_DAMAGE_HEALING);
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
        TypeDamage = _typeDamage;
    }
    #endregion Mono
}
