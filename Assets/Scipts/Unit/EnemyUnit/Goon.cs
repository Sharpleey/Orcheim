using System.Collections;
using UnityEngine;

public class Goon : EnemyUnit
{
    /// <summary>
    /// Способность данного юнита
    /// </summary>
    public Warcry Warcry { get; private set; }

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

    public override void InitAbilities()
    {
        base.InitAbilities();

        Warcry = new Warcry(this, timeCooldown: 20, radius: 8, isActive: true);

        Abilities.Add(typeof(Warcry), Warcry); // TODO
    }

    #region Methods Event Animation

    /// <summary>
    /// Метод для смены состояния из EventAnimation
    /// </summary>
    private void SetChasingState()
    {
        SetState<ChasingState>();
    }

    /// <summary>
    /// Вызывается из анимации
    /// </summary>
    private void ApplyWarcry() //TODO
    {
        if (Warcry.IsCooldown)
            return;

        Warcry.Apply();

        StartCoroutine(Warcry.Cooldown());
    }

    #endregion Methods Event Animation
}
