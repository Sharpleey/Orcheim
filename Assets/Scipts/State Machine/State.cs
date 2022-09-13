using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������� ����������� �����, �� �������� ����������� ��� ������ ���������
/// </summary>
public abstract class State : Object
{
    /// <summary>
    /// ������ ������ �������� ������ ������ ���������� �� ����� ����������� � �������. ���������� ��� ������� ���� ��������������
    /// </summary>
    protected SwordsmanEnemy _enemy;

    /// <summary>
    /// ������ ������ ������� ������ ������ ���������, ������� ��������� � ������� ����������
    /// </summary>
    protected StateMachineSwordsman _stateMachine;

    /// <summary>
    /// ����������� ������ ���������, ��������� ��� ������������ ������ � ������� ���������� � ������ ���������
    /// </summary>
    /// <param name="enemy">������ � ��������� ����������</param>
    /// <param name="stateMachineEnemy">������ ��������� ����������</param>
    protected State(SwordsmanEnemy enemy, StateMachineSwordsman stateMachine)
    {
        _enemy = enemy;
        _stateMachine = stateMachine;
    }
    /// <summary>
    /// ����� ���������� ��� ����� � ���������
    /// </summary>
    public virtual void Enter()
    {

    }

    /// <summary>
    /// ������ ����� ���������� ������� � ������ Update � ������ �������������� �� MonoBehaviour. ����� ������, �� ������� ������ �� ��������� � ������ Update 
    /// </summary>
    public virtual void Update()
    {

    }

    /// <summary>
    /// ������ ����� ���������� ������� � ������ FixedUpdate � ������ �������������� �� MonoBehaviour. ����� ������, �� ������� ������ �� ��������� � ������ FixedUpdate 
    /// </summary>
    public virtual void FixedUpdate()
    {

    }

    /// <summary>
    /// ����� ���������� ��� ������ �� ���������
    /// </summary>
    public virtual void Exit()
    {

    }
}
