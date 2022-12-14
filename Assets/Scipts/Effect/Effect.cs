using System;
using System.Collections;
using System.Collections.Generic;
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
    public virtual float Duration { get; set; }
    public virtual float Frequency { get; set; } = 1f;

    public IEnumerator CoroutineEffect { get; private set; }

    protected Enemy enemy;

    public Effect()
    {
        
    }

    public Effect DeepCopy(Enemy enemy)
    {
        Effect other = (Effect)MemberwiseClone();
        other.enemy = enemy;
        other.CoroutineEffect = other.UpdateEffect();
        return other;
    }

    public virtual void Enable()
    {

    }

    public virtual void Tick()
    {

    }

    public virtual void Disable()
    {

    }

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
