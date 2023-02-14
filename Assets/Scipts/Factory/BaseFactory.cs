using UnityEngine;

/// <summary>
/// ������� ����������� ����� ������� ������
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseFactory<T> : MonoBehaviour where T : EnemyUnit
{
    [SerializeField] protected T _prefabUnit;

    public T GetNewInstance(Vector3 position)
    {
        var enemyUnit = Instantiate(_prefabUnit, position, Quaternion.identity);

        // ������ ��������� ����� �� �������������
        enemyUnit.DefaultState = StartStateType.Chasing;

        return enemyUnit;
    }
}
