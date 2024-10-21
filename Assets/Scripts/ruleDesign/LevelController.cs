using System.Collections.Generic;
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
    
    public static float maxAngleDifference = 45f; // 人和狗之间的最大角度差
    public static float fallDuration = 2f; // 僵持时间，超过这个时间就会摔倒
    public static float maxRopeLength = 3f; // 绳子最大长度
    public static float runFallDuration = 4f; // 人跑动超过这个时间会摔倒
    public static float dogRunFallDuration = 2f; // 狗跑动超过这个时间会摔倒

    public static Dictionary<int, LevelData> levelDataDic = new Dictionary<int, LevelData>();
    
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
        GameMgr.people.transform.position = lvData.peopleStartPos;
        GameMgr.dog.transform.position = lvData.dogStartPos;
        GameMgr.mainMenu.StartLevel(lvData);
    }
    
    public static void ResetLevel()
    {
        inControl = false;
        
        GameMgr.PlayFail(() =>
        {
            GameMgr.people.transform.position = curLevelData.peopleStartPos;
            GameMgr.dog.transform.position = curLevelData.dogStartPos;
            inControl = true;
        });
    }

    public static void StopLevel()
    {
        inControl = false;
    }
    
    public static void UpdateCheck()
    {
        if (inControl == false)
        {
            return;
        }

        //两者距离
        float distance = Vector3.Distance(GameMgr.people.transform.position, GameMgr.dog.transform.position);
        if (distance > maxRopeLength)
        {
            curScore += 1;
            EventMgr.SendEvent("UpdateScore");
            if (curScore >= curLevelData.hp)
            {
                StopLevel();
            }
            else
            {
                ResetLevel();
            }
            
        }
        

    }

}
