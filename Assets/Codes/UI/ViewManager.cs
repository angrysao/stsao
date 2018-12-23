using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager:EventDispatcher{
    private const string UIEffectLay = "uiEffectLay";
    private const string MainUILay = "mainUILay";
    private const string PanelLay = "panelLay";
    private const string TipsLay = "tipsLay";

    private Transform m_uiEffectLay;
    private Transform m_mainUILay;
    public Transform m_panelLay;
    private Transform m_tipsLay;
    private Dictionary<string, BaseView> m_viewDic;//普通界面字典存储
    private List<BaseView> m_viewTipsList;//提示界面列表存储
    private string m_UIRoot = "Prefabs/Panel/";
    private ViewManager() {
        m_viewDic = new Dictionary<string, BaseView>();
        m_viewTipsList = new List<BaseView>();
        m_uiEffectLay = Main.GetCanvas().transform.Find(UIEffectLay);
        m_mainUILay = Main.GetCanvas().transform.Find(MainUILay);
        m_panelLay = Main.GetCanvas().transform.Find(PanelLay);
        m_tipsLay = Main.GetCanvas().transform.Find(TipsLay);
    }
    private static ViewManager m_instance;
    public static ViewManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new ViewManager();
            }
            return m_instance;
        }
    }
    /**默认打开界面层级的界面**/
    public BaseView OpenView(string viewName)
    {
        return OpenView(viewName, ViewType.Panel);
    }
    /// <summary>
    /// 打开界面
    /// </summary>
    /// <param name="viewName">界面名称</param>
    /// <param name="viewType">界面类型</param>
    /// <returns></returns>
    public BaseView OpenView(string viewName, ViewType viewType)
    {      
        BaseView view = (BaseView)Activator.CreateInstance(Type.GetType(viewName));
        Transform parentTrans;
        switch (viewType)
        {
            case ViewType.SceneUIEffect:
                parentTrans = m_uiEffectLay;
                break;
            case ViewType.MainUI:
                parentTrans = m_mainUILay;
                break;
            case ViewType.Panel:
                parentTrans = m_panelLay;
                m_viewDic.Add(viewName, view);
                break;
            case ViewType.Tips:
                parentTrans = m_tipsLay;
                m_viewTipsList.Add(view);
                break;
            default:
                parentTrans = Main.GetCanvas().transform;
                break;
        }
        view.gameObject = (GameObject)GameObject.Instantiate(Resources.Load(m_UIRoot+view.GetPath()), parentTrans);
        view.SetViewType(viewType);
        view.viewName = viewName;
        if(view.onLoadComplet!=null) view.onLoadComplet();
        view.Start();
        return view;
    }
    /// <summary>
    /// 打开镶嵌界面
    /// </summary>
    /// <param name="viewName">界面名称</param>
    /// <param name="viewType">界面父对象</param>
    /// <returns></returns>
    public BaseView OpenInnerView(string viewName, GameObject parent)
    {
        BaseView view = (BaseView)Activator.CreateInstance(Type.GetType(viewName));
        view.gameObject = (GameObject)GameObject.Instantiate(Resources.Load(m_UIRoot + view.GetPath()), parent.transform);
        view.SetViewType(ViewType.InnerPanel);
        view.viewName = viewName;
        if (view.onLoadComplet != null) view.onLoadComplet();
        view.Start();
        return view;
    }
    public bool RemoveView(BaseView view)
    {
        if (m_viewDic.ContainsKey(view.viewName) && (view.GetViewType() == ViewType.Panel))
        {
            m_viewDic.Remove(view.viewName);
            return true;
        }
        else if (m_viewTipsList.Contains(view) && (view.GetViewType() == ViewType.Tips))
        {
            m_viewTipsList.Remove(view);
            return true;
        }
        return false;
    }
    /// <summary>
    /// 获取panel界面
    /// </summary>
    /// <param name="viewName">界面名称</param>
    /// <returns></returns>
    public BaseView GetPanel(string viewName)
    {
        if (!m_viewDic.ContainsKey(viewName)) return null;// Debug.LogError("未找到界面："+viewName);
        return m_viewDic[viewName];
    }
    /// <summary>
    /// 关闭所有panel和tips界面
    /// </summary>
    public void CloseAllView()
    {
        foreach (var item in m_viewDic)
        {
            item.Value.Close();
        }
        foreach (BaseView view in m_viewTipsList)
        {
            view.Close();
        }
    }
}
