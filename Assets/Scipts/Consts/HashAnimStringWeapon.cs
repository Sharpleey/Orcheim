using UnityEngine;

/// <summary>
/// ����� � ������������� ����������� ��������� � bow
/// </summary>
public static class HashAnimStringWeapon
{
    #region Trigger
    /// <summary>
    /// Trigger ������� ��������
    /// </summary>
    public static int IsFastShot = Animator.StringToHash("IsFastShot");

    /// <summary>
    /// �rigger ���������� ��������
    /// </summary>
    public static int IsAimingShot = Animator.StringToHash("IsAimingShot");
    #endregion

    #region Float
    /// <summary>
    /// Float �������� ������
    /// </summary>
    public static int PlayerSpeed = Animator.StringToHash("PlayerSpeed");
    #endregion

    #region Bool
    /// <summary>
    /// Bool ��������� �������
    /// </summary>
    public static int IsSprint = Animator.StringToHash("IsSprint");

    /// <summary>
    /// Bool ��������� ���������� ��������
    /// </summary>
    public static int IsAimingLoad = Animator.StringToHash("IsAimingLoad");
    #endregion
}
