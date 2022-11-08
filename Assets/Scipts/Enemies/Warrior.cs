using System;
using System.Collections.Generic;

public class Warrior : Enemy
{
    private new void Start()
    {
        base.Start();

        // Инициализируем состояния
        InitStates();
        // Задаем состояние поумолчанию
        SetStateByDefault();
    }

    /// <summary>
    /// Метод инициализирует состояния
    /// </summary>
    private new void InitStates()
    {
        base.InitStates();

        _states[typeof(PursuitState)] = new PursuitState(this);
        _states[typeof(AttackIdleState)] = new AttackIdleState(this);
    }

    /// <summary>
    /// Метод устанавливает первое состояние поумолчанию
    /// </summary>
    private void SetStateByDefault()
    {
        if (DefaultState == DefaultState.Pursuit)
            SetState<PursuitState>();
        else
            SetState<IdleState>();
    }
}