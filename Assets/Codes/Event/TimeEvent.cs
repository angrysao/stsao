using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEvent : BaseEvent{
    protected float m_deltaTime;
    public TimeEvent(object listener, object[] args):base(listener,args)
    {
        m_deltaTime = (float)args[0];
    }
    public object deltaTime { get { return m_deltaTime; } }
}
