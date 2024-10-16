using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

/// <summary>
/// 事件数据定义
/// </summary>
public struct EventData
{
    public string id;
    public string key;
    public int index;
    public Delegate action;
}

/// <summary>
/// 事件管理
/// </summary>
public class EventMgr
{
    /// <summary>
    /// 节点导航字典，存储该节点的所有事件
    /// </summary>
    private static Dictionary<string, HashSet<(string key, int index)>> _nodeNav = new();

    /// <summary>
    /// 事件字典
    /// </summary>
    private static Dictionary<string, Dictionary<int, EventData>> _events = new();

    /// <summary>
    /// 事件索引
    /// </summary>
    private static int _eventIndex = 0;
    
    /// <summary>
    /// 添加无参事件监听
    /// </summary>
    /// <param name="id">节点id</param>
    /// <param name="key">事件名</param>
    /// <param name="evt">事件函数</param>
    /// <returns></returns>
    public static EventData AddEvent(string key, Action evt, string id = "defaultNodeId")
    {
        int index = _eventIndex++;

        if (!_events.ContainsKey(key))
        {
            _events.Add(key, new Dictionary<int, EventData>());
        }

        EventData eventData = new EventData()
        {
            id = id,
            index = index,
            key = key,
            action = evt
        };

        _events[key][index] = eventData;

        if (!_nodeNav.ContainsKey(id))
        {
            _nodeNav.Add(id, new HashSet<(string e, int index)>());
        }

        _nodeNav[id].Add((key, index));

        return eventData;
    }

    /// <summary>
    /// 添加有参事件监听
    /// </summary>
    /// <param name="id">节点id</param>
    /// <param name="key">事件名</param>
    /// <param name="evt">事件函数</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static EventData AddEvent<T>(string key, Action<T> evt, string id = "defaultNodeId")
    {
        int index = _eventIndex++;

        if (!_events.ContainsKey(key))
        {
            _events.Add(key, new Dictionary<int, EventData>());
        }

        EventData eventData = new EventData()
        {
            id = id,
            index = index,
            key = key,
            action = evt
        };

        _events[key][index] = eventData;

        if (!_nodeNav.ContainsKey(id))
        {
            _nodeNav.Add(id, new HashSet<(string e, int index)>());
        }

        _nodeNav[id].Add((key, index));

        return eventData;
    }

    /// <summary>
    /// 发送无参事件
    /// </summary>
    /// <param name="key">事件名</param>
    public static void SendEvent(string key)
    {
        _events.TryGetValue(key, out var arr);

        if (arr != null)
        {
            foreach (var action in arr)
            {
                if (action.Value.action is Action valueAction)
                {
                    valueAction.Invoke();
                }
                else
                {
#if DEBUG
                    Debug.LogError("事件为有参函数");
#endif
                }
            }
        }
        else
        {
#if DEBUG
            Debug.LogWarning("事件" + key + "未注册");
#endif
        }
    }

    /// <summary>
    /// 发送有参事件
    /// </summary>
    /// <param name="key">事件名</param>
    /// <param name="data">参数</param>
    /// <typeparam name="T"></typeparam>
    public static void SendEvent<T>(string key, T data)
    {
        _events.TryGetValue(key, out var arr);

        if (arr != null)
        {
            foreach (var action in arr)
            {
                if (action.Value.action is Action<T> valueAction)
                {
                    valueAction.Invoke(data);
                }
                else
                {
#if DEBUG
                    Debug.LogError("事件为无参函数或参数错误");
#endif
                }
            }
        }
        else
        {
#if DEBUG
            Debug.LogWarning("事件" + key + "未注册");
#endif
        }
    }

    /// <summary>
    /// 移除事件
    /// </summary>
    /// <param name="data"></param>
    public static void RemoveEvent(EventData data)
    {
        _nodeNav.TryGetValue(data.id, out var datas);
        datas?.Remove((data.key, data.index));

        _events.TryGetValue(data.key, out var arr);
        arr?.Remove(data.index);
    }

    /// <summary>
    /// 移除节点上的所有事件
    /// </summary>
    /// <param name="id"></param>
    public static void RemoveAllEvents(string id)
    {
        _nodeNav.TryGetValue(id, out var data);

        if (data != null)
        {
            foreach (var res in data)
            {
                _events.TryGetValue(res.key, out var arr);

                if (arr != null)
                {
                    arr.Remove(res.index);
                }
            }

            _nodeNav.Remove(id);
        }
    }
    
}
