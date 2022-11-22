using UnityEngine;

public static class HashAnimStringEnemy
{
    #region Trigger
    public static int IsAttack_1 = Animator.StringToHash("isAttack_1");
    public static int IsAttack_2 = Animator.StringToHash("isAttack_2");
    public static int IsInspiration = Animator.StringToHash("isInspiration");
    #endregion

    #region Bool
    public static int IsIdleAttack = Animator.StringToHash("isIdleAttack");
    public static int IsIdle = Animator.StringToHash("isIdle");
    public static int IsMovement = Animator.StringToHash("isMovement");
    #endregion

    #region Float
    public static int Speed = Animator.StringToHash("Speed");
    #endregion

    #region Animation
    //public static int WarriorAttack = Animator.StringToHash("Base Layer.WarriorAttack_1");
    //public static int WarriorIdleAttack = Animator.StringToHash("Base Layer.WarriorIdleAttack");

    //public static int GoonAttack_1 = Animator.StringToHash("Base Layer.GoonAttack_1");
    //public static int GoonAttack_2 = Animator.StringToHash("Base Layer.GoonAttack_2");
    #endregion
}

public static class HashAnimStringWeapon
{
    #region Trigger
    public static int IsShoot = Animator.StringToHash("isShoot");
    #endregion

    #region Bool
    public static int IsIdle = Animator.StringToHash("isIdle");
    public static int IsAimingLoad = Animator.StringToHash("isAimingLoad");
    public static int IsRun = Animator.StringToHash("isRun");
    public static int IsSprint = Animator.StringToHash("isSprint");
    #endregion
}

