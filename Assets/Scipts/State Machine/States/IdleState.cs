using UnityEngine;

/// <summary>
///  ����� ��������� ����� ����������
/// </summary>
public class IdleState : State
{
    /// <summary>
    /// ���� ������ ����
    /// </summary>
    private float _viewAngleDetection = 120f;
    
    /// <summary>
    /// ��������� ������ �����
    /// </summary>
    private float _viewDetectionDistance = 14f;
    
    /// <summary>
    /// ������ �����������, ��� ������� � ����� ������ ���� ������� ������
    /// </summary>
    private float _absoluteDetectionDistance = 6f;
    
    /// <summary>
    /// ������ ����������
    /// </summary>
    private float _timerUpdate;

    public IdleState(Enemy enemy) : base(enemy)
    {

    }

    public override void Enter()
    {
        _timerUpdate = 0.5f;

        // �������� Transform ������ ��� ������������ ��� �������
        transformPlayer = transformPlayer ? transformPlayer : GetTransformPlayer();

        Messenger<int>.AddListener(WaveManager.Event.WAVE_IN_COMMING, SetChasingPlayerState);

        enemy.Animator.SetBool(HashAnimStringEnemy.IsIdle, true);
    }

    public override void Update()
    {
        _timerUpdate += Time.deltaTime;

        if (_timerUpdate > 0.5f)
        {
            // �� ������, ����� ����� ��� �� �����������
            if (!transformPlayer)
            {
                transformPlayer = GetTransformPlayer();
                return;
            }

            distanceEnemyToPlayer = GetDistanceEnemyToPlayer();
                
            // ������ ���������� �� ��������������, ���� (����� � ���� ���������� ��������� ���������) ��� (����� �������� �����)
            if (distanceEnemyToPlayer < _absoluteDetectionDistance || IsPlayerInSight())
            {
                enemy.SetState<ChasingPlayerState>();
            }

            // �������� ������
            _timerUpdate = 0;
        }
    }

    public override void Exit()
    {
        Messenger<int>.RemoveListener(WaveManager.Event.WAVE_IN_COMMING, SetChasingPlayerState);

        enemy?.Animator?.SetBool(HashAnimStringEnemy.IsIdle, false);
    }

    #region Private methods
    /// <summary>
    /// ����� ��������� ��������� �� ����� � ���� ������ �����
    /// </summary>
    /// <returns></returns>
    private bool IsPlayerInSight()
    {
        float realAngle = Vector3.Angle(enemy.transform.forward, transformPlayer.position - enemy.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(enemy.transform.position, transformPlayer.position - enemy.transform.position, out hit, _viewDetectionDistance))
        {
            if (realAngle < _viewAngleDetection / 2f && Vector3.Distance(enemy.transform.position, transformPlayer.position) <= _viewDetectionDistance && hit.transform == transformPlayer.transform)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// ����� ��������� ������� 
    /// </summary>
    /// <param name="wave"></param>
    private void SetChasingPlayerState(int wave)
    {
        enemy.SetState<ChasingPlayerState>();
    }
    #endregion Private methods
}
