using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewType {
    /// <summary>
    /// 场景ui特效
    /// </summary>
    SceneUIEffect = 1,
    /// <summary>
    /// 主界面
    /// </summary>
    MainUI = 2,
    /// <summary>
    /// 普通界面（界面名称唯一对应界面）
    /// </summary>
    Panel = 3,
    /// <summary>
    /// 提示界面（界面名称可对应多个界面）
    /// </summary>
    Tips = 4,
    /// <summary>
    /// 嵌套界面（从属于父界面）
    /// </summary>
    InnerPanel = 5
}

