using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReActiveTool : MonoBehaviour
{
    //根目录Content 添加有ContentSizeFitter的父级游戏物体
    public GameObject parentObj;//ViewPort
    [SerializeField]
    public List<GameObject>  secondLvBtnObjList=new List<GameObject>();//第三级折叠列表点开的触发按钮;
    private ContentSizeFitter m_Csf;
    private float intervalTime = 0.03f;//refresh  interval time
    // Start is called before the first frame update
    private void Start()
    {
        m_Csf = parentObj.GetComponent<ContentSizeFitter>();
        AddListenerForBtn();
    }
    private void AddListenerForBtn()
    {
        foreach(GameObject go in secondLvBtnObjList)
        {
            go.GetComponent<Toggle>().onValueChanged.AddListener((isOn) =>
            {
                if (isOn)
                {
                    DetermineComponentState();
                }
                else
                {
                    DetermineComponentState();
                }
            });
        }
    }
    private void DetermineComponentState()
    {
        if (m_Csf.IsActive())
        {
            m_Csf.enabled = false;
            Invoke("InversionOfControl", intervalTime);
        }
        else
        {
            m_Csf.enabled = true;
            Invoke("InversionOfControl", intervalTime);
        }
    }
    private void InversionOfControl()
    {
        if (m_Csf.IsActive())
        {
            m_Csf.enabled = false;
        }
        else
        {
            m_Csf.enabled = true;
        }
    }

}
