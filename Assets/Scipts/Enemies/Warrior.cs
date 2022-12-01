public class Warrior : Enemy
{
    private new void Start()
    {
        base.Start();

        // Инициализируем состояния
        InitStates();
        // Задаем состояние поумолчанию
        SetStateByDefault();
    }

    /// <summary>
    /// Метод инициализирует состояния
    /// </summary>
    private new void InitStates()
    {
        base.InitStates();

        _states[typeof(ChasingState)] = new WarriorChasingState(this);
        _states[typeof(WarriorAttackState)] = new WarriorAttackState(this);
    }
}