using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "new AttackState", menuName = "Enemy States/AttackState", order = 1)]
public class AttackIdleState : State
{
    /// <summary>
    /// Дистанция атаки противника в метрах
    /// </summary>
    private float _attackDistance = 2.5f;
    /// <summary>
    /// Частота атаки, т.е. задержка между атаками в секундах
    /// </summary>
    private float _attackFrequency = 3.5f;
    private float _currentAttackFrequency = 3.5f;
    /// <summary>
    /// Скорость поворота врага к цели
    /// </summary>
    private float _rotationSpeedToTarget = 2.5f;
    /// <summary>
    /// Дистанция от противника до игрока
    /// </summary>
    private float _distanceFromEnemyToPlayer;

    private Transform _transformPlayer;

    private AnimatorStateInfo _animatorStateInfo;

    private float _timer;
    private float _timerUpdate;

    public AttackIdleState(Enemy enemy) : base(enemy)
    {

    }
    public override void Enter()
    {
        base.Enter();

        // Обнуляем таймеры
        _timer = 0;
        _timerUpdate = 0;

        // Рандомизируем частоту атаки, делаем ее немного хаотичной
        _currentAttackFrequency = Random.Range(0.2f, 1.0f);

        // Устанавливаем дистанцию атаки
        _attackDistance = _enemy.NavMeshAgent.stoppingDistance + 0.1f;
        // Включаем анимацию
        _enemy.Animator.SetBool(HashAnimation.IsIdleAttacking, true);
        // Получаем transform игрока для использования его в дальнейшем
        _transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Update()
    {
        base.Update();

        // Атака с определенной частотой
        // -------------------------------------------------------------------------------
        _timer += Time.deltaTime;
        if (_timer > _currentAttackFrequency)
        {
            // Включаем анимацию атаки, тем самым атакуем
            _enemy.Animator.SetTrigger(HashAnimation.IsAttacking);

            // Рандомизируем частоту атаки, делаем ее немного хаотичной
            _currentAttackFrequency = Random.Range(_attackFrequency - 0.8f, _attackFrequency + 0.8f);

            _timer = 0;
        }
        // -------------------------------------------------------------------------------


        // Делаем частоту обновления дистацнии не каждый кадр, а раз в пол секунды
        // -------------------------------------------------------------------------------
        _timerUpdate += Time.deltaTime;
        if (_timerUpdate > 0.5f)
        {
            // Определяем дистанцию до игрока
            _distanceFromEnemyToPlayer = Vector3.Distance(_enemy.transform.position, _transformPlayer.position);
            _animatorStateInfo = _enemy.Animator.GetCurrentAnimatorStateInfo(0);
            if (_distanceFromEnemyToPlayer > _attackDistance && _animatorStateInfo.nameHash != HashAnimation.MeleeAttack1)
            {
                _enemy.SetPursuitState();
            }
            _timerUpdate = 0;
        }
        // -------------------------------------------------------------------------------

        LookAtTarget();
    }

    public override void Exit()
    {
        base.Exit();

        _enemy.Animator.SetBool(HashAnimation.IsIdleAttacking, false);
    }

    /// <summary>
    /// Метод плавно поворачивает с определенной скоростью врага к игроку
    /// </summary>
    private void LookAtTarget()
    {
        Vector3 direction = -(_enemy.transform.position - _transformPlayer.position);
        _enemy.transform.rotation = Quaternion.Lerp(_enemy.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _rotationSpeedToTarget);
    }
}
