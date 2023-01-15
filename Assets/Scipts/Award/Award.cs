
/// <summary>
/// ����������� ����� �������
/// </summary>
public abstract class Award
{
    /// <summary>
    /// �������� ���� �������
    /// </summary>
    public string TypeName { get; protected set; }

    /// <summary>
    /// �������� ��������, �����������, ������� ��� �.�., �.�. �� ��� �� ��������
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// �������� �������, ���������
    /// </summary>
    public string Description { get; protected set; }
}
