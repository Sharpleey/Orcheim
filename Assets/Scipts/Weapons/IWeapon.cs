using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon: IItem
{
    float DamageSpread { get; }
}
