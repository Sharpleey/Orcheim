using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private float _attackDistance;
    private float _attackFrequency = 3f;
    private float _attackTimeAnimation;

    private Transform _transformPlayer;

    private float _timer;

    public AttackState(SwordsmanEnemy enemy, StateMachineEnemy stateMachineEnemy) : base(enemy, stateMachineEnemy)
    {

    }
    public override void Enter()
    {
        base.Enter();

        _timer = 0;

        _attackDistance = _enemy.navMeshAgent.stoppingDistance;
        _enemy.animator.SetBool("isIdleAttacking", true);

        _transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;

    }

    public override void Update()
    {
        base.Update();

        _timer += Time.deltaTime;
        if (_timer > _attackFrequency)
        {
            _enemy.animator.SetTrigger("isAttacking");
            _timer = 0;
        }
        
        float distanceToTarget = Vector3.Distance(_enemy.transform.position, _transformPlayer.position);
        if (distanceToTarget > _attackDistance)
        {
            // Если анимация атаки не выполняется
            if (!_enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Melee Attack 1"))
            {
                _stateMachineEnemy.ChangeState(_enemy.pursuitState);
            }
        }

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        _enemy.transform.LookAt(_transformPlayer);
    }

    public override void Exit()
    {
        base.Exit();

        _enemy.animator.SetBool("isIdleAttacking", false);

    }
}
