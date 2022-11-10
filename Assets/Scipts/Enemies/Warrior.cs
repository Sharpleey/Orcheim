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

        _states[typeof(ChasingPlayerState)] = new ChasingPlayerState(this);
        _states[typeof(AttackState)] = new AttackState(this);
    }
}