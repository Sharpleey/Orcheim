using UnityEngine;

/// <summary>
/// Класс состояния атаки проивника. В этом состоянии противник чередует анимацию стойки перед атакой с анимацией атаки 
/// </summary>
public class WarriorIdleAttackState : EnemyState
{  
    /// <summary>
    /// Частота атаки, т.е. задержка между атаками в секундах
    /// </summary>
    private float _attackFrequency = 3.5f;
    
    /// <summary>
    /// Скорость поворота врага к цели
    /// </summary>
    private float _rotationSpeedToTarget = 2.5f;

    /// <summary>
    /// Таймер задержки между атаками
    /// </summary>
    private float _timerAttack;
    
    /// <summary>
    /// Таймер между обновлениями дистации
    /// </summary>
    private float _timerUpdateDistance;

    public WarriorIdleAttackState(Enemy enemy) : base(enemy)
    {

    }
    public override void Enter()
    {
        // Обнуляем таймеры
        _timerAttack = 0;
        _timerUpdateDistance = 0.5f;

        // Получаем transform игрока для использования его в дальнейшем
        transformPlayer = transformPlayer ? transformPlayer : GetTransformPlayer();

        // Определяем дистанцию до игрока
        distanceEnemyToPlayer = GetDistanceEnemyToPlayer();

        // Включаем анимацию
        enemy.Animator.SetBool(HashAnimStringEnemy.IsIdleAttack, true);
    }

    public override void Update()
    {
        // Атака с определенной частотой
        // -------------------------------------------------------------------------------
        _timerAttack += Time.deltaTime;
        if (_timerAttack > _attackFrequency)
        {
            enemy.SetState<WarriorAttackState>();
        }
        // -------------------------------------------------------------------------------


        // Делаем частоту обновления дистацнии не каждый кадр, а раз в пол секунды
        // -------------------------------------------------------------------------------
        _timerUpdateDistance += Time.deltaTime;
        if (_timerUpdateDistance > 0.5f)
        {
            // Определяем дистанцию до игрока
            distanceEnemyToPlayer = GetDistanceEnemyToPlayer();
            if (distanceEnemyToPlayer > enemy.AttackDistance)
            {
                enemy.SetState<ChasingState>();
            }

            _timerUpdateDistance = 0;
        }
        // -------------------------------------------------------------------------------

        LookAtTarget();
    }

    public override void Exit()
    {
        enemy.Animator.SetBool(HashAnimStringEnemy.IsIdleAttack, false);
    }

    /// <summary>
    /// Метод плавно поворачивает с определенной скоростью врага к игроку
    /// </summary>
    private void LookAtTarget()
    {
        Vector3 direction = -(enemy.transform.position - transformPlayer.position);
        enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _rotationSpeedToTarget);
    }
}
