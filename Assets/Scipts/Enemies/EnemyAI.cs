using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _targetEnemy;

    private NavMeshAgent NavMeshAgent;
    private Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_targetEnemy != null)
            NavMeshAgent.SetDestination(_targetEnemy.position);
    }

    public void SetEnabledNavMeshAgent(bool enabled)
    {
        NavMeshAgent.enabled = enabled;
    }
}
