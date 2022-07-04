using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MFramework;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class PanelBase : MonoBehaviour, IPanel 
{ 
    protected LoaderManager loaderManager;
    protected AudioSvc audioSvcMananger = null;
    protected TimerSvc timerSvc;
    protected ResSvc resSvc = null;
    private UIRoot uiRoot;

    public virtual void OnInit(IPanelData panelData=null)
    {
        if(loaderManager==null)
        loaderManager = LoaderManager.Instance;
        if (uiRoot == null)
            uiRoot = UIRoot.Instance;
    }
    public virtual void OnAwake()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnStart()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnLateUpdate()
    {
        throw new System.NotImplementedException();
    }
    protected void OpenPanel(GameObject go)
    {
        uiRoot.OpenPanel(go);
    }
    protected void OpenPanel(PanelType panelType, params object[] objs)
    {
        uiRoot.OpenPanel(panelType,objs);
    }
    protected void ClosePanel(GameObject go)
    {
        uiRoot.ClosePanel(go);
    }
    protected void ClosePanel(PanelType panelType, params object[] objs)
    {
        uiRoot.ClosePanel(panelType, objs);
    }
    #region  UI优化 SetActive 公共函数/方法
    protected void SetActive(GameObject go, bool isActive)//1 游戏物体
    {
        go.SetActive(isActive);
    }
    protected void SetActive(GameObject go, bool isActive, float delayTime)//1 游戏物体
    {
        StartCoroutine(DelayImplementSetActive(go, isActive, delayTime));
    }
    private IEnumerator DelayImplementSetActive(GameObject go, bool isActive, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        go.SetActive(isActive);
    }
    protected void SetActive(Transform trans, bool state = true) //2 Transform组件
    {
        trans.gameObject.SetActive(state);
    }
    protected void SetActive(RectTransform rectTrans, bool state = true) //3 RectTransform组件
    {
        rectTrans.gameObject.SetActive(state);
    }
    protected void SetActive(Image img, bool state = true) //4  图片
    {
        img.transform.gameObject.SetActive(state);
    }
    protected void SetActive(Text txt, bool state = true) //5 文本
    {
        txt.transform.gameObject.SetActive(state);
    }
    #endregion
    #region  UI优化 SetText 公共函数/方法
    //1
    protected void SetText(Text txt, string content = "")
    {
        txt.text = content;
    }
    //2
    protected void SetText(Text txt, int num = 0)
    {
        //1
        SetText(txt, num.ToString());
    }
    //3
    protected void SetText(Transform trans, int num = 0)
    {
        //2
        SetText(trans.GetComponent<Text>(), num);
    }
    //4
    protected void SetText(Transform trans, string context = "")
    {
        //1
        SetText(trans.GetComponent<Text>(), context);
    }
    #endregion
    #region 切花设置Image
    protected void SetImage(Image img, string path)
    {
        Sprite sprite = loaderManager.LoadSpriteAssetsByPath(path, true);
        if (sprite != null)
        {
            img.sprite = sprite;
        }
    }
    #endregion
    #region  UI触摸点击事件  公用函数/方法
    //添加组件函数，首先获取游戏物体身上该组件，如果没有 则天机该组件
    protected T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T t = null;
        t = go.GetComponent<T>();
        if (t == null)
        {
            t = go.AddComponent<T>();
        }
        return t;
    }
    //点击
    protected void OnClickEvent(GameObject go, Action<PointerEventData, object> OnClick, object args)
    {
        PETouchListenner pETouchListenner = GetOrAddComponent<PETouchListenner>(go);
        pETouchListenner.OnClick = OnClick;
        pETouchListenner.args = args;
    }
    //拖拽
    protected void OnTouchDragEvent(GameObject go, Action<PointerEventData> OnTouchDrag)
    {
        PETouchListenner pETouchListenner = GetOrAddComponent<PETouchListenner>(go);
        pETouchListenner.OnTouchDrag = OnTouchDrag;
    }
    protected void OnTouchDownEvent(GameObject go, Action<PointerEventData> OnTouchDown)
    {
        PETouchListenner pETouchListenner = GetOrAddComponent<PETouchListenner>(go);
        pETouchListenner.OnTouchDown = OnTouchDown;
    }
    protected void OnTouchUpEvent(GameObject go, Action<PointerEventData> OnTouchUp)
    {
        PETouchListenner pETouchListenner = GetOrAddComponent<PETouchListenner>(go);
        pETouchListenner.OnTouchUp = OnTouchUp;
    }
    #endregion
}
