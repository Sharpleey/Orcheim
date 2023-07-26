using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// ����� ���������� �������� �� ����� ����������� ���� ����������� ����� ��� �����������
/// </summary>
public class PopupDamageController : MonoBehaviour
{
    #region Public fields

    /// <summary>
    /// ���� ������ ���� ����� ��� ������� �� ����� �����
    /// </summary>
    public Dictionary<DamageType, Color> TYPE_DAMAGE_COLOR = new Dictionary<DamageType, Color>
    {
        { DamageType.Physical, new Color(0.9f, 0.9f, 0.9f) },
        { DamageType.Fire, new Color(0.81f, 0.32f, 0.07f) },
        { DamageType.Bleeding, Color.red },
        { DamageType.Poison, Color.green }
    };

    #endregion Public fields

    #region Private fields
    private Pool<PopupDamage> _popupDamagePool;
    #endregion

    #region Mono
    [Inject]
    private void Construct(Pool<PopupDamage> pool)
    {
        _popupDamagePool = pool;
    }
    #endregion

    #region Public methods

    /// <summary>
    /// ���������� ����������� ���������� ���� ��� �����������. ������� ������ � ������� � ������ ���������
    /// </summary>
    /// <param name="damage">�������� �����</param>
    /// <param name="isCriticalHit">����������� ��������� ��� ���</param>
    /// <param name="typeDamage">��� ����������� ����� (���������� ��� ����������� ����� ������ �����)</param>
    public void ShowPopupDamage(float damage, bool isCriticalHit, DamageType typeDamage)
    {
        PopupDamage popupDamage = _popupDamagePool?.GetFreeElement();

        popupDamage?.transform.SetParent(transform, false);

        popupDamage?.SetDataAndStartAnimation(Mathf.Round(damage), isCriticalHit, TYPE_DAMAGE_COLOR[typeDamage]);
    }

    #endregion Public methods
}
