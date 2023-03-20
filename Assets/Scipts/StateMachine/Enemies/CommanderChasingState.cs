using UnityEngine;

public class CommanderChasingState : ChasingState
{
    /// <summary>
    /// Таймер проверки союзных существ
    /// </summary>
    private float _timerCheckAlliesNearby;

    public CommanderChasingState(EnemyUnit enemyUnit) : base(enemyUnit)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // Обнуляем таймер
        _timerCheckAlliesNearby = 0;

        // Включаем анимацию для этого состояния, задаем параметр анимации
        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsMovement, true);
    }

    public override void Update()
    {
        base.Update();

        _timerCheckAlliesNearby += Time.deltaTime;

        if (_timerCheckAlliesNearby > 3f)
        {
            // Получаем кол-во союзных существ поблизости
            int countAlliesNearby = GetCountAlliesNearby(((Commander)enemyUnit).Inspiration.Radius.Value);

            // Если их больше 2х, то кастуем баф
            if (countAlliesNearby > 2 && !((Commander)enemyUnit).Inspiration.IsCooldown)
            {
                enemyUnit.SetState<InspirationState>();
            }

            // Обнуляем таймер
            _timerCheckAlliesNearby = 0;
        }

        // Если противник подошел на дистанцию атаки (_attackDistance), то изменяем состояние
        if (distanceEnemyToPlayer < enemyUnit.AttackDistance)
        {
            // Изменяем состояние на состояние атаки
            enemyUnit.SetState<WarriorAttackState>();
        }
    }

    public override void Exit()
    {
        base.Exit();

        // Включаем анимацию для этого состояния, задаем параметр анимации
        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsMovement, false);
    }
}
