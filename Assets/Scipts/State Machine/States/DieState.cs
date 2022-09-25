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
    /// ������ ����� ���������� ������� � ������ Update � ������ �������������� �� MonoBehaviour. ����� ������, �� ������� ������ �� ��������� � ������ Update 
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
    /// ������ ����� ���������� ������� � ������ FixedUpdate � ������ �������������� �� MonoBehaviour. ����� ������, �� ������� ������ �� ��������� � ������ FixedUpdate 
    /// </summary>
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    /// <summary>
    /// ����� ���������� ��� ������ �� ���������
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }

    /// <summary>
    /// ����� ���������� ������ �� ������ ���������� � ������� ��� �� ����� ����� ��������� ����� 
    /// </summary>
    /// <returns></returns>
    private void MakePhysicalWeapon()
    {
        _enemy.Weapon.transform.parent = null;

        Rigidbody rigidbodyWeapon = _enemy.Weapon.GetComponent<Rigidbody>();
        rigidbodyWeapon.isKinematic = false;

    }
}
