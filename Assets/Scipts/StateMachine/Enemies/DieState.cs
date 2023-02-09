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
        GlobalGameEventManager.EnemyKilled(enemyUnit);

        enemyUnit?.WeaponController?.MakeWeaponPhysical(true);
        enemyUnit?.RagdollController?.MakePhysical();
        enemyUnit?.HealthBarController?.SetActiveHealthBar(false);
        enemyUnit?.IconEffectsController?.DisableAllActiveIcons();
        enemyUnit?.AudioController?.PlayRandomSoundWithProbability(EnemySoundType.Dead);

        if (enemyUnit.Animator)
            enemyUnit.Animator.enabled = false;

        if (enemyUnit.BurningEffectController)
            enemyUnit.BurningEffectController.enabled = false;

        if (enemyUnit.NavMeshAgent)
            enemyUnit.NavMeshAgent.enabled = false;
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 3 && enemyUnit.DieEffectController != null && enemyUnit.DieEffectController.enabled == false)
        {
            if(enemyUnit)
                enemyUnit.DieEffectController.enabled = true;
        }

        if (_timer > 8)
        {
            // Производим действия перед удалением объекта врага

            // Возращае оружие к объекту врага
            enemyUnit?.WeaponController.MakeWeaponPhysical(false);

            // Удаляем объект врага со сцены
            enemyUnit?.DestroyUnit();
        }
    }
}
