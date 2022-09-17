using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс обеспечивает абстракцию для машины состояний
/// </summary>
public class StateMachineSwordsman : MonoBehaviour
{
    /// <summary>
    /// Враг имеет только состояние покоя
    /// </summary>
    [SerializeField] private bool _isOnlyIdleState;
    /// <summary>
    /// Начальное состояние врага - преследование (По умолчанию - покоя)
    /// </summary>
    [SerializeField] private bool _isStartPursuitState;

    /// <summary>
    /// Ссылка на текущее состояние противника
    /// </summary>
    public State CurrentState { get; private set; }
    public IdleState IdleState { get; private set; }
    public PursuitState PursuitState { get; private set; }
    public AttackState AttackState { get; private set; }
    public DieState DieState { get; private set; }

    public bool IsStartPursuitState { get; private set; }
    public bool IsOnlyIdleState { get; private set; }

    private void Awake()
    {
        IsStartPursuitState = _isStartPursuitState;
        IsOnlyIdleState = _isOnlyIdleState;
    }
    public void InitializeStates(SwordsmanEnemy swordsmanEnemy)
    {
        IdleState = new IdleState(swordsmanEnemy, this);
        PursuitState = new PursuitState(swordsmanEnemy, this);
        AttackState = new AttackState(swordsmanEnemy, this);
        DieState = new DieState(swordsmanEnemy, this);
    }

    /// <summary>
    /// Метод конфигурирует машину состояний, присваивая CurrentState значение startingState и вызывая для него Enter. 
    /// Это инициализирует машину состояний, в первый раз задавая активное состояние.
    /// </summary>
    /// <param name="startingState">Стартовое состояние</param>
    public void InitializeStartingState(State startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    /// <summary>
    /// Метод обрабатывает переходы между состояниями. Он вызывает Exit для старого CurrentState перед заменой его ссылки на newState. В конце он вызывает Enter для newState.
    /// </summary>
    /// <param name="newState">Новое состояние, которое хотим установить</param>
    public void ChangeState(State newState)
    {
        CurrentState.Exit();

        CurrentState = newState;
        newState.Enter();
    }
}
