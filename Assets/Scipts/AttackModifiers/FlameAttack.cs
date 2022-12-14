
/// <summary>
/// ����� ������������ �����, ����������� ��������� ����
/// </summary>
public class FlameAttack : ProcableAttackModifaer
{
    public override string Name => "����������� �����";
    public override string Description => "����� ����������� �� ���� ������ Flame";
    public override int Proc�hance { get; set; } = 10;

    /// <summary>
    /// �����, ������� �������� �� �������
    /// </summary>
    public Flame Effect { get; private set; }

    public FlameAttack()
    {
        Effect = new Flame();
    }
}
