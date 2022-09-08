using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "new AttackState", menuName = "Enemy States/AttackState", order = 1)]
public class AttackState : State
{
    /// <summary>
    /// Дистанция атаки противника в метрах
    /// </summary>
    private float _attackDistance;
    /// <summary>
    /// Частота атаки, т.е. задержка между атаками в секундах
    /// </summary>
    private float _attackFrequency = 3.5f;
    private float _attackTimeAnimation;

    private Transform _transformPlayer;

    private float _rotationSpeedToTarget = 2.5f;

    private float _timer;
    private float _timerUpdate;

    public AttackState(SwordsmanEnemy enemy, StateMachineEnemy stateMachineEnemy) : base(enemy, stateMachineEnemy)
    {

    }
    public override void Enter()
    {
        base.Enter();

        // Обнуляем таймеры
        _timer = 0;
        _timerUpdate = 0;

         // Рандомизируем частоту атаки, делаем ее немного хаотичной
        _attackFrequency = Random.Range(_attackFrequency - 0.5f, _attackFrequency + 0.5f);

        // Устанавливаем дистанцию атаки
        _attackDistance = _enemy.navMeshAgent.stoppingDistance + 0.1f;
        // Включаем анимацию
        _enemy.animator.SetBool("isIdleAttacking", true);
        // Получаем transform игрока для использования его в дальнейшем
        _transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;

    }

    public override void Update()
    {
        base.Update();

        // Атака с определенной частотой
        // -------------------------------------------------------------------------------
        _timer += Time.deltaTime;
        if (_timer > _attackFrequency)
        {
            // Включаем анимацию атаки, тем самым атакуем
            _enemy.animator.SetTrigger("isAttacking");
            _timer = 0;
        }
        // -------------------------------------------------------------------------------

        // Делаем частоту обновления дистацнии не каждый кадр, а раз в пол секунды
        // -------------------------------------------------------------------------------
        _timerUpdate += Time.deltaTime;
        if (_timerUpdate > 0.5f)
        {
            // Определяем дистанцию до игрока
            float distanceToTarget = Vector3.Distance(_enemy.transform.position, _transformPlayer.position);
            if (distanceToTarget > _attackDistance)
            {
                // Если анимация атаки не выполняется
                if (!_enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Melee Attack 1"))
                {
                    _stateMachineEnemy.ChangeState(_enemy.pursuitState);
                }
            }
            _timerUpdate = 0;
        }
        // -------------------------------------------------------------------------------
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //_enemy.transform.LookAt(_transformPlayer);
        LookAtTarget();
    }

    public override void Exit()
    {
        base.Exit();

        _enemy.animator.SetBool("isIdleAttacking", false);

    }

    private void LookAtTarget()
    {
        Vector3 direction = -(_enemy.transform.position - _transformPlayer.position);
        _enemy.transform.rotation = Quaternion.Lerp(_enemy.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _rotationSpeedToTarget);
    }
}
