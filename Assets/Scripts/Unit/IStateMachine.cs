using System;
using System.Collections.Generic;

// TODO Сделать отдельный класс может быть?? мм?
public interface IStateMachine
{
    /// <summary>
    /// Состояние юнита врага по умолчанию
    /// </summary>
    public StartStateType DefaultState { get; }

    /// <summary>
    /// Словарь состояний
    /// </summary>
    public Dictionary<Type, EnemyState> States { get; }

    /// <summary>
    /// Текущее состояние юнита врага
    /// </summary>
    public EnemyState CurrentState { get; }

    /// <summary>
    /// Инициализация состояний
    /// </summary>
    public void InitStates();

    /// <summary>
    /// Установка состояния по-умолчанию
    /// </summary>
    public void SetStateByDefault();

    /// <summary>
    /// Установаить состояние юниту врага
    /// </summary>
    /// <typeparam name="T">Тип состояния нового состояния</typeparam>
    public void SetState<T>() where T : EnemyState;
}
