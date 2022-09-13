using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс обеспечивает абстракцию для машины состояний
/// </summary>
public class StateMachineSwordsman : MonoBehaviour
{
    //[SerializeField] private String _nameCurrentState;
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

    private SwordsmanEnemy _swordsmanEnemy;

    private void Awake()
    {
        IsStartPursuitState = _isStartPursuitState;
        IsOnlyIdleState = _isOnlyIdleState;
    }
    private void Start()
    {
        _swordsmanEnemy = GetComponent<SwordsmanEnemy>();

        IdleState = new IdleState(_swordsmanEnemy, this);
        PursuitState = new PursuitState(_swordsmanEnemy, this);
        AttackState = new AttackState(_swordsmanEnemy, this);
        DieState = new DieState(_swordsmanEnemy, this);
    }

    /// <summary>
    /// Метод конфигурирует машину состояний, присваивая CurrentState значение startingState и вызывая для него Enter. 
    /// Это инициализирует машину состояний, в первый раз задавая активное состояние.
    /// </summary>
    /// <param name="startingState">Стартовое состояние</param>
    public void InitializeStartingState(State startingState)
    {
        CurrentState = startingState;
        //_nameCurrentState = CurrentState.ToString();
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
        //_nameCurrentState = CurrentState.ToString();
        newState.Enter();
    }
}
