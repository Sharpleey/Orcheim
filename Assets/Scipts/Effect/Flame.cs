using UnityEngine;

public class Flame : Effect, IArmorUnitDecrease
{
    public override string Name => "Горение";
    public override string Description => "Эффект наносит персонажу урон каждые...";

    public override EffectType EffectType => EffectType.Negative;
    public override float Duration { get; set; } = 3;
    public override float Frequency { get; set; } = 1f;

    public Damage Damage { get; private set; } = new Damage(10, 2, DamageType.Fire, false, "Урон в секунду от горения");

    public int ArmorDecrease { get; set; } = 1;

    public override void Enable()
    {
        // Включаем иконку горения над противником
        if (enemy.IconEffectsController)
            enemy.IconEffectsController.SetActiveIconBurning(true);
        
        // Включаем визульный эффект горения
        if(enemy.BurningEffectController)
            enemy.BurningEffectController.enabled = true;
    }

    public override void Tick()
    {
        enemy.TakeDamage(Damage.ActualDamage, Damage.DamageType, Damage.IsArmorIgnore);
    }

    public override void Disable()
    {
        // Выключаем иконку горения над противником
        if (enemy.IconEffectsController)
            enemy.IconEffectsController.SetActiveIconBurning(false);

        // Выключаем визульный эффект горения
        if (enemy.BurningEffectController)
            enemy.BurningEffectController.enabled = false;
    }
}
