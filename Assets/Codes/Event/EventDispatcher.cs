using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 监听事件消息，函数委托必须添加BaseEvent参数
/// </summary>
public delegate void eventDelegate(BaseEvent evt);
public class EventDispatcher
{
    Dictionary<string, eventDelegate> eventDic;
    Dictionary<string, eventDelegate> eventOnceDic;

    public EventDispatcher()
    {
        eventDic = new Dictionary<string, eventDelegate>();
        eventOnceDic = new Dictionary<string, eventDelegate>();
    }


    /// <summary>
    /// 监听事件消息，函数委托必须添加BaseEvent参数
    /// </summary>
    public bool AddEventListener(string msg, eventDelegate func)
    {
        if (eventDic.ContainsKey(msg))
        {
            eventDic[msg] += func;
        }
        else
        {
            eventDic.Add(msg, func);
        }
        return true;
    }
    /// <summary>
    /// 监听事件消息，执行一次后销毁，函数委托必须添加BaseEvent参数
    /// </summary>
    public bool AddOnce(string msg, eventDelegate func)
    {
        if (eventOnceDic.ContainsKey(msg))
        {
            eventOnceDic[msg] += func;
        }
        else
        {
            eventOnceDic.Add(msg, func);
        }
        return true;
    }
    /// <summary>
    /// 广播事件消息
    /// </summary>
    public bool Dispatch(string msg, params object[] args)
    {
        BaseEvent evt = new BaseEvent(this,args);
        bool result = false;
        if (eventDic.ContainsKey(msg))
        {
            eventDic[msg](evt);
            result = true;
        }
        if (eventOnceDic.ContainsKey(msg))
        {
            eventOnceDic[msg](evt);
            eventOnceDic.Remove(msg);
            result = true;
        }
        return result;
    }
    /// <summary>
    /// 移除事件监听
    /// </summary>
    public void RemoveEventListener(string msg, eventDelegate func)
    {
        if (eventDic.ContainsKey(msg))
        {
            eventDic[msg] -= func;
            if (eventDic[msg] == null) {
                eventDic.Remove(msg);
            }
        }
    }
}
