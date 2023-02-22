using UnityEngine;
/// <summary>
/// ������� ����������� �����, �� �������� ����������� ��� ������ ���������
/// </summary>
public abstract class EnemyState: IState
{
    /// <summary>
    /// ������ ������ �������� ������ ������ ���������� �� ����� ����������� � �������. ���������� ��� ������� ���� ��������������
    /// </summary>
    protected EnemyUnit enemyUnit;

    /// <summary>
    /// Transform ������ ��� ������������ ��� �������
    /// </summary>
    protected Transform transformPlayer;

    /// <summary>
    /// ��������� �� ����� �� ������
    /// </summary>
    protected float distanceEnemyToPlayer;

    /// <summary>
    /// ����� ����� � ����� ����� Enemy, ������� �� ����� ������
    /// </summary>
    private LayerMask collisionMask = 4096;

    /// <summary>
    /// ����������� ������ ���������, ��������� ��� ������������ ������ � ������� ���������� � ������ ���������
    /// </summary>
    /// <param name="enemyUnit">������ � ��������� ����������</param>
    protected EnemyState(EnemyUnit enemyUnit)
    {
        this.enemyUnit = enemyUnit;
    }
    /// <summary>
    /// ����� ���������� ��� ����� � ���������
    /// </summary>
    public virtual void Enter()
    {

    }

    /// <summary>
    /// ������ ����� ���������� ������� � ������ Update � ������ �������������� �� MonoBehaviour. ����� ������, �� ������� ������ �� ��������� � ������ Update 
    /// </summary>
    public virtual void Update()
    {

    }

    /// <summary>
    /// ����� ���������� ��� ������ �� ���������
    /// </summary>
    public virtual void Exit()
    {

    }

    /// <summary>
    /// ����� ���� ������ �� ���� �� �����, �������� ��� ��������� � ���������� ���
    /// </summary>
    /// <returns>Transfrom ������</returns>
    protected Transform GetTransformPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    /// <summary>
    /// ����� ���� ��������� ������ ���������� (EnemyUnit) � ���������� �� ���-��
    /// </summary>
    /// <returns>���-�� EnemyUnit � �������� �������</returns>
    protected int GetCountAlliesNearby(float searchRadius)
    {
        Vector3 center = enemyUnit.transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(center, searchRadius, collisionMask);

        return hitColliders.Length;
    }

    /// <summary>
    /// ����� ��� ��������� �������. ��� ����� ��������� �� �������������
    /// </summary>
    /// <param name="wave"></param>
    protected void SetChasingState(int wave)
    {
        enemyUnit.SetState<ChasingState>();
    }
}
