using UnityEngine;

/// <summary>
///  Класс состояния покоя противника
/// </summary>
public class IdleState : State
{
    /// <summary>
    /// Угол обзора враг
    /// </summary>
    private float _viewAngleDetection = 120f;
    
    /// <summary>
    /// Дистанция обзора врага
    /// </summary>
    private float _viewDetectionDistance = 14f;
    
    /// <summary>
    /// Радиус обнаружения, при котором в любом случае враг заметит игрока
    /// </summary>
    private float _absoluteDetectionDistance = 6f;
    
    /// <summary>
    /// Таймер обновления
    /// </summary>
    private float _timerUpdate;

    public IdleState(Enemy enemy) : base(enemy)
    {

    }

    public override void Enter()
    {
        _timerUpdate = 0.5f;

        // Получаем Transform игрока для отслеживания его позиции
        transformPlayer = transformPlayer ? transformPlayer : GetTransformPlayer();

        Messenger<int>.AddListener(WaveManager.Event.WAVE_IN_COMMING, SetChasingPlayerState);

        enemy.Animator.SetBool(HashAnimStringEnemy.IsIdle, true);
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

            distanceEnemyToPlayer = GetDistanceEnemyToPlayer();
                
            // Меняем сосстояние на преследеование, если (Игрок в зоне абсолютной дистанции видимости) или (Игрок атаковал врага)
            if (distanceEnemyToPlayer < _absoluteDetectionDistance || IsPlayerInSight())
            {
                // Воспроизводим звук
                if(enemy.AudioController)
                    enemy.AudioController.PlaySound(EnemySoundType.Confused);

                enemy.SetState<ChasingPlayerState>();
            }

            // Обнуляем таймер
            _timerUpdate = 0;
        }
    }

    public override void Exit()
    {
        Messenger<int>.RemoveListener(WaveManager.Event.WAVE_IN_COMMING, SetChasingPlayerState);

        enemy?.Animator?.SetBool(HashAnimStringEnemy.IsIdle, false);
    }

    #region Private methods
    /// <summary>
    /// Метод проверяет находится ли игрок в поле зрения врага
    /// </summary>
    /// <returns></returns>
    private bool IsPlayerInSight()
    {
        float realAngle = Vector3.Angle(enemy.transform.forward, transformPlayer.position - enemy.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(enemy.transform.position, transformPlayer.position - enemy.transform.position, out hit, _viewDetectionDistance))
        {
            if (realAngle < _viewAngleDetection / 2f && Vector3.Distance(enemy.transform.position, transformPlayer.position) <= _viewDetectionDistance && hit.transform == transformPlayer.transform)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Метод обработки события 
    /// </summary>
    /// <param name="wave"></param>
    private void SetChasingPlayerState(int wave)
    {
        enemy.SetState<ChasingPlayerState>();
    }
    #endregion Private methods
}
