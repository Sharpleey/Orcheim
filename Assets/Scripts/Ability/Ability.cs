
/// <summary>
/// ����������� ����� �����������
/// </summary>
public abstract class Ability
{
    /// <summary>
    /// ������� ����������� ��� ���
    /// </summary>
    public bool IsActive { get; protected set; }

    protected Unit unit;

    public Ability(Unit unit, bool isActive = false)
    {
        this.unit = unit;
        IsActive = isActive;
    }
}
