
public interface IUnitParameters
{
    /// <summary>
    /// �������� �����
    /// </summary>
    public Health Health { get; }

    /// <summary>
    /// ����� �����
    /// </summary>
    public Armor Armor { get; }

    /// <summary>
    /// ���� �����
    /// </summary>
    public Damage Damage { get; }

    /// <summary>
    /// �������� ������������ �����
    /// </summary>
    public MovementSpeed MovementSpeed { get; }

    /// <summary>
    /// �������� ����� �����
    /// </summary>
    public AttackSpeed AttackSpeed { get; }

    /// <summary>
    /// ����� �������������� ��������� �����, �������������� ���������� �� �������
    /// </summary>
    public void InitParameters();
}
