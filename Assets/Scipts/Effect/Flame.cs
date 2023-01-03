using UnityEngine;

public class Flame : Effect, IArmorUnitDecrease
{
    public override string Name => "Горение";
    public override string Description => "Эффект наносит персонажу урон каждые...";

    public override EffectType EffectType => EffectType.Negative;
    public override float Duration { get; set; } = 3;
    public override float Frequency { get; set; } = 1f;

    public Damage Damage { get; private set; } = new Damage(10, 2, DamageType.Fire, false);

    public int ArmorDecrease { get; set; } = 1;

    public override void Enable()
    {
        EnemyUnit? enemyUnit = unit as EnemyUnit;

        if(enemyUnit)
        {
            // Включаем иконку горения над противником
            if (enemyUnit.IconEffectsController)
                enemyUnit.IconEffectsController.SetActiveIconBurning(true);

            // Включаем визульный эффект горения
            if (enemyUnit.BurningEffectController)
                enemyUnit.BurningEffectController.enabled = true;
        }
       
    }

    public override void Tick()
    {
        unit.TakeDamage(Damage);
    }

    public override void Disable()
    {
        EnemyUnit? enemyUnit = unit as EnemyUnit;

        if (enemyUnit)
        {
            // Выключаем иконку горения над противником
            if (enemyUnit.IconEffectsController)
                enemyUnit.IconEffectsController.SetActiveIconBurning(false);

            // Выключаем визульный эффект горения
            if (enemyUnit.BurningEffectController)
                enemyUnit.BurningEffectController.enabled = false;
        }
    }
}
