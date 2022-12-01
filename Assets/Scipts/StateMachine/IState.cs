
public interface IState
{
    /// <summary>
    /// ����� ���������� ��� ����� � ���������
    /// </summary>
    public void Enter();


    /// <summary>
    /// ������ ����� ���������� ������� � ������ Update � ������ �������������� �� MonoBehaviour. ����� ������, �� ������� ������ �� ��������� � ������ Update 
    /// </summary>
    public void Update();


    /// <summary>
    /// ����� ���������� ��� ������ �� ���������
    /// </summary>
    public void Exit();

}
