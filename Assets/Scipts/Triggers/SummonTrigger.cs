using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� �������� �� ������ �������� ������ ��������� ������, ����� ��������, �� ������� ������������ ���� �����, �������� ����� � ������� �����������.
/// ��� ������ ���������� ������ ��������� �� �������, �������� ��������� �� �������������
/// </summary>
public class SummonTrigger : MonoBehaviour
{
    private Warrior _enemy;

    private void Awake()
    {
        _enemy = GetComponentInParent<Warrior>();
    }
    private void OnTriggerExit(Collider otherSummonTriggerCollider)
    {
        // ���� ������� ��������� "�����", �� ������ �� ������
        if (_enemy.CurrentState == _enemy.IdleState)
            return;

        Warrior otherEnemy = otherSummonTriggerCollider.GetComponentInParent<Warrior>();

        // ���� �������� � ��������� "�������������" � ������ �������� (����� �������) � ��������� "�����", �� ������� ������ ��������� �� "�������������"
        if (_enemy.CurrentState == _enemy.PursuitState && otherEnemy.CurrentState == otherEnemy.IdleState)
        {
            otherEnemy.ChangeState(otherEnemy.PursuitState);
        }
    }
}
