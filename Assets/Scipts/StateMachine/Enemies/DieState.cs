using UnityEngine;

/// <summary>
/// Класс состояния смерти противника
/// </summary>
public class DieState : EnemyState
{
    private float _timer = 0;
    public DieState(Enemy enemy) : base(enemy)
    {
    }
    public override void Enter()
    {
        base.Enter();

        GlobalGameEventManager.EnemyKilled();

        if (enemy.Weapon)
            MakePhysicalWeapon();

        if (enemy.RagdollController)
            enemy.RagdollController.MakePhysical();

        if (enemy.Animator)
            enemy.Animator.enabled = false;

        if (enemy.BurningEffectController)
            enemy.BurningEffectController.enabled = false;

        if (enemy.HealthBarController)
            enemy.HealthBarController.SetActiveHealthBar(false);

        if (enemy.IconEffectsController)
            enemy.IconEffectsController.DeactivateAllIcons();

        if (enemy.NavMeshAgent)
            enemy.NavMeshAgent.enabled = false;

        if (enemy.AudioController)
            enemy.AudioController.PlayRandomSoundWithProbability(EnemySoundType.Dead);
    }

    /// <summary>
    /// Данный метод необходимо вызвать в методе Update в классе наслдеованного от MonoBehaviour. Пищем логику, ту которую хотели бы выполнить в методе Update 
    /// </summary>
    public override void Update()
    {
        base.Update();

        _timer += Time.deltaTime;
        if (_timer > 3 && enemy.DieEffectController != null && enemy.DieEffectController.enabled == false)
        {
           enemy.DieEffectController.enabled = true;
        }

        if (_timer > 8)
        {
            enemy.DestroyEnemyObjects();
        }
    }

    /// <summary>
    /// Метод вызываемый при выходе из состояния
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }

    /// <summary>
    /// Метод отвязывает оружие от модели противника и удаляет его со сцены через некоторое время 
    /// </summary>
    /// <returns></returns>
    private void MakePhysicalWeapon() //TODO Убрать
    {
        enemy.Weapon.transform.parent = null;

        Rigidbody rigidbodyWeapon = enemy.Weapon.GetComponent<Rigidbody>();
        rigidbodyWeapon.isKinematic = false;
    }
}
