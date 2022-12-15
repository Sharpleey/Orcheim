using UnityEngine;

public class Flame : Effect, IUnitDamage, IArmorUnitDecrease
{
    public override string Name => "Горение";
    public override string Description => "Эффект наносит персонажу урон каждые...";

    public override EffectType EffectType => EffectType.Negative;
    public override float Duration { get; set; } = 3;
    public override float Frequency { get; set; } = 1f;


    public int AvgDamage { get; set; } = 10;
    public int Damage
    {
        get
        {
            int range = (int)(AvgDamage * GeneralParameter.OFFSET_DAMAGE_HEALING);
            return Random.Range(AvgDamage - range, AvgDamage + range);
        }
    }
    public bool IsArmorIgnore { get; } = false;
    public DamageType TypeDamage { get; } = DamageType.Fire;
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
        enemy.TakeDamage(Damage, TypeDamage);
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
