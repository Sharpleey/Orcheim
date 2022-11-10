using UnityEngine;
/// <summary>
/// ������� ����������� �����, �� �������� ����������� ��� ������ ���������
/// </summary>
public abstract class State
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
    protected State(Enemy enemy)
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

    protected float GetDistanceEnemyToPlayer()
    {
        return Vector3.Distance(enemy.transform.position, transformPlayer.position);
    }
}
