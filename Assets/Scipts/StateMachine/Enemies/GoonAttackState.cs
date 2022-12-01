using UnityEngine;

public class GoonAttackState : EnemyState
{
    private int[] _hashAnimAttackTriggers;

    private int _curAnimAttackTrigger;

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
        _hashAnimAttackTriggers = new int[2] { HashAnimStringEnemy.IsAttack_1, HashAnimStringEnemy.IsAttack_2 };
    }

    public override void Enter()
    {
        // �������� �������
        _timerRotateToPlayer = 0;

        // �������� transform ������ ��� ������������� ��� � ����������
        transformPlayer = transformPlayer ? transformPlayer : GetTransformPlayer();

        // �������� ��������� �����
        _curAnimAttackTrigger = _hashAnimAttackTriggers[Random.Range(0, _hashAnimAttackTriggers.Length)];

        // �������� ��������
        enemy.Animator.SetTrigger(_curAnimAttackTrigger);

        // ��������� ������� ���������. ������������ �������� � ��������
        enemy.IsBlockChangeState = true;
    }

    public override void Update()
    {
        _timerRotateToPlayer += Time.deltaTime;
        if (_timerRotateToPlayer < 1f)
        {
            LookAtTarget();
        }

        if (!enemy.IsBlockChangeState)
            enemy.SetState<ChasingState>();

    }

    public override void Exit()
    {

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
