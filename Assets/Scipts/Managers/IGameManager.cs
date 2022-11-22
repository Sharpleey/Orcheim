using System.Collections;
using System.Collections.Generic;

public interface IGameManager
{
    ManagerStatus Status {get; }
    void Startup();

    /// <summary>
    /// События для менеджера или транслируемые менеджером
    /// </summary>
    public class Event
    {

    }
}
