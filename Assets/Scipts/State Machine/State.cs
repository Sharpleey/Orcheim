using UnityEngine;
/// <summary>
/// Базовый абстрактный класс, от которого наследуются все классы состояния
/// </summary>
public abstract class State
{
    /// <summary>
    /// Хранит ссылку основной объект класса противника со всеми параметрами и данными. Необходимо для разного рода взаимодействий
    /// </summary>
    protected Enemy enemy;

    /// <summary>
    /// Transform игрока для отслеживания его позиции
    /// </summary>
    protected Transform transformPlayer;

    /// <summary>
    /// Конструктор класса состояния, необходим для прокидывания связей с данными противника и машины состояний
    /// </summary>
    /// <param name="enemy">Данный и параметры противника</param>
    /// <param name="stateMachineEnemy">Машина состоянйи противника</param>
    protected State(Enemy enemy)
    {
        this.enemy = enemy;
        //_stateMachine = stateMachine;
    }
    /// <summary>
    /// Метод вызываемый при входе в состояние
    /// </summary>
    public virtual void Enter()
    {

    }

    /// <summary>
    /// Данный метод необходимо вызвать в методе Update в классе наслдеованного от MonoBehaviour. Пищем логику, ту которую хотели бы выполнить в методе Update 
    /// </summary>
    public virtual void Update()
    {

    }

    /// <summary>
    /// Метод вызываемый при выходе из состояния
    /// </summary>
    public virtual void Exit()
    {

    }
}
