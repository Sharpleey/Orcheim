
/// <summary>
/// ����� �������
/// </summary>
public interface IUnitLevel
{
    /// <summary>
    /// ������� �����
    /// </summary>
    public int Level { get; }

    /// <summary>
    /// �������� ������� ����� �� ������������ ���-�� ������� (�� ��������� �� 1)
    /// </summary>
    /// <param name="levelUp">�� ������� ������� ����� ���������</param>
    void LevelUp(int levelUp = 1);

    /// <summary>
    /// ������������� ������������ ������� �����
    /// </summary>
    /// <param name="newLevel">�������, ������� ����� ����������</param>
    void SetLevel(int newLevel);
}

