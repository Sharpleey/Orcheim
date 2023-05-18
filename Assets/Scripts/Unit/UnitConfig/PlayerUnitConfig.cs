using UnityEngine;

[CreateAssetMenu(menuName = "UnitConfig/PlayerUnitConfig", fileName = "PlayerUnitConfig", order = 0)]
public class PlayerUnitConfig : UnitConfig
{
    [Header("Начальнео кол-во золота")]
    [Tooltip("Золото")]
    [SerializeField, Min(0)] private int _gold = 0;

    /// <summary>
    /// Начальнео кол-во золота
    /// </summary>
    public int Gold => _gold;
}
