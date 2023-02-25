
/// <summary>
///  ����� �������, ������� ����� ��������� ��������� ������ ��� ���������� �������������, ������������
/// </summary>
public class AwardParameterUpgrade : Award
{
    public Upgratable UpgratableParameter { get; private set; }

    public AwardParameterUpgrade(AwardType awardType, string name, Upgratable parameter) : base(awardType, name)
    {
        UpgratableParameter = parameter;
    }
}
