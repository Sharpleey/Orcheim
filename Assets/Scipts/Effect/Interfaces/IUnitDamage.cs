
public interface IUnitDamage : IDamage
{
    /// <summary>
    /// ��������� ���� ������� ����
    /// </summary>
    public int DefaultAvgDamage { get; }

    /// <summary>
    /// ������� ����
    /// </summary>
    int AvgDamage { get; set; }

    /// <summary>
    /// ���������� �������� �����
    /// </summary>
    int ActualDamage { get; }
}
