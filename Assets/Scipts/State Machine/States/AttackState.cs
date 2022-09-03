using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private float _attackDistance = 2.5f;
    private float _attackFrequency = 1f;

    private Transform _transformPlayer;

    private float _timer;

    public AttackState(Enemy1 enemy, StateMachineEnemy stateMachineEnemy) : base(enemy, stateMachineEnemy)
    {

    }
    public override void Enter()
    {
        base.Enter();

        _timer = 0;

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

        //if (!_enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Melee Attack 1"))
        //{

        //}

            float distance = Vector3.Distance(_enemy.transform.position, _transformPlayer.position);
        if (distance > _attackDistance)
        {
            _stateMachineEnemy.ChangeState(_enemy.pursuitState);
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

    private bool AnimatorIsPlaying()
    {
        return _enemy.animator.GetCurrentAnimatorStateInfo(0).length >
               _enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
