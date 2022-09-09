using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitState : State
{
    private float _attackDistance = 2.5f;
    private Transform _transformPlayer;

    private float _timerUpdate;

    public PursuitState(SwordsmanEnemy enemy, StateMachineEnemy stateMachineEnemy) : base(enemy, stateMachineEnemy)
    {

    }
    public override void Enter()
    {
        base.Enter();

        _timerUpdate = 0;

        _transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;

        _enemy.Animator.SetBool("isMovement", true);
    }

    public override void Update()
    {
        base.Update();

        _timerUpdate += Time.deltaTime;
        if (_timerUpdate > 0.5f)
        {
            float distance = Vector3.Distance(_enemy.transform.position, _transformPlayer.position);

            if (distance < _attackDistance)
            {
                _stateMachineEnemy.ChangeState(_enemy.attackState);
            }

            _timerUpdate = 0;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        _enemy.NavMeshAgent.SetDestination(_transformPlayer.position);

        _enemy.Animator.SetFloat("Speed", _enemy.CurrentSpeed / _enemy.Speed);
    }

    public override void Exit()
    {
        base.Exit();

        _enemy.Animator.SetBool("isMovement", false);
    }
}
