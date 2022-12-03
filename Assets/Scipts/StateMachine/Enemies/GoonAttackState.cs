using UnityEngine;

public class GoonAttackState : EnemyState
{
    /// <summary>
    /// ���-�� ��������� ����
    /// </summary>
    private int _attackVariantCount = 2;

    /// <summary>
    /// �������� �������� ����� � ����
    /// </summary>
    private float _rotationSpeedToTarget = 2.5f;

    /// <summary>
    /// ������ ����� ������������ ��������
    /// </summary>
    private float _timerRotateToPlayer;


    public GoonAttackState(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        // �������� �������
        _timerRotateToPlayer = 0;

        // �������� transform ������ ��� ������������� ��� � ����������
        transformPlayer = transformPlayer ? transformPlayer : GetTransformPlayer();

        // �������� ��������
        enemy.Animator.SetInteger(HashAnimStringEnemy.AttackVariant, Random.Range(0, _attackVariantCount));
        enemy.Animator.SetTrigger(HashAnimStringEnemy.IsAttack);
    }

    public override void Update()
    {
        _timerRotateToPlayer += Time.deltaTime;
        if (_timerRotateToPlayer < 1f)
        {
            LookAtTarget();
        }
    }

    /// <summary>
    /// ����� ������ ������������ � ������������ ��������� ����� � ������
    /// </summary>
    private void LookAtTarget()
    {
        Vector3 direction = -(enemy.transform.position - transformPlayer.position);
        enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _rotationSpeedToTarget);
    }
}
