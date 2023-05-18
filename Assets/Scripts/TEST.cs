using UnityEngine;

/// <summary>
/// Тестовый класс
/// </summary>
public class TEST : MonoBehaviour
{
    [SerializeField] private LayerMask collisionMask = 4096;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            ExplosionDamage();
        }
    }

    void ExplosionDamage()
    {
        Vector3 center = transform.position;
        float radius = GetComponent<SphereCollider>().radius;

        Collider[] hitColliders = Physics.OverlapSphere(center, radius, collisionMask);
        foreach (var hitCollider in hitColliders)
        {
            EnemyUnit enemyUnit = hitCollider.GetComponent<EnemyUnit>();
            enemyUnit.SetState<ChasingState>();
        }
    }
}
