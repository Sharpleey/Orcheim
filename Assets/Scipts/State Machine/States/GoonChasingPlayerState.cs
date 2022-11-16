using UnityEngine;
using UnityEngine.AI;

public class GoonChasingPlayerState : ChasingPlayerState
{
    /// <summary>
    /// Таймер проверки союзных существ
    /// </summary>
    private float _timerCheckAlliesNearby;

    /// <summary>
    /// Маска слоев с одним слоем Enemy, который мы будем искать
    /// </summary>
    private LayerMask collisionMask = 4096;

    public GoonChasingPlayerState(Enemy enemy) : base(enemy)
    {

    }
    public override void Enter()
    {
        base.Enter();

        // Обнуляем таймер
        _timerCheckAlliesNearby = 0;

        // Включаем анимацию для этого состояния, задаем параметр анимации
        enemy.Animator.SetBool(HashAnimStringEnemy.IsMovement, true);
    }

    public override void Update()
    {
        base.Update();

        _timerCheckAlliesNearby += Time.deltaTime;

        if (_timerCheckAlliesNearby > 3f)
        {
            // Получаем кол-во союзных существ поблизости
            int countAlliesNearby = GetCountAlliesNearby();

            // Если их больше 2х, то кастуем баф
            if (countAlliesNearby > 2 && !((Goon)enemy).IsWarcryInCooldown)
            {
                enemy.SetState<InspirationState>();
            }

            // Обнуляем таймер
            _timerCheckAlliesNearby = 0;
        }

        // Если противник подошел на дистанцию атаки (_attackDistance), то изменяем состояние
        if (distanceEnemyToPlayer < enemy.AttackDistance)
        {
            // Изменяем состояние на состояние атаки
            enemy.SetState<GoonAttackState>();
        }
    }

    public override void Exit()
    {
        base.Exit();

        // Включаем анимацию для этого состояния, задаем параметр анимации
        enemy.Animator.SetBool(HashAnimStringEnemy.IsMovement, false);
    }

    private int GetCountAlliesNearby()
    {
        Vector3 center = enemy.transform.position;
        float radius = 8;

        Collider[] hitColliders = Physics.OverlapSphere(center, radius, collisionMask);

        return hitColliders.Length;

    }
}
