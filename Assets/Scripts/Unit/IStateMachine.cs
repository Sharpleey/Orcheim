using System;
using System.Collections.Generic;

// TODO ������� ��������� ����� ����� ����?? ��?
public interface IStateMachine
{
    /// <summary>
    /// ��������� ����� ����� �� ���������
    /// </summary>
    public StartStateType DefaultState { get; }

    /// <summary>
    /// ������� ���������
    /// </summary>
    public Dictionary<Type, EnemyState> States { get; }

    /// <summary>
    /// ������� ��������� ����� �����
    /// </summary>
    public EnemyState CurrentState { get; }

    /// <summary>
    /// ������������� ���������
    /// </summary>
    public void InitStates();

    /// <summary>
    /// ��������� ��������� ��-���������
    /// </summary>
    public void SetStateByDefault();

    /// <summary>
    /// ����������� ��������� ����� �����
    /// </summary>
    /// <typeparam name="T">��� ��������� ������ ���������</typeparam>
    public void SetState<T>() where T : EnemyState;
}
