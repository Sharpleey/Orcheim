
public abstract class AttackModifier : INaming
{
    /// <summary>
    /// �������� ������������ �����
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// �������� ������������ �����
    /// </summary>
    public abstract string Description { get; }

    /// <summary>
    /// ������� ����������� ��� ���
    /// </summary>
    public bool IsActive { get; set; }

    public AttackModifier(bool isActive = false)
    {
        IsActive = isActive;
    }
}
