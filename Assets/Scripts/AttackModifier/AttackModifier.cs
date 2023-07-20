
public abstract class AttackModifier : IEntity
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

    public AttackModifier(bool isActive = true)
    {
        IsActive = isActive;
    }
}
