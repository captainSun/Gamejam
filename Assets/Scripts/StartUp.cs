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
        EventMgr.AddEvent("testEvent", OnTestEvent);
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(2);
        seq.AppendCallback(() =>
        {
            EventMgr.SendEvent("testEvent");
        });
    }

    void OnTestEvent()
    {
        print("================OnTestEvent");
    }
}
