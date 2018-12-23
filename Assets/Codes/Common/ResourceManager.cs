using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : EventDispatcher {

    private static ResourceManager m_instance;
    public static ResourceManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new ResourceManager();
            }
            return m_instance;
        }
    }

    /// <summary>
    /// 加载预制体(后续添加对象池和assetsbundle等内容)
    /// </summary>
    /// <param name="path">预制体路径</param>
    /// <returns></returns>
    public GameObject Load(string path)
    {
        return Resources.Load<GameObject>(path);
    }
    /// <summary>
    /// 销毁预制体(后续添加对象池和assetsbundle等内容)
    /// </summary>
    /// <param name="path">预制体路径</param>
    /// <returns></returns>
    public void Despose(GameObject gameObject)
    {
        GameObject.Destroy(gameObject);
    }
}
