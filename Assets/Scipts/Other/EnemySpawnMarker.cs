using UnityEngine;

public class EnemySpawnMarker : MonoBehaviour
{
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private StartStateType _startStateType;
    
    [Space(1)]
    [SerializeField] private float _radiusMarker = 1f;

    public EnemyType EnemyType => _enemyType;
    public StartStateType StartStateType => _startStateType;
    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, _radiusMarker);
        Gizmos.color = Color.white;
    }
}