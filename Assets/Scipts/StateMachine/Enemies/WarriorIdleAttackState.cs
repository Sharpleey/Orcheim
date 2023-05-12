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

    public WarriorIdleAttackState(EnemyUnit enemyUnit) : base(enemyUnit)
    {

    }
    public override void Enter()
    {
        // Обнуляем таймеры
        _timerAttack = 0;
        _timerUpdateDistance = 0.5f;

        // Определяем частоту атак в зависимости от скорости атаки
        _attackFrequency = 3.5f /  (enemyUnit.AttackSpeed.Actual / 100f);

        // Определяем дистанцию до игрока
        distanceEnemyToTarget = Vector3.Distance(enemyUnit.transform.position, enemyUnit.TargetUnit.transform.position);

        // Включаем анимацию
        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsIdleAttack, true);
    }

    public override void Update()
    {
        // Атака с определенной частотой
        // -------------------------------------------------------------------------------
        _timerAttack += Time.deltaTime;
        if (_timerAttack > _attackFrequency)
        {
            enemyUnit.SetState<WarriorAttackState>();
        }
        // -------------------------------------------------------------------------------


        // Делаем частоту обновления дистацнии не каждый кадр, а раз в пол секунды
        // -------------------------------------------------------------------------------
        _timerUpdateDistance += Time.deltaTime;
        if (_timerUpdateDistance > 0.5f)
        {
            // Определяем дистанцию до игрока
            distanceEnemyToTarget = Vector3.Distance(enemyUnit.transform.position, enemyUnit.TargetUnit.transform.position);
            if (distanceEnemyToTarget > enemyUnit.AttackDistance)
            {
                enemyUnit.SetState<ChasingState>();
            }

            _timerUpdateDistance = 0;
        }
        // -------------------------------------------------------------------------------

        LookAtTarget();
    }

    public override void Exit()
    {
        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsIdleAttack, false);
    }

    /// <summary>
    /// Метод плавно поворачивает с определенной скоростью врага к игроку
    /// </summary>
    private void LookAtTarget()
    {
        Vector3 direction = -(enemyUnit.transform.position - enemyUnit.TargetUnit.transform.position);
        enemyUnit.transform.rotation = Quaternion.Lerp(enemyUnit.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _rotationSpeedToTarget);
    }
}
