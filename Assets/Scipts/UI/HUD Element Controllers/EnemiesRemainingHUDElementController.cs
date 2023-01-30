using TMPro;
using UnityEngine;

/// <summary>
/// ������ HUD ��������: ������� ���������� ������
/// </summary>
public class EnemiesRemainingHUDElementController : MonoBehaviour
{
    [Header("����� ������ ��������")]
    [SerializeField] private TextMeshProUGUI _enemiesRemainingValueText;

    private void Awake()
    {
        SpawnEnemyEventManager.OnEnemiesRemaining.AddListener(SetValueEnemiesRemainingCounter); //TODO �������� �������
    }

    private void Start()
    {
        if (_enemiesRemainingValueText)
            _enemiesRemainingValueText.text = "";
    }

    /// <summary>
    /// ����� ������������ �������� ��� �������� ���������� ������, ������ ��� � ���� ������
    /// </summary>
    /// <param name="wave">����� �����</param>
    private void SetValueEnemiesRemainingCounter(int enemiesRemaining)
    {
        if (_enemiesRemainingValueText)
            _enemiesRemainingValueText.text = enemiesRemaining.ToString();
    }
}