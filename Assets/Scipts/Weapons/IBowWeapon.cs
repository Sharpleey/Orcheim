using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBowWeapon : IWeapon
{
    float TimeReloadShot { get; }
    void Shot();
}
