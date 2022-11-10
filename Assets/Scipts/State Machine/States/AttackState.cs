using UnityEngine;

/// <summary>
/// Класс состояния атаки проивника. В этом состоянии противник чередует анимацию стойки перед атакой с анимацией атаки 
/// </summary>
public class AttackState : State
{
    /// <summary>
    /// Дистанция атаки противника в метрах
    /// </summary>
    private float _attackDistance = 2.5f;
   
    /// <summary>
    /// Частота атаки, т.е. задержка между атаками в секундах
    /// </summary>
    private float _attackFrequency = 3.5f;

    /// <summary>
    /// Текущая задержка перед атакой
    /// </summary>
    private float _currentAttackFrequency = 3.5f;
    
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

    public AttackState(Enemy enemy) : base(enemy)
    {

    }
    public override void Enter()
    {
        base.Enter();

        // Обнуляем таймеры
        _timerAttack = 0;
        _timerUpdateDistance = 0;

        // Рандомизируем частоту атаки, делаем ее немного хаотичной
        _currentAttackFrequency = Random.Range(0.2f, 1.0f);

        // Устанавливаем дистанцию атаки
        _attackDistance = enemy.NavMeshAgent.stoppingDistance + 0.1f;
        // Включаем анимацию
        enemy.Animator.SetBool(HashAnimation.IsIdleAttacking, true);
        // Получаем transform игрока для использования его в дальнейшем
        transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Update()
    {
        base.Update();

        // Атака с определенной частотой
        // -------------------------------------------------------------------------------
        _timerAttack += Time.deltaTime;
        if (_timerAttack > _currentAttackFrequency)
        {
            // Включаем анимацию атаки, тем самым атакуем
            enemy.Animator.SetTrigger(HashAnimation.IsAttacking);

            // Рандомизируем частоту атаки, делаем ее немного хаотичной
            _currentAttackFrequency = Random.Range(_attackFrequency - 0.8f, _attackFrequency + 0.8f);

            _timerAttack = 0;
        }
        // -------------------------------------------------------------------------------


        // Делаем частоту обновления дистацнии не каждый кадр, а раз в пол секунды
        // -------------------------------------------------------------------------------
        _timerUpdateDistance += Time.deltaTime;
        if (_timerUpdateDistance > 0.5f)
        {
            // Определяем дистанцию до игрока
            distanceEnemyToPlayer = GetDistanceEnemyToPlayer();
            if (distanceEnemyToPlayer > _attackDistance && !enemy.IsBlockChangeState)
            {
                enemy.SetState<ChasingPlayerState>();
            }
            _timerUpdateDistance = 0;
        }
        // -------------------------------------------------------------------------------

        LookAtTarget();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.Animator.SetBool(HashAnimation.IsIdleAttacking, false);
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
