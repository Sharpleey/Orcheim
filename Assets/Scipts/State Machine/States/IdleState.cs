using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private float _playerDetectionDistance = 10f;
    private Transform _transformPlayer;

    public IdleState(SwordsmanEnemy enemy, StateMachineEnemy stateMachineEnemy) : base(enemy, stateMachineEnemy)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _enemy.animator.StopPlayback();

        _transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Update()
    {
        base.Update();
        _enemy.animator.SetFloat("Speed", _enemy.CurrentSpeed/_enemy.Speed);

        float distance = Vector3.Distance(_enemy.transform.position, _transformPlayer.position);

        if (distance < _playerDetectionDistance)
        {
            _stateMachineEnemy.ChangeState(_enemy.pursuitState);
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
