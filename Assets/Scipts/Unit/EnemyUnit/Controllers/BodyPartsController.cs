using UnityEngine;

/// <summary>
/// ����� ���������� �������� �� ���������� �������� ������ ���� ���������. ����, ��������, ������, ���� ��������� � ������ ����� ����.
/// </summary>
public class BodyPartsController : MonoBehaviour
{
    [Header("Ears Parametres")]
    [SerializeField] private GameObject _usedEars;
    [SerializeField] private GameObject[] _ears;

    [Header("Hair Parametres")]
    [SerializeField] private GameObject _usedHair;
    [SerializeField] private GameObject[] _hairs;

    [Header("Head Parametres")]
    [SerializeField] private GameObject _usedHead;
    [SerializeField] private GameObject[] _heads;

    [Header("Body Skin Parametres")]
    [SerializeField] private Material _usedBodySkin;
    [SerializeField] private Material[] _bodySkins;
    [SerializeField] private SkinnedMeshRenderer _orcBody;

    private void Awake() 
    {
        DisableParts(_ears);
        DisableParts(_hairs);
        DisableParts(_heads);

        if(!_usedEars)
        {
            SetRandomBodyParts(ref _usedEars, _ears);
        }
        if (!_usedHair)
        {
            SetRandomBodyParts(ref _usedHair, _hairs);
        }
        if (!_usedHead)
        {
            SetRandomBodyParts(ref _usedHead, _heads);
        }

        SetRandomBodyPartsMaterial();
    }
    /// <summary>
    /// ���������� ��������� ������ ����������� ����� ���� �� ������� ��������
    /// </summary>
    /// <param name="usedPart">������ �� ������������ ����� ����</param>
    /// <param name="parts">������ �������� ������ ����, �� �������� ���������� ���� ���������</param>
    private void SetRandomBodyParts(ref GameObject usedPart, GameObject[] parts)
    {
        int indexPart = Random.Range(0, parts.Length);
        usedPart = parts[indexPart];
        usedPart.SetActive(true);
    }
    /// <summary>
    /// ����� ������������� ��������� �������� ��� ������ ���� ��������� �� �������
    /// </summary>
    private void SetRandomBodyPartsMaterial()
    {
        int indexBodyMaterial = Random.Range(0, _bodySkins.Length);
        _usedBodySkin = _bodySkins[indexBodyMaterial];
        _usedEars.GetComponent<SkinnedMeshRenderer>().material = _usedBodySkin;
        _usedHead.GetComponent<SkinnedMeshRenderer>().material = _usedBodySkin;
        _orcBody.material = _usedBodySkin;
    }
    /// <summary>
    /// ������������ ��� ������������ ����� ����
    /// </summary>
    /// <param name="parts">������ ������ ����</param>
    private void DisableParts(GameObject[] parts)
    {
        foreach (GameObject part in parts)
        {
            part.SetActive(false);
        }
    }
}
