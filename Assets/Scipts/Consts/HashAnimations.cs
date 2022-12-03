using UnityEngine;

public static class HashAnimStringEnemy
{
    public static int IsAttack = Animator.StringToHash("IsAttack");
    public static int IsInspiration = Animator.StringToHash("isInspiration");


    public static int IsIdle = Animator.StringToHash("IsIdle");
    public static int IsMovement = Animator.StringToHash("IsMovement");
    public static int IsIdleAttack = Animator.StringToHash("IsIdleAttack");

    public static int IdleVariant = Animator.StringToHash("IdleVariant");
    public static int AttackVariant = Animator.StringToHash("AttackVariant");

    public static int Speed = Animator.StringToHash("Speed");

    #region Trigger
    //public static int IsAttack_1 = Animator.StringToHash("isAttack_1");
    //public static int IsAttack_2 = Animator.StringToHash("isAttack_2");
    #endregion

    #region Bool
    //public static int IsIdleAttack = Animator.StringToHash("isIdleAttack");
    //public static int IsIdle = Animator.StringToHash("isIdle");
    //public static int IsMovement = Animator.StringToHash("isMovement");
    #endregion

    #region Float
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
    public static int IsFastShot = Animator.StringToHash("IsFastShot");
    public static int IsAimingShot = Animator.StringToHash("IsAimingShot");
    #endregion

    #region Float
    public static int PlayerSpeed = Animator.StringToHash("PlayerSpeed");
    #endregion

    #region Bool
    public static int IsSprint = Animator.StringToHash("IsSprint");
    public static int IsAimingLoad = Animator.StringToHash("isAimingLoad");
    #endregion
}

