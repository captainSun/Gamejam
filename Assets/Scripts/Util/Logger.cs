using System;
using UnityEngine;

/// <summary>
/// 日志记录
/// </summary>
public static class Logger
{
    public static void Initialize()
    {
        Application.logMessageReceived += UnityLogCallback;
    }


    /// <summary>
    /// Unity 日志回调
    /// </summary>
    /// <param name="condition">Condition.</param>
    /// <param name="stackTrace">Stack trace.</param>
    /// <param name="type">Type.</param>
    private static void UnityLogCallback(string condition, string stackTrace, LogType type)
    {
        switch (type)
        {
            case LogType.Exception:
            case LogType.Assert:
                string logType = type.ToString();
                stackTrace = string.Format("\nstack traceback:\n\t{0}", stackTrace.TrimEnd().Replace("\n", "\n\t"));
                LogData.Append(logType, condition, stackTrace);
                break;

            case LogType.Warning:
                // 将 DoTween 回调中的报错从 Warning 转为 Error
                int idx = condition.IndexOf("An error inside a tween callback");
                if (idx != -1)
                {
                    condition = condition.Substring(condition.IndexOf('►', idx) + 1);
                    int idx1 = condition.IndexOf('\n');
                    int idx2 = condition.IndexOf("\n\n", idx1);
                    stackTrace = condition.Substring(idx1, idx2 - idx1);
                    condition = condition.Substring(0, idx1);
                    condition = "[tween callback]" + condition;
                    LogError(condition, stackTrace);
                }
                break;
        }
    }
    

    /// <summary>
    /// 添加一条普通日志
    /// </summary>
    /// <param name="msg">Message.</param>
    /// <param name="type">Type.</param>
    /// <param name="stackTrace">Stack trace.</param>
    public static void Log(string msg, string type = LogData.TYPE_LOG, string stackTrace = null)
    {
        LogData data = LogData.Append(type.Trim(), msg, stackTrace);
        Debug.Log(data);
    }


    /// <summary>
    /// 添加一条警告日志
    /// </summary>
    /// <param name="msg">Message.</param>
    /// <param name="stackTrace">Stack trace.</param>
    public static void LogWarning(string msg, string stackTrace = null)
    {
        LogData data = LogData.Append(LogData.TYPE_WARNING, msg, stackTrace);
        Debug.LogWarning(data);
    }


    /// <summary>
    /// 添加一条错误日志
    /// </summary>
    /// <param name="msg">Message.</param>
    /// <param name="stackTrace">Stack trace.</param>
    public static void LogError(string msg, string stackTrace = null)
    {
        LogData data = LogData.Append(LogData.TYPE_ERROR, msg, stackTrace);
        Debug.LogError(data);
    }


    /// <summary>
    /// 添加一条网络日志
    /// </summary>
    /// <param name="msg">Message.</param>
    /// <param name="type">Type.</param>
    /// <param name="info">Info.</param>
    public static void LogNet(string msg, string type, string info)
    {
        LogData.Append(type, msg, "\ninfo: " + info);
    }



    /// <summary>
    /// [C#] 添加一条异常日志
    /// </summary>
    /// <param name="exception">Exception.</param>
    public static void LogException(Exception exception)
    {
        Debug.LogException(exception);
    }
    
    public static void LogException(string message)
    {
        Debug.LogException(new Exception(message));
    }





    //
}

