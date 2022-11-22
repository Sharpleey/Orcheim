using UnityEngine;

/// <summary>
/// ����� ��������� ����� ���������. � ���� ��������� ��������� �������� �������� ������ ����� ������ � ��������� ����� 
/// </summary>
public class WarriorAttackState : State
{  
    /// <summary>
    /// ������� �����, �.�. �������� ����� ������� � ��������
    /// </summary>
    private float _attackFrequency = 3.5f;

    /// <summary>
    /// ������� �������� ����� ������
    /// </summary>
    private float _currentAttackFrequency = 3.5f;
    
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

    public WarriorAttackState(Enemy enemy) : base(enemy)
    {

    }
    public override void Enter()
    {
        // �������� �������
        _timerAttack = 0;
        _timerUpdateDistance = 0.5f;

        // ������������� ������� �����, ������ �� ������� ���������
        _currentAttackFrequency = Random.Range(0.2f, 1.0f);

        // �������� transform ������ ��� ������������� ��� � ����������
        transformPlayer = transformPlayer ? transformPlayer : GetTransformPlayer();

        // �������� ��������
        enemy.Animator.SetBool(HashAnimStringEnemy.IsIdleAttack, true);
    }

    public override void Update()
    {
        // ����� � ������������ ��������
        // -------------------------------------------------------------------------------
        _timerAttack += Time.deltaTime;
        if (_timerAttack > _currentAttackFrequency)
        {
            //������������� ����
            if (enemy.AudioController)
                enemy.AudioController.PlaySound(EnemySoundType.Atttack);

            // �������� �������� �����, ��� ����� �������
            enemy.Animator.SetTrigger(HashAnimStringEnemy.IsAttack_1);

            // ������������� ������� �����, ������ �� ������� ���������
            _currentAttackFrequency = Random.Range(_attackFrequency - 0.8f, _attackFrequency + 0.8f);

            _timerAttack = 0;
        }
        // -------------------------------------------------------------------------------


        // ������ ������� ���������� ��������� �� ������ ����, � ��� � ��� �������
        // -------------------------------------------------------------------------------
        _timerUpdateDistance += Time.deltaTime;
        if (_timerUpdateDistance > 0.5f)
        {
            // ���������� ��������� �� ������
            distanceEnemyToPlayer = GetDistanceEnemyToPlayer();
            if (distanceEnemyToPlayer > enemy.AttackDistance && !enemy.IsBlockChangeState)
            {
                enemy.SetState<ChasingPlayerState>();
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
