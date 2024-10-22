using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WanderAi : MonoBehaviour
{
    public float wanderRadius = 10f;  // ·���ƶ��İ뾶��Χ
    public float waitTime = 0.2f;       // ÿ�ε���Ŀ���ȴ���ʱ��
    private NavMeshAgent agent;       // ��������
    private Vector3 targetPosition;   // Ŀ��λ��

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetRandomDestination();  // ��ʼ��ʱ����Ŀ��λ��
    }

    void Update()
    {
        // ���·�˽ӽ�Ŀ�꣬��ȴ�һ��ʱ���������Ŀ��
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            StartCoroutine(WaitAndSetNewDestination());
        }
    }

    // �������һ����Ŀ��λ��
    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position; // ���ڵ�ǰλ��ƫ��

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
        {
            targetPosition = hit.position;
            agent.SetDestination(targetPosition);  // ����Ϊ�µ�Ŀ��λ��
        }
    }

    // �ȴ�һ��ʱ��������µ�Ŀ��
    IEnumerator WaitAndSetNewDestination()
    {
        yield return new WaitForSeconds(waitTime);
        SetRandomDestination();
    }
}
