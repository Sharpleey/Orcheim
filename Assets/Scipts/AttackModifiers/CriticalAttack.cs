
/// <summary>
/// ����� ������������ �����, ����������� �������� ����������� ���� �����
/// </summary>
public class CriticalAttack : ProcableAttackModifaer
{
    public override string Name => "����������� �����";
    public override string Description => "����� ����� ����������� ������� ����������� ����";
    public override int Proc�hance { get; set; } = 10;

    /// <summary>
    /// ��������� ����� ����������� �����
    /// </summary>
    public float DamageMultiplier { get; set; } = 1.5f;
}
