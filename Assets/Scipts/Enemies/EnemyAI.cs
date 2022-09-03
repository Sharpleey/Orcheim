using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _targetEnemy;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_targetEnemy != null)
            _navMeshAgent.SetDestination(_targetEnemy.position);
    }

    public void SetEnabledNavMeshAgent(bool enabled)
    {
        _navMeshAgent.enabled = enabled;
    }
}
