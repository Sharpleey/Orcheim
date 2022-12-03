using UnityEngine;

/// <summary>
/// ����� ��������� ����� ���������. � ���� ��������� ��������� �������� �������� ������ ����� ������ � ��������� ����� 
/// </summary>
public class WarriorIdleAttackState : EnemyState
{  
    /// <summary>
    /// ������� �����, �.�. �������� ����� ������� � ��������
    /// </summary>
    private float _attackFrequency = 3.5f;
    
    /// <summary>
    /// �������� �������� ����� � ����
    /// </summary>
    private float _rotationSpeedToTarget = 2.5f;

    /// <summary>
    /// ������ �������� ����� �������
    /// </summary>
    private float _timerAttack;
    
    /// <summary>
    /// ������ ����� ������������ ��������
    /// </summary>
    private float _timerUpdateDistance;

    public WarriorIdleAttackState(Enemy enemy) : base(enemy)
    {

    }
    public override void Enter()
    {
        // �������� �������
        _timerAttack = 0;
        _timerUpdateDistance = 0.5f;

        // �������� transform ������ ��� ������������� ��� � ����������
        transformPlayer = transformPlayer ? transformPlayer : GetTransformPlayer();

        // ���������� ��������� �� ������
        distanceEnemyToPlayer = GetDistanceEnemyToPlayer();

        // �������� ��������
        enemy.Animator.SetBool(HashAnimStringEnemy.IsIdleAttack, true);
    }

    public override void Update()
    {
        // ����� � ������������ ��������
        // -------------------------------------------------------------------------------
        _timerAttack += Time.deltaTime;
        if (_timerAttack > _attackFrequency)
        {
            enemy.SetState<WarriorAttackState>();
        }
        // -------------------------------------------------------------------------------


        // ������ ������� ���������� ��������� �� ������ ����, � ��� � ��� �������
        // -------------------------------------------------------------------------------
        _timerUpdateDistance += Time.deltaTime;
        if (_timerUpdateDistance > 0.5f)
        {
            // ���������� ��������� �� ������
            distanceEnemyToPlayer = GetDistanceEnemyToPlayer();
            if (distanceEnemyToPlayer > enemy.AttackDistance)
            {
                enemy.SetState<ChasingState>();
            }

            _timerUpdateDistance = 0;
        }
        // -------------------------------------------------------------------------------

        LookAtTarget();
    }

    public override void Exit()
    {
        enemy.Animator.SetBool(HashAnimStringEnemy.IsIdleAttack, false);
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
