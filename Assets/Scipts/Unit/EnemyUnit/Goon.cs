using System.Collections;
using UnityEngine;

public class Goon : EnemyUnit
{
    [Header("Spell Parameters")]
    [SerializeField, Min(5)] private float _cooldownWarcry = 20f;
    [SerializeField, Min(2)] private float _radiusWarcry = 8f;

    public bool IsWarcryInCooldown { get; private set; }

    /// <summary>
    /// Метод инициализирует состояния
    /// </summary>
    public override void InitStates()
    {
        base.InitStates();

        States[typeof(ChasingState)] = new GoonChasingState(this);
        States[typeof(GoonAttackState)] = new GoonAttackState(this);
        States[typeof(InspirationState)] = new InspirationState(this);
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
