using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitState : State
{
    private float _attackDistance = 2.5f;
    private Transform _transformPlayer;

    public PursuitState(Enemy1 enemy, StateMachineEnemy stateMachineEnemy) : base(enemy, stateMachineEnemy)
    {

    }
    public override void Enter()
    {
        base.Enter();

        _transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Update()
    {
        base.Update();

        _enemy.navMeshAgent.SetDestination(_transformPlayer.position);

        _enemy.animator.SetFloat("Speed", _enemy.CurrentSpeed / _enemy.Speed);

        float distance = Vector3.Distance(_enemy.transform.position, _transformPlayer.position);

        if (distance < _attackDistance)
        {
            _stateMachineEnemy.ChangeState(_enemy.attackState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
