using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 事件管理器，单例
/// </summary>
public class EventManager : EventDispatcher {

    private EventManager() {}
    private static EventManager m_instance;
    public static EventManager instance
    { get {
            if (m_instance == null)
            {
                m_instance = new EventManager();
            }
            return m_instance;
        }
    }
}
