using UnityEngine;

/// <summary>
/// Класс состояния атаки проивника. В этом состоянии противник чередует анимацию стойки перед атакой с анимацией атаки 
/// </summary>
public class WarriorAttackState : State
{  
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

    public WarriorAttackState(Enemy enemy) : base(enemy)
    {

    }
    public override void Enter()
    {
        // Обнуляем таймеры
        _timerAttack = 0;
        _timerUpdateDistance = 0.5f;

        // Рандомизируем частоту атаки, делаем ее немного хаотичной
        _currentAttackFrequency = Random.Range(0.2f, 1.0f);

        // Получаем transform игрока для использования его в дальнейшем
        transformPlayer = transformPlayer ? transformPlayer : GetTransformPlayer();

        // Включаем анимацию
        enemy.Animator.SetBool(HashAnimStringEnemy.IsIdleAttack, true);
    }

    public override void Update()
    {
        // Атака с определенной частотой
        // -------------------------------------------------------------------------------
        _timerAttack += Time.deltaTime;
        if (_timerAttack > _currentAttackFrequency)
        {
            //Воспроизводим звук
            if (enemy.AudioController)
                enemy.AudioController.PlaySound(EnemySoundType.Atttack);

            // Включаем анимацию атаки, тем самым атакуем
            enemy.Animator.SetTrigger(HashAnimStringEnemy.IsAttack_1);

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
            if (distanceEnemyToPlayer > enemy.AttackDistance && !enemy.IsBlockChangeState)
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
