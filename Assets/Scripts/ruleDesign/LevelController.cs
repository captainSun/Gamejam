using UnityEngine;

/// <summary>
/// 关卡控制
/// </summary>
public static class LevelController
{
    public static int curLevelIndex = 0; //当前的关卡Index
    public static int curScore = 0; //当前游戏得分
    public static bool inControl = false; //是否在关卡检测中
    
    public static float maxAngleDifference = 45f; // 人和狗之间的最大角度差
    public static float fallDuration = 2f; // 僵持时间，超过这个时间就会摔倒
    public static float maxRopeLength = 2f; // 绳子最大长度
    public static float runFallDuration = 4f; // 人跑动超过这个时间会摔倒
    public static float dogRunFallDuration = 2f; // 狗跑动超过这个时间会摔倒

    
    public static void InitLevel(int level)
    {
        curLevelIndex = level;
        
    }
    
    public static void StartLevel()
    {
        inControl = true;
        
    }
    
    public static void PauseLevel()
    {
        inControl = false;
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
        
        
    }

}
