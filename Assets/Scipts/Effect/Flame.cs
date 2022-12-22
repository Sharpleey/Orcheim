using UnityEngine;

public class Flame : Effect, IArmorUnitDecrease
{
    public override string Name => "�������";
    public override string Description => "������ ������� ��������� ���� ������...";

    public override EffectType EffectType => EffectType.Negative;
    public override float Duration { get; set; } = 3;
    public override float Frequency { get; set; } = 1f;

    public Damage Damage { get; private set; } = new Damage(10, 2, DamageType.Fire, false, "���� � ������� �� �������");

    public int ArmorDecrease { get; set; } = 1;

    public override void Enable()
    {
        // �������� ������ ������� ��� �����������
        if (enemy.IconEffectsController)
            enemy.IconEffectsController.SetActiveIconBurning(true);
        
        // �������� ��������� ������ �������
        if(enemy.BurningEffectController)
            enemy.BurningEffectController.enabled = true;
    }

    public override void Tick()
    {
        enemy.TakeDamage(Damage.ActualDamage, Damage.DamageType, Damage.IsArmorIgnore);
    }

    public override void Disable()
    {
        // ��������� ������ ������� ��� �����������
        if (enemy.IconEffectsController)
            enemy.IconEffectsController.SetActiveIconBurning(false);

        // ��������� ��������� ������ �������
        if (enemy.BurningEffectController)
            enemy.BurningEffectController.enabled = false;
    }
}
