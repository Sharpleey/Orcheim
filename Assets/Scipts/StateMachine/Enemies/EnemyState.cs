using UnityEngine;
/// <summary>
/// ������� ����������� �����, �� �������� ����������� ��� ������ ���������
/// </summary>
public abstract class EnemyState: IState
{
    /// <summary>
    /// ������ ������ �������� ������ ������ ���������� �� ����� ����������� � �������. ���������� ��� ������� ���� ��������������
    /// </summary>
    protected Enemy enemy;

    /// <summary>
    /// Transform ������ ��� ������������ ��� �������
    /// </summary>
    protected Transform transformPlayer;

    /// <summary>
    /// ��������� �� ����� �� ������
    /// </summary>
    protected float distanceEnemyToPlayer;

    /// <summary>
    /// ����������� ������ ���������, ��������� ��� ������������ ������ � ������� ���������� � ������ ���������
    /// </summary>
    /// <param name="enemy">������ � ��������� ����������</param>
    /// <param name="stateMachineEnemy">������ ��������� ����������</param>
    protected EnemyState(Enemy enemy)
    {
        this.enemy = enemy;
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
    /// ����� ��� ��������� �������. ��� ����� ��������� �� �������������
    /// </summary>
    /// <param name="wave"></param>
    protected void SetChasingState(int wave)
    {
        enemy.SetState<ChasingState>();
    }
}
