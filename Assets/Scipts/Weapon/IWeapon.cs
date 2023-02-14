using UnityEngine;

public interface IWeapon
{
    string Name { get; }

    Animator Animator { get; }
}
