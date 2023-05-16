using UnityEngine;

public class EnemyWaveSpawnZone : MonoBehaviour
{
    [SerializeField] private float _radiusZone = 4;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, _radiusZone);
        Gizmos.color = Color.white;
    }
}
