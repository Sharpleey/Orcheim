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
        GlobalGameEventManager.EnemyKilled();

        if (enemy.WeaponController)
            enemy.WeaponController.MakeWeaponPhysical(true);

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

    public override void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 3 && enemy.DieEffectController != null && enemy.DieEffectController.enabled == false)
        {
            enemy.DieEffectController.enabled = true;
        }

        if (_timer > 8)
        {
            // Производим действия перед удалением объекта врага

            // Возращае оружие к объекту врага
            if (enemy.WeaponController)
                enemy.WeaponController.MakeWeaponPhysical(false);

            // Удаляем объекь врага со сцены
            //enemy.DestroyEnemy();
        }
    }
}
