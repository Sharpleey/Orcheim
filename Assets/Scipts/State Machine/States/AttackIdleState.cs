using UnityEngine;

/// <summary>
/// ����� ��������� ����� ���������. � ���� ��������� ��������� �������� �������� ������ ����� ������ � ��������� ����� 
/// </summary>
public class AttackIdleState : State
{
    /// <summary>
    /// ��������� ����� ���������� � ������
    /// </summary>
    private float _attackDistance = 2.5f;
   
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
    /// ��������� �� ���������� �� ������
    /// </summary>
    private float _distanceFromEnemyToPlayer;

    /// <summary>
    /// ��������� ������ ��� ������������ ���������
    /// </summary>
    private Transform _transformPlayer;

    private float _timerAttack;
    private float _timerUpdateDistance;

    public AttackIdleState(Enemy enemy) : base(enemy)
    {

    }
    public override void Enter()
    {
        base.Enter();

        // �������� �������
        _timerAttack = 0;
        _timerUpdateDistance = 0;

        // ������������� ������� �����, ������ �� ������� ���������
        _currentAttackFrequency = Random.Range(0.2f, 1.0f);

        // ������������� ��������� �����
        _attackDistance = enemy.NavMeshAgent.stoppingDistance + 0.1f;
        // �������� ��������
        enemy.Animator.SetBool(HashAnimation.IsIdleAttacking, true);
        // �������� transform ������ ��� ������������� ��� � ����������
        _transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Update()
    {
        base.Update();

        // ����� � ������������ ��������
        // -------------------------------------------------------------------------------
        _timerAttack += Time.deltaTime;
        if (_timerAttack > _currentAttackFrequency)
        {
            // �������� �������� �����, ��� ����� �������
            enemy.Animator.SetTrigger(HashAnimation.IsAttacking);

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
            _distanceFromEnemyToPlayer = Vector3.Distance(enemy.transform.position, _transformPlayer.position);
            if (_distanceFromEnemyToPlayer > _attackDistance && !enemy.IsBlockChangeState)
            {
                enemy.SetState<PursuitState>();
            }
            _timerUpdateDistance = 0;
        }
        // -------------------------------------------------------------------------------

        LookAtTarget();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.Animator.SetBool(HashAnimation.IsIdleAttacking, false);
    }

    /// <summary>
    /// ����� ������ ������������ � ������������ ��������� ����� � ������
    /// </summary>
    private void LookAtTarget()
    {
        Vector3 direction = -(enemy.transform.position - _transformPlayer.position);
        enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _rotationSpeedToTarget);
    }
}
