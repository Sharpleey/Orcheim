using UnityEngine;

[CreateAssetMenu(menuName = "UnitConfig/PlayerUnitConfig", fileName = "PlayerUnitConfig", order = 0)]
public class PlayerUnitConfig : UnitConfig
{
    [Header("��������� ���-�� ������")]
    [Tooltip("������")]
    [SerializeField, Min(0)] private int _gold = 0;

    /// <summary>
    /// ��������� ���-�� ������
    /// </summary>
    public int Gold => _gold;
}
