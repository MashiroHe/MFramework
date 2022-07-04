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
public class TargetSelectPanelData : IPanelData
{
}
public partial class TargetSelectPanel : PanelBase
{
    private TargetSelectPanelData m_Data;
    public override void OnInit(IPanelData data=null)
    {
        base.OnInit();
        this.m_Data = data as TargetSelectPanelData;

        ClosePanel(this.gameObject);
    }
}

