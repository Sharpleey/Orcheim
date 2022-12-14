
/// <summary>
/// ����� ������������ �����, ����������� ��������� ������������ � �������� ����� �����
/// </summary>
public class SlowAttack : ProcableAttackModifaer
{
    public override string Name => "����������� �����";
    public override string Description => "����� ����������� �� ���� ������ Slowdown";
    public override int Proc�hance { get; set; } = 10;

    /// <summary>
    /// ������, ������� �������� �� ����������
    /// </summary>
    public Slowdown Effect { get; private set; }

    public SlowAttack()
    {
        Effect = new Slowdown();
    }
}
