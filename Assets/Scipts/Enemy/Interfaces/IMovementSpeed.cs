
public interface IMovementSpeed
{
    /// <summary>
    /// ��������� ��������
    /// </summary>
    public float DefaultSpeed { get; }

    /// <summary>
    /// ������������ �������� ��������
    /// </summary>
    public float MaxSpeed { get; }

    /// <summary>
    /// ���������� �������� ��������
    /// </summary>
    public float ActualSpeed { get; set; }
}
