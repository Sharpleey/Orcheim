using System.Collections;
using System.Collections.Generic;

public interface IGameManager
{
    ManagerStatus Status {get; }
    void Startup();

    /// <summary>
    /// ������� ��� ��������� ��� ������������� ����������
    /// </summary>
    public class Event
    {

    }
}
