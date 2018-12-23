using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
    private GameObject m_mainCanvas;
    private static Main m_gameManager;
    public static Main gameManager { get { return m_gameManager; } }
    Dictionary<IEnumerator, Coroutine> corDic;
	// Use this for initialization
	void Start () {
        m_gameManager = this;
        InitDontDestroy();
        corDic = new Dictionary<IEnumerator, Coroutine>();
        EventManager.instance.AddEventListener(GameEvent.START_COROUTINE, AddCoroutine);
        EventManager.instance.AddEventListener(GameEvent.STOP_COROUTINE, RemoveCoroutine);
    }
	
	// Update is called once per frame
	void Update () {
        EventManager.instance.Dispatch(GameEvent.GAME_LOOP, Time.deltaTime);
    }
    
    void AddCoroutine(BaseEvent evt) {
        IEnumerator ienumKey = (IEnumerator)evt.args[0];
        if (corDic.ContainsKey(ienumKey))
        {
            corDic.Remove(ienumKey);
        }
        Coroutine cor = StartCoroutine(ienumKey);
        corDic.Add(ienumKey, cor);
    }
    void RemoveCoroutine(BaseEvent evt) {
        IEnumerator ienumKey = (IEnumerator)evt.args[0];
        if (corDic.ContainsKey(ienumKey))
        {
            corDic.Remove(ienumKey);
        }
        else
        {
        }
    }
    void InitDontDestroy()
    {
        this.m_mainCanvas = GameObject.Find("mainCanvas");
        DontDestroyOnLoad(GameObject.Find("Root"));
        //DontDestroyOnLoad(GameObject.Find("EventSystem"));
        DontDestroyOnLoad(GameObject.Find("GameManager"));
    }
    public static GameObject GetCanvas()
    {
        return m_gameManager.m_mainCanvas;
    }

}
