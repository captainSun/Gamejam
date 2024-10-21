using System;
using System.Diagnostics;


/// <summary>
/// 时间相关工具
/// </summary>
public static class TimeUtil
{
    private static Stopwatch s_stopwatch;

    /// 当前程序已运行时间（秒.毫秒）
    public static float timeSec;
    /// 当前程序已运行时间（毫秒）
    public static uint timeMsec;


    //初始化
    public static void Initialize()
    {
        s_stopwatch = new Stopwatch();
        s_stopwatch.Start();
    }



    /// <summary>
    /// Updates the time.
    /// </summary>
    public static void Update()
    {
        long value = s_stopwatch.ElapsedMilliseconds;
        timeSec = (float)value / 1000;
        timeMsec = Convert.ToUInt32(value);
    }


    /// <summary>
    /// 更新并返回当前程序已运行时间（秒.毫秒）
    /// </summary>
    /// <returns>The time sec.</returns>
    public static float GetTimeSec()
    {
        Update();
        return timeSec;
    }


    /// <summary>
    /// 更新并返回当前程序已运行时间（毫秒）
    /// </summary>
    /// <returns>The time msec.</returns>
    public static uint GetTimeMsec()
    {
        Update();
        return timeMsec;
    }

    public static string FloatForTime(float time)
    {
        //秒数取整
        int seconds = (int)time/1000;
        //一小时为3600秒 秒数对3600取整即为小时
        int hour = seconds / 3600;
        //一分钟为60秒 秒数对3600取余再对60取整即为分钟
        int minute = seconds % 3600 / 60;
        //对3600取余再对60取余即为秒数
        seconds = seconds % 3600 % 60;
        
        //打印00:00:00时间格式
        return string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, seconds);
    }

    //
}