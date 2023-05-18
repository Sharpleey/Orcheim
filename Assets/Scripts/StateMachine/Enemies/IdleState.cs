using UnityEngine;

/// <summary>
///  Класс состояния покоя противника
/// </summary>
public class IdleState : EnemyState
{
    /// <summary>
    /// Угол обзора враг
    /// </summary>
    protected float _viewAngleDetection = 120f;

    /// <summary>
    /// Дистанция обзора врага
    /// </summary>
    protected float _viewDetectionDistance = 14f;

    /// <summary>
    /// Радиус обнаружения, при котором в любом случае враг заметит игрока
    /// </summary>
    protected float _absoluteDetectionDistance = 4f;
    
    /// <summary>
    /// Таймер обновления
    /// </summary>
    protected float _timerUpdate;

    public IdleState(EnemyUnit enemyUnit) : base(enemyUnit)
    {

    }

    public override void Enter()
    {
        _timerUpdate = 0.5f;

        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsIdle, true);

        // Включием триггер отвечающий за призыв атаковать цель
        enemyUnit.SummonTrigger?.SetEnable(true);
    }

    public override void Update()
    {
        _timerUpdate += Time.deltaTime;

        if (_timerUpdate > 0.5f && enemyUnit.TargetUnit != null)
        {
            distanceEnemyToTarget = Vector3.Distance(enemyUnit.transform.position, enemyUnit.TargetUnit.transform.position);

            // Меняем сосстояние на преследеование, если (Игрок в зоне абсолютной дистанции видимости) или (Персонаж противника увидел игрока перед собой)
            if (distanceEnemyToTarget < _absoluteDetectionDistance || IsTargetInSight())
            {
                // Воспроизводим звук
                enemyUnit?.AudioController?.PlayRandomSoundWithProbability(EnemySoundType.Confused);

                enemyUnit.SetState<ChasingState>();
            }

            // Обнуляем таймер
            _timerUpdate = 0;
        }
    }

    public override void Exit()
    {
        enemyUnit?.Animator?.SetBool(HashAnimStringEnemy.IsIdle, false);

        // Выключием триггер отвечающий за призыа атаковать рядом стоящих союзных юнитов
        enemyUnit.SummonTrigger?.SetEnable(false);
    }

    #region Private methods

    /// <summary>
    /// Метод проверяет находится ли игрок в поле зрения врага
    /// </summary>
    /// <returns></returns>
    private bool IsTargetInSight()
    {
        float realAngle = Vector3.Angle(enemyUnit.transform.forward, enemyUnit.TargetUnit.transform.position - enemyUnit.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(enemyUnit.transform.position, enemyUnit.TargetUnit.transform.position - enemyUnit.transform.position, out hit, _viewDetectionDistance))
        {
            if (realAngle < _viewAngleDetection / 2f && Vector3.Distance(enemyUnit.transform.position, enemyUnit.TargetUnit.transform.position) <= _viewDetectionDistance && hit.transform == enemyUnit.TargetUnit.transform)
            {
                return true;
            }
        }
        return false;
    }

    #endregion Private methods
}
