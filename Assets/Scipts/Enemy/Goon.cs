using System.Collections;
using UnityEngine;

public class Goon : Enemy
{
    [Header("Spell Parameters")]
    [SerializeField, Min(5)] private float _cooldownWarcry = 20f;
    [SerializeField, Min(2)] private float _radiusWarcry = 8f;

    public bool IsWarcryInCooldown { get; private set; }

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

        _states[typeof(ChasingState)] = new GoonChasingState(this);
        _states[typeof(GoonAttackState)] = new GoonAttackState(this);
        _states[typeof(InspirationState)] = new InspirationState(this);
    }

    /// <summary>
    /// Метод для смены состояния из EventAnimation
    /// </summary>
    private void SetChasingState()
    {
        SetState<ChasingState>();
    }

    private IEnumerator ResetCooldown()
    {
        IsWarcryInCooldown = true;

        yield return new WaitForSeconds(_cooldownWarcry);

        IsWarcryInCooldown = false;
    }

    private void CastWarcry()
    {
        if (IsWarcryInCooldown)
            return;

        Debug.Log("Использование способности Warcry");

        StartCoroutine(ResetCooldown());

        // Ищем союзных существ в радиусе
        // Вешаем на них положительный эффект повышащий броню
    }
}
