
public interface IEnemyUnitParameters : IUnitParameters
{
    /// <summary>
    /// ��������� ����� ����� ����������
    /// </summary>
    public float AttackDistance { get; }

    /// <summary>
    /// ��������� �� ������� � ������
    /// </summary>
    public Parameter CostInGold { get; }

    /// <summary>
    /// ��������� �� �������� � �����
    /// </summary>
    public Parameter CostInExp { get; }
}
