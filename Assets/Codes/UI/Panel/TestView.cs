using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestView : BaseView {

    protected string m_viewPath;
    private Button closeBtn;
    private Button createObjectBtn;
    private Button createSourceBtn;
    private Button createMeshBtn;

    public TestView() : base()
    {
        this.SetPath("testPanel");
    }
    public override void Start()
    {
        base.Start();
        closeBtn = this.GetChild<Button>("closeBtn");
        createObjectBtn = this.GetChild<Button>("createObjectBtn");
        createSourceBtn = this.GetChild<Button>("createSourceBtn");
        createMeshBtn = this.GetChild<Button>("createMeshBtn");
        this.SetCloseBtn(closeBtn);
        createObjectBtn.onClick.AddListener(OnClickCreateObject);
        createSourceBtn.onClick.AddListener(OnClickCreateSource);
        createMeshBtn.onClick.AddListener(OnClickCreateMesh);
    }
    protected override void AddEventListener()
    { }
    protected override void RemoveEventListener()
    { }
    private void OnClickCreateObject()
    {
        Debug.Log(1);
    }
    private void OnClickCreateSource()
    {
        Debug.Log(2);
    }
    private void OnClickCreateMesh()
    {
        Debug.Log(3);
    }
}
