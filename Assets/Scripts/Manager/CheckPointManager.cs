using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public Transform[] startPoints;  // 每一关的起点
    public Transform[] endPoints;    // 每一关的终点
    //public Transform player;         // 玩家角色的 Transform
    //public Transform guideDog;         // 导盲犬角色的 Transform
    public Transform playerGroup;

    private int currentLevel = 0;    // 当前关卡索引

    private Rigidbody playerRb;
    private Rigidbody dogRb;
    void Start()
    {
        //playerRb = player.gameObject.GetComponent<Rigidbody>();
        //dogRb = guideDog.gameObject.GetComponent<Rigidbody>();
        this.ResetToStart();  // 初始化时重置到第1关的起点
    }

    // 重置玩家位置到当前关卡的起点
    public void ResetToStart()
    {
        //this.SetKinematic(true); //先暂时禁用物理效果，放置物理驱动的移动在位移时出现bug     
        this.ResetTrans();
        //this.SetKinematic(false);
    }

    public void SetKinematic(bool _is)
    {
        playerRb.isKinematic = _is;
        dogRb.isKinematic = _is;
    }

    public void ResetTrans()
    {
        print(startPoints[currentLevel].position.y);
        playerGroup.transform.position = startPoints[currentLevel].position;
        //playerGroup.transform.rotation = startPoints[currentLevel].rotation;
    }

    // 切换到下一关卡
    public void NextLevel()
    {
        if (currentLevel < startPoints.Length - 1)
        {
            currentLevel++;
            print("过关啦" + currentLevel);
            ResetToStart();  // 重置到新关卡的起点
        }
        else
        {
            Debug.Log("游戏结束！");
        }
    }
}
