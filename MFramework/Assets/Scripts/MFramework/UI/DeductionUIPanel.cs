/********************************************************************
    created: 2022/06/24
    filename: GameRoot.cs
    author:     Mashiro Shiina
    e-mail address:1967407707@qq.com 
    date: 24:6:2022   15:29
    purpose: 应用启动脚本
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MFramework;
public class DeductionUIPanelData : IPanelData
{
}
public partial class DeductionUIPanel : PanelBase
{
    private DeductionUIPanelData m_Data;
    public override void OnInit(IPanelData data=null)
    {
        base.OnInit();
        this.m_Data = data as DeductionUIPanelData;
        LeftBtn2.onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                ClosePanel(this.gameObject);
                //loaderManager.LoadSceneSycn(SceneConsts.SelectScene);
                OpenPanel(PanelType.TargetSelectPanel);
                Debug.Log("3");
            }
        });
    }
}

