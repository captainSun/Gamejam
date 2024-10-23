using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointTriggerMgr : MonoBehaviour
{
    public CheckPointManager checkpointManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("People"))  // 只检测玩家
        {
            // Debug.Log("到达终点！");
            // checkpointManager.NextLevel();  // 切换到下一关
            EventMgr.SendEvent<GameObject>("EndPointTriggerEnter", gameObject);
        }
    }
}
