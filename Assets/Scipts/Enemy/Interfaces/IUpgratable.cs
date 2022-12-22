
public interface IUpgratable
{
    /// <summary>
    /// ������������ ������� ���������
    /// </summary>
    public int MaxLevel { get; }

    /// <summary>
    /// ������� ��������
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// �������� �������� �������������� ���������
    /// </summary>
    public float UpgradeValue { get; }

    /// <summary>
    /// �������� ���������
    /// </summary>
    public string DescriptionUpdate { get; }

    /// <summary>
    /// ��������� ��������, �������� ������� (�� ��������� �� 1)
    /// </summary>
    /// <param name="levelUp">�� ������� ������� ���������</param>
    public void Upgrade(int levelUp = 1);

    /// <summary>
    /// ���������� ������������ ������� ���������
    /// </summary>
    /// <param name="level"></param>
    public void SetLevel(int level);
}
