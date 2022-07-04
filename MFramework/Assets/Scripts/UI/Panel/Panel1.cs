/********************************************************************
created:  2022/7/3  14:8
filename: Panel1.cs
author:	  Mashiro Shiina
e-mail address:1967407707@qq.com
filefullpath:Assets/Scripts/UI/Panel/Panel1.cs
purpose:    
********************************************************************/
using UnityEngine;
using UnityEngine.UI;
using EditorExtensions;

namespace MFramework
{
	public  class Panel1Data :IPanelData 
	{
	}
	public partial class Panel1  : PanelBase
	{
		 private Panel1Data  m_Data;
		 public override void OnInit(IPanelData data=null)
		 {
			  base.OnInit();
			  this.m_Data= data as Panel1Data;
			/* Here code ui logic :*/

		 }
	}
}
