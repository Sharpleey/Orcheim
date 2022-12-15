using System.Collections;
using UnityEngine;

public abstract class Effect : IItem, ICloneable
{
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
    public abstract EffectType EffectType { get; }
    
    /// <summary>
    /// Длительность эффекта, если хотим сделать эффект без длительности, то не переопределяем
    /// </summary>
    public virtual float Duration { get; set; }
    
    /// <summary>
    /// Частота действия эффекта, по умолчанию эффект действует каждый 1 сек.
    /// </summary>
    public virtual float Frequency { get; set; } = 1f;

    /// <summary>
    /// Корутина отвечающая за переодичность эффекта, запускается в объекте на котором установлен эффект
    /// </summary>
    public IEnumerator CoroutineEffect { get; private set; }

    /// <summary>
    /// Объект врага, над который производим изменения с помощью эффекта
    /// </summary>
    protected Enemy enemy; //TODO Должо работать не только на Enemy

    public Effect()
    {
        
    }

    /// <summary>
    /// Метод для глубокого копирования объекта эффекта
    /// </summary>
    /// <param name="enemy">Для какого объекта врага будет устанавливаться данный эффекта</param>
    /// <returns>Копия эффекта</returns>
    public Effect DeepCopy(Enemy enemy)
    {
        Effect other = (Effect)MemberwiseClone();
        other.enemy = enemy;
        other.CoroutineEffect = other.UpdateEffect();
        return other;
    }

    /// <summary>
    /// Метод применения эффекта, производим все действия над врагом при установке эффекта
    /// </summary>
    public virtual void Enable()
    {

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

            if (duration >= Duration)
            {
                break;
            }
        }
        
        enemy.RemoveEffect(this);
    }
}
