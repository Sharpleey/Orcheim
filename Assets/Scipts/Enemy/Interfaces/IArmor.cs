
public interface IArmor
{
    /// <summary>
    /// ��������� �����
    /// </summary>
    public int DefaultArmor { get; }

    /// <summary>
    /// ������������ �������� �����
    /// </summary>
    public int MaxArmor { get; }

    /// <summary>
    /// ���������� �������� �����
    /// </summary>
    public int ActualArmor { get; set; }
}
