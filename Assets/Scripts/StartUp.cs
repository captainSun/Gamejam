using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 启动脚本
/// </summary>
public class StartUp : MonoBehaviour
{
    void Start()
    {
        TimeUtil.Initialize();
        GameMgr.Initialize();
        this.gameObject.AddComponent<Looper>();
    }
    
}
