using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WanderAi : MonoBehaviour
{
    public float wanderRadius = 10f;  // 路人移动的半径范围
    public float waitTime = 0.2f;       // 每次到达目标后等待的时间
    private NavMeshAgent agent;       // 导航代理
    private Vector3 targetPosition;   // 目标位置

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetRandomDestination();  // 初始化时设置目标位置
    }

    void Update()
    {
        // 如果路人接近目标，则等待一段时间后设置新目标
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            StartCoroutine(WaitAndSetNewDestination());
        }
    }

    // 随机设置一个新目标位置
    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position; // 基于当前位置偏移

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
        {
            targetPosition = hit.position;
            agent.SetDestination(targetPosition);  // 设置为新的目标位置
        }
    }

    // 等待一段时间后设置新的目标
    IEnumerator WaitAndSetNewDestination()
    {
        yield return new WaitForSeconds(waitTime);
        SetRandomDestination();
    }
}
