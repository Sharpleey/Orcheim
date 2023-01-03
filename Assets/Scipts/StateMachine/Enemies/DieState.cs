using UnityEngine;

/// <summary>
/// Класс состояния смерти противника
/// </summary>
public class DieState : EnemyState
{
    private float _timer = 0;
    public DieState(EnemyUnit enemyUnit) : base(enemyUnit)
    {
    }
    public override void Enter()
    {
        GlobalGameEventManager.EnemyKilled();

        if (enemyUnit.WeaponController)
            enemyUnit.WeaponController.MakeWeaponPhysical(true);

        if (enemyUnit.RagdollController)
            enemyUnit.RagdollController.MakePhysical();

        if (enemyUnit.Animator)
            enemyUnit.Animator.enabled = false;

        if (enemyUnit.BurningEffectController)
            enemyUnit.BurningEffectController.enabled = false;

        if (enemyUnit.HealthBarController)
            enemyUnit.HealthBarController.SetActiveHealthBar(false);

        if (enemyUnit.IconEffectsController)
            enemyUnit.IconEffectsController.DeactivateAllIcons();

        if (enemyUnit.NavMeshAgent)
            enemyUnit.NavMeshAgent.enabled = false;

        if (enemyUnit.AudioController)
            enemyUnit.AudioController.PlayRandomSoundWithProbability(EnemySoundType.Dead);
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 3 && enemyUnit.DieEffectController != null && enemyUnit.DieEffectController.enabled == false)
        {
            enemyUnit.DieEffectController.enabled = true;
        }

        if (_timer > 8)
        {
            // Производим действия перед удалением объекта врага

            // Возращае оружие к объекту врага
            if (enemyUnit.WeaponController)
                enemyUnit.WeaponController.MakeWeaponPhysical(false);

            // Удаляем объект врага со сцены
            enemyUnit.DestroyUnit();
        }
    }
}
