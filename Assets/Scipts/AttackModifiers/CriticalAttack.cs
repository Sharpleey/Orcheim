
/// <summary>
/// ����� ������������ �����, ����������� �������� ����������� ���� �����
/// </summary>
public class CriticalAttack : ProcableAttackModifier
{
    public override string Name => "����������� �����";
    public override string Description => $"����� c ������ {�hance.Actual}% ����� ������� {DamageMultiplier * 100}% ����";

    /// <summary>
    /// ��������� ����� ����������� �����
    /// </summary>
    public float DamageMultiplier { get; set; } = 1.5f;

    public CriticalAttack()
    {

    }
}
