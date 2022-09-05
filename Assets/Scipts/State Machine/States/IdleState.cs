using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    /// <summary>
    /// Угол обзора враг
    /// </summary>
    private float _viewAngleDetection = 90f;
    /// <summary>
    /// Дистанция обзора врага
    /// </summary>
    private float _viewDetectionDistance = 10f;
    /// <summary>
    /// Радиус обнаружения, при котором в любом случае враг заметит игрока
    /// </summary>
    private float _absoluteDetectionDistance = 3f;

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

        float distanceToTarget = Vector3.Distance(_enemy.transform.position, _transformPlayer.position);

        // Меняем сосстояние на преследеование, если (Игрок в зоне абсолютной дистанции видимости) или (Игрок атаковал врага)
        if (distanceToTarget < _absoluteDetectionDistance || _enemy.IsAttacked || IsIsView())
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
