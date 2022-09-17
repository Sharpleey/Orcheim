using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ������������ ���������� ��� ������ ���������
/// </summary>
public class StateMachineSwordsman : MonoBehaviour
{
    /// <summary>
    /// ���� ����� ������ ��������� �����
    /// </summary>
    [SerializeField] private bool _isOnlyIdleState;
    /// <summary>
    /// ��������� ��������� ����� - ������������� (�� ��������� - �����)
    /// </summary>
    [SerializeField] private bool _isStartPursuitState;

    /// <summary>
    /// ������ �� ������� ��������� ����������
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
