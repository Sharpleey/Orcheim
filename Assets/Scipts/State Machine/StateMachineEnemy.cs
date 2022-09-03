using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ������������ ���������� ��� ������ ���������
/// </summary>
public class StateMachineEnemy : MonoBehaviour
{
    //private Dictionary<Type, State> _states;
    /// <summary>
    /// ������ �� ������� ��������� ����������
    /// </summary>
    public State CurrentState { get; private set; }

    //private void InitializeStates()
    //{
    //    IEnemy enemy = GetComponent<IEnemy>();

    //    _states = new Dictionary<Type, State>();

    //    _states[typeof(IdleState)] = new IdleState(enemy, this);
    //    _states[typeof(PursuitState)] = new PursuitState(enemy, this);
    //    _states[typeof(AttackState)] = new AttackState(enemy, this);
    //}

    //private State GetState<T>() where T: State
    //{
    //    var type = typeof(T);
    //    return _states[type];
    //}

    /// <summary>
    /// ����� ������������� ������ ���������, ���������� CurrentState �������� startingState � ������� ��� ���� Enter. 
    /// ��� �������������� ������ ���������, � ������ ��� ������� �������� ���������.
    /// </summary>
    /// <param name="startingState">��������� ���������</param>
    public void InitializeStartingState(State startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    /// <summary>
    /// ����� ������������ �������� ����� �����������. �� �������� Exit ��� ������� CurrentState ����� ������� ��� ������ �� newState. � ����� �� �������� Enter ��� newState.
    /// </summary>
    /// <param name="newState">����� ���������, ������� ����� ����������</param>
    public void ChangeState(State newState)
    {
        CurrentState.Exit();

        CurrentState = newState;
        newState.Enter();
    }
}
