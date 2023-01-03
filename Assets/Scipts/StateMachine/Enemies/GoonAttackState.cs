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


    public GoonAttackState(EnemyUnit enemyUnit) : base(enemyUnit)
    {

    }

    public override void Enter()
    {
        // �������� �������
        _timerRotateToPlayer = 0;

        // �������� transform ������ ��� ������������� ��� � ����������
        transformPlayer = transformPlayer ? transformPlayer : GetTransformPlayer();

        // �������� ��������
        enemyUnit.Animator.SetInteger(HashAnimStringEnemy.AttackVariant, Random.Range(0, _attackVariantCount));
        enemyUnit.Animator.SetTrigger(HashAnimStringEnemy.IsAttack);
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
        Vector3 direction = -(enemyUnit.transform.position - transformPlayer.position);
        enemyUnit.transform.rotation = Quaternion.Lerp(enemyUnit.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _rotationSpeedToTarget);
    }
}
