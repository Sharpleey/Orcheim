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
        _states[typeof(WarriorIdleAttackState)] = new WarriorIdleAttackState(this);
        _states[typeof(WarriorAttackState)] = new WarriorAttackState(this);
    }

    /// <summary>
    /// Метод для смены состояния при срабатывании события по окончанию анимации атаки.
    /// </summary>
    private void SetIdleAttackState()
    {
        SetState<WarriorIdleAttackState>();
    }
}