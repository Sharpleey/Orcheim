using UnityEngine;

public class WarriorAttackState : EnemyState
{
    //TODO Добавить тип состояния с помощью Enum, переделать метод смены состояния. Сделать его универсальным для вызова из кода и события анимации

    /// <summary>
    /// Кол-во вариантов атак
    /// </summary>
    private int _attackVariantCount = 5;

    public WarriorAttackState(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        enemy.NavMeshAgent.isStopped = true;

        enemy.Animator.SetInteger(HashAnimStringEnemy.AttackVariant, Random.Range(0, _attackVariantCount));
        enemy.Animator.SetTrigger(HashAnimStringEnemy.IsAttack);
    }

    public override void Exit()
    {
        enemy.NavMeshAgent.isStopped = false;
    }
}
