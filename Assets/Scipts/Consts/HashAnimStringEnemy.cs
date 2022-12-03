using UnityEngine;

/// <summary>
/// Класс с хешириванными параметрами аниматоров у enemy
/// </summary>
public static class HashAnimStringEnemy
{
    #region Trigger
    /// <summary>
    /// Trigger атаки
    /// </summary>
    public static int IsAttack = Animator.StringToHash("IsAttack");

    /// <summary>
    /// Trigger заклинания, способности
    /// </summary>
    public static int IsInspiration = Animator.StringToHash("IsInspiration");
    #endregion

    #region
    /// <summary>
    /// Bool состояния покоя
    /// </summary>
    public static int IsIdle = Animator.StringToHash("IsIdle");

    /// <summary>
    /// Bool состояния преследования, движения
    /// </summary>
    public static int IsMovement = Animator.StringToHash("IsMovement");

    /// <summary>
    /// Bool состояния покоя между атаками
    /// </summary>
    public static int IsIdleAttack = Animator.StringToHash("IsIdleAttack");
    #endregion

    #region Int
    /// <summary>
    /// Int вариант состояния покоя
    /// </summary>
    public static int IdleVariant = Animator.StringToHash("IdleVariant");

    /// <summary>
    /// Int вариант атаки
    /// </summary>
    public static int AttackVariant = Animator.StringToHash("AttackVariant");
    #endregion Int

    #region
    /// <summary>
    /// Float скорость передвижения персонажа противника
    /// </summary>
    public static int Speed = Animator.StringToHash("Speed");
    #endregion
}
