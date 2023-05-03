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

        // Получаем Transform игрока для отслеживания его позиции
        transformPlayer = transformPlayer ? transformPlayer : GetTransformPlayer();

        enemyUnit.Animator.SetBool(HashAnimStringEnemy.IsIdle, true);

        // Включием триггер отвечающий за призыв атаковать игрока
        enemyUnit.SummonTrigger?.SetEnable(true);
    }

    public override void Update()
    {
        _timerUpdate += Time.deltaTime;

        if (_timerUpdate > 0.5f)
        {
            // На случай, когда игрок еще не заспавнился
            if (!transformPlayer)
            {
                transformPlayer = GetTransformPlayer();
                return;
            }

            distanceEnemyToPlayer = Vector3.Distance(enemyUnit.transform.position, transformPlayer.position);

            // Меняем сосстояние на преследеование, если (Игрок в зоне абсолютной дистанции видимости) или (Персонаж противника увидел игрока перед собой)
            if (distanceEnemyToPlayer < _absoluteDetectionDistance || IsPlayerInSight())
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
    private bool IsPlayerInSight()
    {
        float realAngle = Vector3.Angle(enemyUnit.transform.forward, transformPlayer.position - enemyUnit.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(enemyUnit.transform.position, transformPlayer.position - enemyUnit.transform.position, out hit, _viewDetectionDistance))
        {
            if (realAngle < _viewAngleDetection / 2f && Vector3.Distance(enemyUnit.transform.position, transformPlayer.position) <= _viewDetectionDistance && hit.transform == transformPlayer.transform)
            {
                return true;
            }
        }
        return false;
    }

    #endregion Private methods
}
