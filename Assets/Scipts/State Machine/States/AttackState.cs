using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "new AttackState", menuName = "Enemy States/AttackState", order = 1)]
public class AttackState : State
{
    /// <summary>
    /// ��������� ����� ���������� � ������
    /// </summary>
    private float _attackDistance;
    /// <summary>
    /// ������� �����, �.�. �������� ����� ������� � ��������
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

        // �������� �������
        _timer = 0;
        _timerUpdate = 0;

         // ������������� ������� �����, ������ �� ������� ���������
        _attackFrequency = Random.Range(_attackFrequency - 0.5f, _attackFrequency + 0.5f);

        // ������������� ��������� �����
        _attackDistance = _enemy.navMeshAgent.stoppingDistance + 0.1f;
        // �������� ��������
        _enemy.animator.SetBool("isIdleAttacking", true);
        // �������� transform ������ ��� ������������� ��� � ����������
        _transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;

    }

    public override void Update()
    {
        base.Update();

        // ����� � ������������ ��������
        // -------------------------------------------------------------------------------
        _timer += Time.deltaTime;
        if (_timer > _attackFrequency)
        {
            // �������� �������� �����, ��� ����� �������
            _enemy.animator.SetTrigger("isAttacking");
            _timer = 0;
        }
        // -------------------------------------------------------------------------------

        // ������ ������� ���������� ��������� �� ������ ����, � ��� � ��� �������
        // -------------------------------------------------------------------------------
        _timerUpdate += Time.deltaTime;
        if (_timerUpdate > 0.5f)
        {
            // ���������� ��������� �� ������
            float distanceToTarget = Vector3.Distance(_enemy.transform.position, _transformPlayer.position);
            if (distanceToTarget > _attackDistance)
            {
                // ���� �������� ����� �� �����������
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
