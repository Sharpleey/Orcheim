using UnityEngine;

public class GoonAttackState : EnemyState
{
    /// <summary>
    /// Кол-во вариантов атак
    /// </summary>
    private int _attackVariantCount = 2;

    /// <summary>
    /// Скорость поворота врага к цели
    /// </summary>
    private float _rotationSpeedToTarget = 2.5f;

    /// <summary>
    /// Таймер между обновлениями дистации
    /// </summary>
    private float _timerRotateToPlayer;


    public GoonAttackState(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        // Обнуляем таймеры
        _timerRotateToPlayer = 0;

        // Получаем transform игрока для использования его в дальнейшем
        transformPlayer = transformPlayer ? transformPlayer : GetTransformPlayer();

        // Включаем анимацию
        enemy.Animator.SetInteger(HashAnimStringEnemy.AttackVariant, Random.Range(0, _attackVariantCount));
        enemy.Animator.SetTrigger(HashAnimStringEnemy.IsAttack);
    }

    public override void Update()
    {
        _timerRotateToPlayer += Time.deltaTime;
        if (_timerRotateToPlayer < 1f)
        {
            LookAtTarget();
        }
    }

    /// <summary>
    /// Метод плавно поворачивает с определенной скоростью врага к игроку
    /// </summary>
    private void LookAtTarget()
    {
        Vector3 direction = -(enemy.transform.position - transformPlayer.position);
        enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _rotationSpeedToTarget);
    }
}
