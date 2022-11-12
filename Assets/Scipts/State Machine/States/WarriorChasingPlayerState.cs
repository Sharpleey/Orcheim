using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorChasingPlayerState : ChasingPlayerState
{
    public WarriorChasingPlayerState(Enemy enemy) : base(enemy)
    {

    }
    public override void Enter()
    {
        base.Enter();

        // �������� �������� ��� ����� ���������, ������ �������� ��������
        enemy.Animator.SetBool(HashAnimString.IsMovement, true);
    }

    public override void Update()
    {
        base.Update();

        // ���� ��������� ������� �� ��������� ����� (_attackDistance), �� �������� ���������
        if (distanceEnemyToPlayer < enemy.AttackDistance)
        {
            // �������� ��������� �� ��������� �����
            enemy.SetState<WarriorAttackState>();
        }
    }

    public override void Exit()
    {
        base.Exit();

        // ������ �������� ��������, ��������� �������� ��� ����� ���������
        enemy.Animator.SetBool(HashAnimString.IsMovement, false);
    }
}
