
public interface IAttackSpeed
{
    /// <summary>
    /// ��������� �������� �����
    /// </summary>
    public int DefaultAttackSpeed { get; }

    /// <summary>
    /// ������������ �������� �������� �����
    /// </summary>
    public int MaxAttackSpeed { get; }

    /// <summary>
    /// ���������� �������� �������� �����
    /// </summary>
    public int ActualAttackSpeed { get; set; }
}
