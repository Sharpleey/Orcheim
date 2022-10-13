using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    /// <summary>
    /// Угол обзора враг
    /// </summary>
    private float _viewAngleDetection = 110f;
    /// <summary>
    /// Дистанция обзора врага
    /// </summary>
    private float _viewDetectionDistance = 12f;
    /// <summary>
    /// Радиус обнаружения, при котором в любом случае враг заметит игрока
    /// </summary>
    private float _absoluteDetectionDistance = 4f;

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

        _transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;

        _enemy.Animator.SetBool("isIdle", true);

        if(_enemy.IsStartPursuitState)
            _enemy.ChangeState(_enemy.PursuitState);
    }

    public override void Update()
    {
        base.Update();

        _timerUpdate += Time.deltaTime;
        if (_timerUpdate > 0.5)
        {
            float distanceToTarget = Vector3.Distance(_enemy.transform.position, _transformPlayer.position);

            // Меняем сосстояние на преследеование, если (Игрок в зоне абсолютной дистанции видимости) или (Игрок атаковал врага)
            if (distanceToTarget < _absoluteDetectionDistance || IsIsView())
            {
                if (!_enemy.IsOnlyIdleState)
                    _enemy.ChangeState(_enemy.PursuitState);
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

        _enemy.Animator.SetBool("isIdle", false);
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
