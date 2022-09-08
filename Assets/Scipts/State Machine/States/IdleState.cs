using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    /// <summary>
    /// ���� ������ ����
    /// </summary>
    private float _viewAngleDetection = 110f;
    /// <summary>
    /// ��������� ������ �����
    /// </summary>
    private float _viewDetectionDistance = 12f;
    /// <summary>
    /// ������ �����������, ��� ������� � ����� ������ ���� ������� ������
    /// </summary>
    private float _absoluteDetectionDistance = 3f;

    private Transform _transformPlayer;

    private float _timerUpdate;

    public IdleState(SwordsmanEnemy enemy, StateMachineEnemy stateMachineEnemy) : base(enemy, stateMachineEnemy)
    {

    }

    public override void Enter()
    {
        base.Enter();

        _timerUpdate = 0;

        //_enemy.animator.StopPlayback();

        _transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;

        _enemy.animator.SetBool("isIdle", true);
    }

    public override void Update()
    {
        base.Update();

        _timerUpdate += Time.deltaTime;
        if (_timerUpdate > 0.5)
        {
            float distanceToTarget = Vector3.Distance(_enemy.transform.position, _transformPlayer.position);

            // ������ ���������� �� ��������������, ���� (����� � ���� ���������� ��������� ���������) ��� (����� �������� �����)
            if (distanceToTarget < _absoluteDetectionDistance || _enemy.IsAttacked || IsIsView())
            {
                _stateMachineEnemy.ChangeState(_enemy.pursuitState);
            }

            _timerUpdate = 0;
        }

        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();

        _enemy.animator.SetBool("isIdle", false);
    }

    #region Private methods
    private bool IsIsView()
    {
        float realAngle = Vector3.Angle(_enemy.transform.forward, _transformPlayer.position - _enemy.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(_enemy.transform.position, _transformPlayer.position - _enemy.transform.position, out hit, _viewDetectionDistance))
        {
            if (realAngle < _viewAngleDetection / 2f && Vector3.Distance(_enemy.transform.position, _transformPlayer.position) <= _viewDetectionDistance && hit.transform == _transformPlayer.transform)
            {
                return true;
            }
        }
        return false;
    }
    #endregion Private methods
}
