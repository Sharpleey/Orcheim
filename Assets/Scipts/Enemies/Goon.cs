using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goon : Enemy
{
    private new void Start()
    {
        base.Start();

        // �������������� ���������
        InitStates();
        // ������ ��������� �����������
        SetStateByDefault();
    }

    /// <summary>
    /// ����� �������������� ���������
    /// </summary>
    private new void InitStates()
    {
        base.InitStates();

        _states[typeof(PursuitState)] = new PursuitState(this);
        _states[typeof(AttackIdleState)] = new AttackIdleState(this);
    }

    /// <summary>
    /// ����� ������������� ������ ��������� �����������
    /// </summary>
    private void SetStateByDefault()
    {
        if (DefaultState == DefaultState.Pursuit)
            SetState<PursuitState>();
        else
            SetState<IdleState>();
    }
}
