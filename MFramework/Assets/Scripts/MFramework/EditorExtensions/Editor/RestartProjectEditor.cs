/********************************************************************
created:  2022/1/17  0:2
filename: RestartProjectEditor.cs
author:	  Mashiro Shiina
e-mail address:1967407707@qq.com
filefullpath:G:/MGDemo/EditorExtensions/Assets/EditorExtensions/Editor/RestartProjectEditor.cs
purpose:    实现一键重启项目
********************************************************************/
using UnityEngine;
using UnityEditor;

namespace EditorExtensions
{
	public class RestartProjectEditor 
	{
		[MenuItem("Tools/EditorExtensions/Settings/RestartProject")]
		public static void DoRestartProject()
        {
			EditorApplication.OpenProject(Application.dataPath + "/../");
        }
	}
}
