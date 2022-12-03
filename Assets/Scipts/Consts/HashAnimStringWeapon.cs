using UnityEngine;

/// <summary>
/// Класс с хешированными параметрами аниматора у bow
/// </summary>
public static class HashAnimStringWeapon
{
    #region Trigger
    /// <summary>
    /// Trigger быстрой стрельбы
    /// </summary>
    public static int IsFastShot = Animator.StringToHash("IsFastShot");

    /// <summary>
    /// Тrigger прицельной стрельбы
    /// </summary>
    public static int IsAimingShot = Animator.StringToHash("IsAimingShot");
    #endregion

    #region Float
    /// <summary>
    /// Float скорость игрока
    /// </summary>
    public static int PlayerSpeed = Animator.StringToHash("PlayerSpeed");
    #endregion

    #region Bool
    /// <summary>
    /// Bool состояние спринта
    /// </summary>
    public static int IsSprint = Animator.StringToHash("IsSprint");

    /// <summary>
    /// Bool состояние прицельной стрельбы
    /// </summary>
    public static int IsAimingLoad = Animator.StringToHash("IsAimingLoad");
    #endregion
}
