using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TimeCallBack();
public delegate void TimeFunction();
public class Timer : EventDispatcher {
    private float m_time;
    private float m_totalTime;
    private int m_count;
    private bool m_switch;
    private float m_resttime;
    private int m_restcount;
    private TimeCallBack m_timeCallBack;

    public TimeFunction onUpdate;
    public TimeFunction onStop;
    public TimeFunction onPause;
    public TimeFunction onStart;
    public TimeFunction onContinue;
    public TimeFunction onReset;

    /// <summary>
    /// 创建一个计时器
    /// </summary>
    /// <param name="time">计时间隔时间（秒）</param>
    public Timer(float time) {
        m_time = time;
        m_totalTime = 0;
        m_count = 0;
    }
    /// <summary> 获取计时总时间 </summary>
    public float totalTime { get { return m_totalTime; } }
    /// <summary> 获取计时次数 </summary>
    public float count { get { return m_count; } }

    /// <summary>
    /// 设置计时器间隔时间（秒）
    /// </summary>
    /// <param name="time">计时间隔时间（秒）</param>
    public void SetTime(float time)
    {
        m_time = time;
    }
    /// <summary>
    /// 开启倒计时计时器（秒）
    /// </summary>
    /// <param name="time">计时时间（秒）</param>
    public void StartTime(float time)
    {
        Stop();
        m_resttime = time;
        Start();
    }
    /// <summary>
    /// 开启倒计时计时器（秒）
    /// </summary>
    /// <param name="time">计时时间（秒）</param>
    public void StartTime(float time, TimeCallBack callBack)
    {
        StartTime(time);
        m_timeCallBack = callBack;
    }
    /// <summary>
    /// 开启倒计次数
    /// </summary>
    /// <param name="conut">计次次数</param>
    public void StartCount(int conut)
    {
        Stop();
        m_restcount = conut;
        Start();
    }
    /// <summary>
    /// 开启倒计时计时器（秒）
    /// </summary>
    /// <param name="time">计时时间（秒）</param>
    public void StartCount(int conut, TimeCallBack callBack)
    {
        StartCount(conut);
        m_timeCallBack = callBack;
    }
    /// <summary>
    /// 开启计时器
    /// </summary>
    public void Start() {
        m_switch = true;
        if (m_count == 0)
        {
            if (this.onStart != null) this.onStart();
        }
        else
        {
            if (this.onContinue != null) this.onContinue();
        }

        EventManager.instance.Dispatch(GameEvent.START_COROUTINE, StartLoop());
    }

    /// <summary>
    /// 暂停计时器
    /// </summary>
    public void Pause()
    {
        m_switch = false;
        if (this.onPause != null) this.onPause();
        EventManager.instance.Dispatch(GameEvent.STOP_COROUTINE, StartLoop());
    }
    
    /// <summary>
    /// 停止计时器
    /// </summary>
    public void Stop()
    {
        m_timeCallBack = null;
        m_restcount = 0;
        m_resttime = 0;
        m_switch = false;
        if (this.onStop != null) this.onStop();
        m_count = 0;
        m_totalTime = 0;
        EventManager.instance.Dispatch(GameEvent.STOP_COROUTINE, StartLoop());
    }
    /// <summary>
    /// 重置计时器（不停止计时）
    /// </summary>
    public void Reset()
    {
        m_count = 0;
        m_totalTime = 0;
        if (this.onReset != null)this.onReset();
    }
    IEnumerator StartLoop()
    {
        while (m_switch)
        {
            if (m_restcount != 0 && m_count >= m_restcount)
            {
                m_timeCallBack();
                Stop();
                yield break;
            }
            if (m_resttime != 0 && m_totalTime >= m_resttime)
            {
                m_timeCallBack();
                Stop();
                yield break;
            }
            if(this.onUpdate!=null)this.onUpdate();
            m_count++;
            m_totalTime+=m_time;
            yield return new WaitForSeconds(m_time);
        }

    }
    
}
