
public interface IEnemyUnitParameters : IUnitParameters
{
    /// <summary>
    /// ��������� ����� ����� ����������
    /// </summary>
    public float AttackDistance { get; }

    /// <summary>
    /// ��������� �� ������� � ������
    /// </summary>
    public CostInGold CostInGold { get; }

    /// <summary>
    /// ��������� �� �������� � �����
    /// </summary>
    public CostInExp CostInExp { get; }
}
