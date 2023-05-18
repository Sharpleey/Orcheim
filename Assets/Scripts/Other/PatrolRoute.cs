using UnityEngine;

public class PatrolRoute : MonoBehaviour
{
    [Header("Объекты, точки по которым будет производится патрулирование (Расположить их очередно)")]
    [SerializeField] private Transform[] _patrolWayPoints;

    /// <summary>
    /// Точки маршрута, расположенный в порядке обхода их персонажем противника
    /// </summary>
    public Transform[] WayPoints => _patrolWayPoints;
}
