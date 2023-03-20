using System.Collections;
using UnityEngine;

/// <summary>
/// Абстрактный класс эффекта
/// </summary>
public abstract class Effect : IEntity, ICloneableEffect
{
    #region Properties

    /// <summary>
    /// Название эффекта
    /// </summary>
    public abstract string Name { get; }
    
    /// <summary>
    /// Описание эффекта
    /// </summary>
    public abstract string Description { get; }

    /// <summary>
    /// Тип эффекта, положительный или негативный
    /// </summary>
    public EffectType Type { get; protected set; } = EffectType.Negative;

    /// <summary>
    /// Длительность эффекта, если хотим сделать эффект без длительности, то не переопределяем
    /// </summary>
    public Parameter Duration { get; protected set; } = null;

    /// <summary>
    /// Частота действия эффекта, по умолчанию эффект действует каждый 1 сек.
    /// </summary>
    public float Frequency { get; protected set; } = 1f;

    /// <summary>
    /// Корутина отвечающая за переодичность эффекта, запускается в объекте на котором установлен эффект
    /// </summary>
    public IEnumerator CoroutineEffect { get; private set; }

    #endregion Properties

    #region Private fields

    /// <summary>
    /// Объект юнита, над который производим изменения с помощью эффекта
    /// </summary>
    protected Unit unit;
    protected EnemyUnit enemyUnit;
    protected Player player;

    #endregion Private fields

    #region Methods

    /// <summary>
    /// Метод для глубокого копирования объекта эффекта
    /// </summary>
    /// <param name="unit">Для какого объекта врага будет устанавливаться данный эффекта</param>
    /// <returns>Копия эффекта</returns>
    public Effect DeepCopy(Unit unit)
    {
        Effect other = (Effect)MemberwiseClone();
        other.unit = unit;
        other.CoroutineEffect = other.UpdateEffect();
        return other;
    }

    /// <summary>
    /// Метод применения эффекта, производим все действия над врагом при установке эффекта
    /// </summary>
    public virtual void Enable()
    {
        enemyUnit = unit as EnemyUnit;
        player = unit as Player;
    }

    /// <summary>
    /// Метод, в котором эффект производит все действия над персонажем врага с указанной переодичностью
    /// </summary>
    public virtual void Tick()
    {

    }

    /// <summary>
    /// Метод вызывается при удалении эффекта с персонажа.
    /// </summary>
    public virtual void Disable()
    {

    }

    /// <summary>
    /// Метод для имитации Update
    /// </summary>
    /// <returns></returns>
    protected IEnumerator UpdateEffect()
    {
        float duration = 0;

        while (true)
        {
            Tick();

            yield return new WaitForSeconds(Frequency);

            duration += Frequency;

            if (duration >= Duration.Value)
            {
                break;
            }
        }
        
        unit.RemoveEffect(this);
    }

    #endregion
}
