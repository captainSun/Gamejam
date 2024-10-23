using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 关卡控制
/// </summary>
public static class LevelController
{
    public static int curLevelIndex = 0; //当前的关卡Index
    public static int curScore = 0; //当前游戏扣分
    public static LevelData curLevelData;
    public static bool inControl = false; //是否在关卡检测中
    public static Timer timer; //关卡计时器
    public static float remainTime = 0; //剩余时间
    
    public static float maxAngleDifference = 135f; // 人和狗之间的最大角度差
    public static float maxRopeLength = 4f; // 绳子最大长度
    
    public static float fallDuration = 2f; // 僵持时间，超过这个时间就会摔倒
    public static float runFallDuration = 4f; // 人跑动超过这个时间会摔倒
    public static float dogRunFallDuration = 2f; // 狗跑动超过这个时间会摔倒

    public static Dictionary<int, LevelData> levelDataDic = new Dictionary<int, LevelData>(); //关卡配置

    public static List<GameObject> triggerList = new List<GameObject>();//关卡触发器
    
    public static void Initialize()
    {
        triggerList.Add(GameObject.Find("trans_2").gameObject);
        triggerList.Add(GameObject.Find("trans_3_bad").gameObject);
        triggerList.Add(GameObject.Find("trans_2").gameObject);
    }

    //开始关卡
    public static void StartLevel(int level)
    {
        inControl = true;
        curLevelIndex = level; 
        LevelData lvData;
        if (levelDataDic.TryGetValue(level, out lvData) == false)
        {
            lvData = ResourceMgr.LoadResAsset<LevelData>("LevelData_"+level, AssetsEnum.Scriptable);
            levelDataDic.Add(level, lvData);
        }
        
        if (lvData == null)
        {
            Logger.LogError("InitLevel 没有配置关卡数据 index:"+level);
            return;
        }
        curLevelData = lvData;

        ResetPos();
        GameMgr.mainMenu.StartLevel(lvData);
        GameMgr.SetMoveControl(true);
        
        if (timer != null)
        {
            timer.Stop();
            timer = null;
        }
        
        remainTime = lvData.timeLimit * 1000;
        timer = Timer.Create(5, (int)remainTime / 5, UpdateTime);
        EventMgr.AddEvent<GameObject>("EndPointTriggerEnter", OnEndPointTriggerEnter, "EndPoint");
    }

    //倒计时更新
    static void UpdateTime(Timer _timer)
    {
        remainTime = remainTime - timer.delay;
        if (remainTime > 0)
        {
            GameMgr.mainMenu.UpdateTime(remainTime);
        }
        else
        {
            timer = null;
            //倒计时结束
            StopLevel(false);
            GameMgr.mainMenu.ShowStopNotice(1);
        }
       
    }

    //播放摔倒重置关卡
    public static void FallResetLevel()
    {
        Logger.Log("StopLevel 播放摔倒重置关卡");
        StopControl();
        GameMgr.mainMenu.ShowStopNotice(3);
        GameMgr.PlayFall(() =>
        {
            ResetPos();
            GameMgr.SetMoveControl(true);
            inControl = true;
            timer = Timer.Create(5, (int)remainTime / 5, UpdateTime);
            GameMgr.mainMenu.ActiveInfo(true);
        });
    }

    //关卡结束
    public static void StopLevel(bool isWin)
    {
        StopControl();
        remainTime = 0;
        curScore = 0;
        ActiveTrigger(false);
        if (isWin)
        {
            Logger.Log("StopLevel 胜利");
            //胜利
            GameMgr.PlayWin(() =>
            {
               
            });
        }
        else
        {
            Logger.Log("StopLevel 失败");
            //失败
            GameMgr.PlayDefeat(() =>
            {
               
            });
        }
    }

    //重置位置
    static void ResetPos()
    {
        GameMgr.people.transform.position = curLevelData.peopleStartPos;
        GameMgr.dog.transform.position = curLevelData.dogStartPos;
        GameMgr.people.transform.localEulerAngles = curLevelData.peopleStartRot;
        GameMgr.dog.transform.localEulerAngles = curLevelData.dogStartRot;
    }

    //停止控制
    static void StopControl()
    {
        inControl = false;
        if (timer != null)
        {
            timer.Stop();
            timer = null;
        }
        GameMgr.SetMoveControl(false);
    }

    //开关场景里的触发器
    public static void ActiveTrigger(bool flag)
    {
        for (int i = 0; i < triggerList.Count; i++)
        {
            triggerList[i].SetActive(flag);
        }
    }

    //关卡触发监听
    public static void OnEndPointTriggerEnter(GameObject endPoint)
    {
        EventMgr.RemoveAllEvents("EndPoint");
        StopControl();
        endPoint.SetActive(false);
        GameMgr.mainMenu.ActiveInfo(false);
        if (endPoint.transform.parent.name == "trans_2")
        {
            Logger.Log("StopLevel 到达第一关");
            //到达第一关
            GameMgr.PlayBridge(() =>
            {
                StartLevel(2);
            });
        }
        else if(endPoint.transform.parent.name == "trans_3_bad") 
        {
            //到达坏结局
            StopLevel(false);
        }
        else if (endPoint.transform.parent.name == "trans_4")
        {
            //到达好结局
            StopLevel(true);
        }
    }

    public static void UpdateCheck()
    {
        if (inControl == false)
        {
            return;
        }

        //两者距离
        float distance = Vector3.Distance(GameMgr.people.transform.position, GameMgr.dog.transform.position);
        //角度差
        float angleDifference = Vector3.Angle(GameMgr.people.transform.forward, GameMgr.dog.transform.forward);
        // Logger.Log("两者距离==="+distance);
        // Logger.Log("角度差==="+angleDifference);
        if (distance > maxRopeLength || angleDifference > maxAngleDifference)
        {
            curScore += 1;
            EventMgr.SendEvent("UpdateScore");
            if (curScore >= curLevelData.hp)
            {
                Logger.Log("StopLevel 次数用尽");
                //次数用尽
                StopLevel(false);
                GameMgr.mainMenu.ShowStopNotice(2);
            }
            else
            {
                FallResetLevel();
            }
            
        }
        

    }

}
