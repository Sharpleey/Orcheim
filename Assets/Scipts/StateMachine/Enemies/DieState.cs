using UnityEngine;

/// <summary>
/// ����� ��������� ������ ����������
/// </summary>
public class DieState : EnemyState
{
    private float _timer = 0;
    public DieState(Enemy enemy) : base(enemy)
    {
    }
    public override void Enter()
    {
        base.Enter();

        GlobalGameEventManager.EnemyKilled();

        if (enemy.Weapon != null)
            MakePhysicalWeapon();

        if (enemy.RagdollController != null)
            enemy.RagdollController.MakePhysical();

        if (enemy.Animator != null)
            enemy.Animator.enabled = false;

        if (enemy.BurningEffectController != null)
            enemy.BurningEffectController.enabled = false;

        if (enemy.HealthBarController != null)
            enemy.HealthBarController.SetActiveHealthBar(false);

        if (enemy.IconEffectsController != null)
            enemy.IconEffectsController.DeactivateAllIcons();

        if (enemy.NavMeshAgent != null)
            enemy.NavMeshAgent.enabled = false;

        if (enemy.AudioController)
            enemy.AudioController.PlayRandomSoundWithProbability(EnemySoundType.Dead);
    }

    /// <summary>
    /// ������ ����� ���������� ������� � ������ Update � ������ �������������� �� MonoBehaviour. ����� ������, �� ������� ������ �� ��������� � ������ Update 
    /// </summary>
    public override void Update()
    {
        base.Update();

        _timer += Time.deltaTime;
        if (_timer > 3 && enemy.DieEffectController != null && enemy.DieEffectController.enabled == false)
        {
           enemy.DieEffectController.enabled = true;
        }

        if (_timer > 8)
        {
            enemy.DestroyEnemyObjects();
        }
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
        enemy.Weapon.transform.parent = null;

        Rigidbody rigidbodyWeapon = enemy.Weapon.GetComponent<Rigidbody>();
        rigidbodyWeapon.isKinematic = false;
    }
}
