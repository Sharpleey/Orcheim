using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorChasingState : ChasingState
{
    public WarriorChasingState(Enemy enemy) : base(enemy)
    {

    }
    public override void Enter()
    {
        base.Enter();

        // ¬ключаем анимацию дл€ этого состо€ни€, задаем параметр анимации
        enemy.Animator.SetBool(HashAnimStringEnemy.IsMovement, true);
    }

    public override void Update()
    {
        base.Update();

        // ≈сли противник подошел на дистанцию атаки (_attackDistance), то измен€ем состо€ние
        if (distanceEnemyToPlayer < enemy.AttackDistance)
        {
            // »змен€ем состо€ние на состо€ние атаки
            enemy.SetState<WarriorAttackState>();
        }
    }

    public override void Exit()
    {
        base.Exit();

        // «адаем параметр анимации, выключаем анимацию дл€ этого состо€ни€
        enemy.Animator.SetBool(HashAnimStringEnemy.IsMovement, false);
    }
}
