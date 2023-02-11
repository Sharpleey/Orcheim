using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ���������� �������� �� ����� ����������� ���� ����������� ����� ��� �����������
/// </summary>
public class PopupDamageController : MonoBehaviour
{
    #region Serialize fields

    /// <summary>
    /// ���� ��� ������� ������� � �������, ������� � ����� ������� � ���������� ������
    /// </summary>
    [SerializeField] private GameObject _prefabDamageText;

    [SerializeField] private GameObject _prefabCriticalDamageText;

    /// <summary>
    /// �������� �������� ������/������� ������ �����
    /// </summary>
    [SerializeField] [Range (0.1f, 10f)] private float _rateShowingPopupDamageText = 2.5f;
    [SerializeField][Range(0.1f, 10f)] private float _rateHidePopupDamageText = 2.5f;
    [SerializeField] [Range (0.0f, 10f)] private float _durationShowPopupDamageText = 1f;

    #endregion Serialize fields

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

    #region Public methods

    /// <summary>
    /// ���������� ����������� ���������� ���� ��� �����������. ������� ������ � ������� � ������ ���������
    /// </summary>
    /// <param name="damage">�������� �����</param>
    /// <param name="isCriticalHit">����������� ��������� ��� ���</param>
    /// <param name="typeDamage">��� ����������� ����� (���������� ��� ����������� ����� ������ �����)</param>
    public void ShowPopupDamage(float damage, bool isCriticalHit, DamageType typeDamage)
    {
        GameObject popupDamageText;

        if (isCriticalHit)
            popupDamageText = Instantiate(_prefabCriticalDamageText);
        else
            popupDamageText = Instantiate(_prefabDamageText);

        popupDamageText.transform.SetParent(gameObject.transform, false);

        PopupDamage popupDamage = popupDamageText.GetComponent<PopupDamage>();
        if (popupDamage != null)
            popupDamage.ShowPopupDamageText(Mathf.Round(damage), TYPE_DAMAGE_COLOR[typeDamage], _rateShowingPopupDamageText, _rateHidePopupDamageText, _durationShowPopupDamageText);
    }

    #endregion Public methods
}
