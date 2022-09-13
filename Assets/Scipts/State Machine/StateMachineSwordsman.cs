using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ������������ ���������� ��� ������ ���������
/// </summary>
public class StateMachineSwordsman : MonoBehaviour
{
    //[SerializeField] private String _nameCurrentState;
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
    /// ����� ������������� ������ ���������, ���������� CurrentState �������� startingState � ������� ��� ���� Enter. 
    /// ��� �������������� ������ ���������, � ������ ��� ������� �������� ���������.
    /// </summary>
    /// <param name="startingState">��������� ���������</param>
    public void InitializeStartingState(State startingState)
    {
        CurrentState = startingState;
        //_nameCurrentState = CurrentState.ToString();
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
        //_nameCurrentState = CurrentState.ToString();
        newState.Enter();
    }
}
