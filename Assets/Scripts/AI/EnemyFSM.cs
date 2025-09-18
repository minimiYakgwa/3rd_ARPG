using System.Linq;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent navMeshAgent;
    private BehaviorGraphAgent BehaviorAgent;

    public void SetUp(Transform target, GameObject[] wayPoints)
    {
        this.target = target;

        navMeshAgent = GetComponent<NavMeshAgent>();
        BehaviorAgent = GetComponent<BehaviorGraphAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        BehaviorAgent.SetVariableValue("PatrolPoints", wayPoints.ToList());
    }
}
