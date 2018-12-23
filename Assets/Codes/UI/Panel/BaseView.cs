using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void ViewFunction();
public abstract class BaseView {
    private GameObject m_gameObject;
    private string m_viewPath;
    private ViewType m_viewType;
    private string m_viewName;

    /********事件*******/
    public ViewFunction onLoadComplet;
    public ViewFunction onDestroy;


    //面板gameobject对象
    public GameObject gameObject
    {
        set { this.m_gameObject = value; }
        get { return this.m_gameObject; }
    }
    //面板名称
    public string viewName
    {
        set { this.m_viewName = value; }
        get { return this.m_viewName; }
    }
    /// <summary>
    /// 构造函数中必须设置路径
    /// </summary>
    public BaseView()
    {
    }
    public void SetPath(string path)
    {
        this.m_viewPath = path;
    }
    public string GetPath()
    {
        return this.m_viewPath;
    }
    public void SetViewType(ViewType viewType)
    {
        this.m_viewType = viewType;
    }
    public ViewType GetViewType()
    {
        return this.m_viewType;
    }
    protected abstract void AddEventListener();
    protected abstract void RemoveEventListener();

    public virtual void Start()
    {
        this.AddEventListener();
    }
    /// <summary>
    /// 关闭界面
    /// </summary>
    public virtual void Close()
    {
        ViewManager.instance.RemoveView(this);
        this.RemoveEventListener();
        if (this.onDestroy != null) this.onDestroy();
        GameObject.Destroy(this.m_gameObject);
    }
    /// <summary>
    /// 获取子控件
    /// </summary>
    /// <param name="path">控件名称</param>
    /// <returns></returns>
    protected GameObject GetChild(string path)
    {
        return this.gameObject.transform.Find(path).gameObject;
    }
    /// <summary>
    /// 获取子控件
    /// </summary>
    /// <typeparam name="T">控件的组件类型</typeparam>
    /// <param name="path">控件名称</param>
    /// <returns></returns>
    protected T GetChild<T>(string path)
    {
        return this.gameObject.transform.Find(path).GetComponent<T>();
    }
    /// <summary>
    /// 设置关闭按钮
    /// </summary>
    protected void SetCloseBtn(Button btn)
    {
        btn.onClick.AddListener(this.Close);
    }

    /// <summary>
    /// 设置面板置顶按键
    /// </summary>
    protected void SetTopViewClick(Button btn)
    {
        btn.onClick.AddListener(this.gameObject.transform.SetAsLastSibling);
    }
}
