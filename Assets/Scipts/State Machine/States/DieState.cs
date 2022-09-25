using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : State
{
    private float _timer = 0;
    public DieState(SwordsmanEnemy enemy) : base(enemy)
    {
    }
    public override void Enter()
    {
        base.Enter();

        if (_enemy.Weapon != null)
            MakePhysicalWeapon();

        if (_enemy.RagdollController != null)
            _enemy.RagdollController.MakePhysical();

        if (_enemy.Animator != null)
            _enemy.Animator.enabled = false;

        if (_enemy.BurningEffectController != null)
            _enemy.BurningEffectController.enabled = false;

        if (_enemy.HealthBarController != null)
            _enemy.HealthBarController.SetActiveHealthBar(false);

        if (_enemy.IconEffectsController != null)
            _enemy.IconEffectsController.DeactivateAllIcons();

        if (_enemy.NavMeshAgent != null)
            _enemy.NavMeshAgent.enabled = false;
    }

    /// <summary>
    /// Данный метод необходимо вызвать в методе Update в классе наслдеованного от MonoBehaviour. Пищем логику, ту которую хотели бы выполнить в методе Update 
    /// </summary>
    public override void Update()
    {
        base.Update();

        _timer += Time.deltaTime;
        if (_timer > 3 && _enemy.DieEffectController != null && _enemy.DieEffectController.enabled == false)
        {
           _enemy.DieEffectController.enabled = true;
        }

        if (_timer > 8)
        {
            _enemy.DestroyEnemyObjects();
        }
    }

    /// <summary>
    /// Данный метод необходимо вызвать в методе FixedUpdate в классе наслдеованного от MonoBehaviour. Пищем логику, ту которую хотели бы выполнить в методе FixedUpdate 
    /// </summary>
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    /// <summary>
    /// Метод вызываемый при выходе из состояния
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }

    /// <summary>
    /// Метод отвязывает оружие от модели противника и удаляет его со сцены через некоторое время 
    /// </summary>
    /// <returns></returns>
    private void MakePhysicalWeapon()
    {
        _enemy.Weapon.transform.parent = null;

        Rigidbody rigidbodyWeapon = _enemy.Weapon.GetComponent<Rigidbody>();
        rigidbodyWeapon.isKinematic = false;

    }
}
