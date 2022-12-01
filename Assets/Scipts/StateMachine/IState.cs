
public interface IState
{
    /// <summary>
    /// Метод вызываемый при входе в состояние
    /// </summary>
    public void Enter();


    /// <summary>
    /// Данный метод необходимо вызвать в методе Update в классе наслдеованного от MonoBehaviour. Пищем логику, ту которую хотели бы выполнить в методе Update 
    /// </summary>
    public void Update();


    /// <summary>
    /// Метод вызываемый при выходе из состояния
    /// </summary>
    public void Exit();

}
