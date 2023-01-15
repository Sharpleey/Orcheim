
/// <summary>
/// ����� ������������ �����, ����������� ��������� ������������ � �������� ����� �����
/// </summary>
public class SlowAttack : ProcableAttackModifier
{
    public override string Name => "����������� �����";
    public override string Description => "����� ����������� �� ���� ������ Slowdown";

    /// <summary>
    /// ������, ������� �������� �� ����������
    /// </summary>
    public Slowdown Effect { get; private set; }

    public SlowAttack()
    {
        Effect = new Slowdown();
    }
}
