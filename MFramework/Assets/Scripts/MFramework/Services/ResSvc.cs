/********************************************************************
	created:	2021/09/07
	created:	7:9:2021   18:27
	filename: 	D:\taikr_unityproject\3DGame\DarkGod\DarkGod\Assets\Scripts\Service\ResSvc.cs
	file path:	D:\taikr_unityproject\3DGame\DarkGod\DarkGod\Assets\Scripts\Service
	file base:	ResSvc
	file ext:	cs
	author:		Mashiro
	
	purpose:	资源服务模块  即资源管理器
*********************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml;
namespace MFramework
{
    public class ResSvc : MonoSingleton<ResSvc>
    {
        public void InitResSvc()
        {
            //配置数据加载读取缓存
            Debug.Log("InitResSvc");
        }
      
     
    }
}
