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
    private float _absoluteDetectionDistance = 4f;

    private float _distanceToTarget;

    private Transform _transformPlayer;

    private float _timerUpdate;

    public IdleState(Enemy enemy) : base(enemy)
    {

    }

    public override void Enter()
    {
        base.Enter();

        _timerUpdate = 0;

        //_enemy.animator.StopPlayback();

        _transformPlayer = UnityUtility.FindGameObjectTransformWithTag("Player");

        Messenger<int>.AddListener(GlobalGameEvent.WAVE_IN_COMMING, PursuitPlayer);

        _enemy.Animator.SetBool(HashAnimation.IsIdle, true);
    }

    public override void Update()
    {
        base.Update();

        _timerUpdate += Time.deltaTime;
        if (_timerUpdate > 0.5)
        {
            // �� ������, ����� ����� ��� �� �����������
            if (!_transformPlayer)
                _transformPlayer = UnityUtility.FindGameObjectTransformWithTag("Player");

            _distanceToTarget = Vector3.Distance(_enemy.transform.position, _transformPlayer.position);
                
            // ������ ���������� �� ��������������, ���� (����� � ���� ���������� ��������� ���������) ��� (����� �������� �����)
            if (_distanceToTarget < _absoluteDetectionDistance || IsIsView())
            {
                _enemy.SetPursuitState();
            }

            _timerUpdate = 0;
        }
    }

    public override void Exit()
    {
        base.Exit();

        Messenger<int>.RemoveListener(GlobalGameEvent.WAVE_IN_COMMING, PursuitPlayer);

        _enemy?.Animator?.SetBool(HashAnimation.IsIdle, false);
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

    private void PursuitPlayer(int wave)
    {
        _enemy.SetPursuitState();
    }
    #endregion Private methods
}
