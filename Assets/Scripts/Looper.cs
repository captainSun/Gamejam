using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主循环
/// </summary>
public class Looper : MonoBehaviour
{
    void Start()
    {
        
    }

   
    void Update()
    {
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPaused) return;
#endif

        TimeUtil.Update();
        Timer.Update();
    }
}
