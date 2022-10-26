using System.Collections;
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
    [SerializeField] private GameObject _prefabPopupDamageText;
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
    public Dictionary<TypeDamage, Color> TYPE_DAMAGE_COLOR = new Dictionary<TypeDamage, Color>
    {
        { TypeDamage.Physical, new Color(0.9f, 0.9f, 0.9f) },
        { TypeDamage.Fire, new Color(0.81f, 0.32f, 0.07f) },
        { TypeDamage.Bleeding, Color.red },
        { TypeDamage.Poison, Color.green }
    };
    #endregion Public fields

    #region Public methods
    /// <summary>
    /// ���������� ����������� ���������� ���� ��� �����������. ������� ������ � ������� � ������ ���������
    /// </summary>
    /// <param name="damage">�������� �����</param>
    /// <param name="typeDamage">��� ����������� ����� (���������� ��� ����������� ����� ������ �����)</param>
    public void ShowPopupDamage(int damage, TypeDamage typeDamage)
    {
        GameObject popupDamageText = Instantiate(_prefabPopupDamageText);
        popupDamageText.transform.SetParent(gameObject.transform, false);

        PopupDamage popupDamage = popupDamageText.GetComponent<PopupDamage>();
        if (popupDamage != null)
            popupDamage.ShowPopupDamageText(damage, TYPE_DAMAGE_COLOR[typeDamage], _rateShowingPopupDamageText, _rateHidePopupDamageText, _durationShowPopupDamageText);
    }
    #endregion Public methods
}
