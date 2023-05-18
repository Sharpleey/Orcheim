using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : EnemyUnit
{
    /// <summary>
    /// Способность данного юнита
    /// </summary>
    public Inspiration Inspiration { get; private set; }

    /// <summary>
    /// Метод инициализирует состояния
    /// </summary>
    public override void InitStates()
    {
        base.InitStates();

        States[typeof(ChasingState)] = new CommanderChasingState(this);
        States[typeof(WarriorIdleAttackState)] = new WarriorIdleAttackState(this);
        States[typeof(WarriorAttackState)] = new WarriorAttackState(this);
        States[typeof(InspirationState)] = new InspirationState(this);
    }

    public override void InitAbilities()
    {
        base.InitAbilities();

        Inspiration = new Inspiration(this, timeCooldown: 30, radius: 8, isActive: true);

        Abilities.Add(typeof(Inspiration), Inspiration); // TODO
    }

    #region Methods Event Animation

    /// <summary>
    /// Метод для смены состояния из EventAnimation
    /// </summary>
    private void SetChasingState()
    {
        SetState<ChasingState>();
    }

    /// <summary>
    /// Метод для смены состояния при срабатывании события по окончанию анимации атаки.
    /// </summary>
    private void SetIdleAttackState()
    {
        SetState<WarriorIdleAttackState>();
    }

    /// <summary>
    /// Вызывается из анимации
    /// </summary>
    private void ApplyInspiration() //TODO
    {
        if (Inspiration.IsCooldown)
            return;

        Inspiration.Apply();

        StartCoroutine(Inspiration.Cooldown());
    }

    #endregion Methods Event Animation
}
