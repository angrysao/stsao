using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestBtn : MonoBehaviour {
    Button m_btn;
    // Use this for initialization
    void Start () {
        m_btn = this.GetComponent<Button>();
        m_btn.onClick.AddListener(() => {
            if (ViewManager.instance.GetPanel("TestView") == null)
                ViewManager.instance.OpenView("TestView");
            else
            {

            }
            //SceneManager.instance.CreateGravityObject<BaseSprite>("Prefabs/Test/Cube", new Vector3(0,200,0)); 
        });
	}
    void Test(Timer t)
    {
        print(t.totalTime);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
