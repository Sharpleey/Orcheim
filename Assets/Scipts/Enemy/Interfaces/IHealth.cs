
public interface IHealth
{
    /// <summary>
    /// ��������� ��������
    /// </summary>
    public int DefaultHealth { get; }

    /// <summary>
    /// ������������ �������� ��������
    /// </summary>
    public int MaxHealth { get; }

    /// <summary>
    /// ���������� �������� ��������
    /// </summary>
    public int ActualHealth { get; set; }
}
