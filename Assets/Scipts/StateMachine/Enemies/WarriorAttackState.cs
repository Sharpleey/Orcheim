using UnityEngine;

public class WarriorAttackState : EnemyState
{
    //TODO Добавить тип состояния с помощью Enum, переделать метод смены состояния. Сделать его универсальным для вызова из кода и события анимации

    private int _attackVariant = 0;
    private int _attackVariantCount = 5;


    public WarriorAttackState(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        _attackVariant = Random.Range(0, _attackVariantCount);

        enemy.Animator.SetInteger(HashAnimStringEnemy.AttackVariant, _attackVariant);
        enemy.Animator.SetTrigger(HashAnimStringEnemy.IsAttack);
    }
}
