using UnityEngine;

/// <summary>
/// ����� � ������������� ����������� ���������� � enemy
/// </summary>
public static class HashAnimStringEnemy
{
    #region Trigger
    /// <summary>
    /// Trigger �����
    /// </summary>
    public static int IsAttack = Animator.StringToHash("IsAttack");

    /// <summary>
    /// Trigger ����������, �����������
    /// </summary>
    public static int IsInspiration = Animator.StringToHash("IsInspiration");
    #endregion

    #region
    /// <summary>
    /// Bool ��������� �����
    /// </summary>
    public static int IsIdle = Animator.StringToHash("IsIdle");

    /// <summary>
    /// Bool ��������� �������������, ��������
    /// </summary>
    public static int IsMovement = Animator.StringToHash("IsMovement");

    /// <summary>
    /// Bool ��������� ����� ����� �������
    /// </summary>
    public static int IsIdleAttack = Animator.StringToHash("IsIdleAttack");
    #endregion

    #region Int
    /// <summary>
    /// Int ������� ��������� �����
    /// </summary>
    public static int IdleVariant = Animator.StringToHash("IdleVariant");

    /// <summary>
    /// Int ������� �����
    /// </summary>
    public static int AttackVariant = Animator.StringToHash("AttackVariant");
    #endregion Int

    #region
    /// <summary>
    /// Float �������� ������������ ��������� ����������
    /// </summary>
    public static int Speed = Animator.StringToHash("Speed");
    #endregion
}
