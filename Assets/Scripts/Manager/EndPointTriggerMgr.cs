using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointTriggerMgr : MonoBehaviour
{
    public CheckPointManager checkpointManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("People"))  // ֻ������
        {
            // Debug.Log("�����յ㣡");
            // checkpointManager.NextLevel();  // �л�����һ��
            EventMgr.SendEvent<GameObject>("EndPointTriggerEnter", gameObject);
        }
    }
}
