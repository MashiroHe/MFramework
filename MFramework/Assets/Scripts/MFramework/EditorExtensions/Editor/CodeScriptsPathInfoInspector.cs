/********************************************************************
created:  2022/1/16  17:45
filename: CodeScriptsPathInfoInspector.cs
author:	  Mashiro Shiina
e-mail address:1967407707@qq.com
filefullpath:G:/MGDemo/EditorExtensions/Assets/EditorExtensions/Editor/CodeScriptsPathInfoInspector.cs
purpose:   编辑器扩展自定义 显示 Inspector 面板信息 
********************************************************************/
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace EditorExtensions
{
	[CustomEditor(typeof(CodeScriptsPathInfo),editorForChildClasses:true)]
	public class CodeScriptsPathInfoInspector : Editor
	{
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            CodeScriptsPathInfo codeScriptsPathInfo = target as CodeScriptsPathInfo;
            GUILayout.BeginVertical("box");
            GUILayout.Label("Spawn path", new GUIStyle() 
            {
                fontSize=12,
                fontStyle=FontStyle.Bold
            });
            GUILayout.Space(1);
            GUILayout.BeginHorizontal();
            GUILayout.Label("scriptsFolderPath:", GUILayout.Width(120));
            codeScriptsPathInfo.m_ScriptsFolderPath = GUILayout.TextField(codeScriptsPathInfo.m_ScriptsFolderPath);
            GUILayout.EndHorizontal();
           
            GUILayout.Space(1);
            GUILayout.BeginHorizontal();
            codeScriptsPathInfo.IsSpawnPrefab = GUILayout.Toggle(codeScriptsPathInfo.IsSpawnPrefab, " isSpawnPrefab");
            GUILayout.EndHorizontal();
          
            if (codeScriptsPathInfo.IsSpawnPrefab)
            {
                GUILayout.Space(1);
                GUILayout.BeginHorizontal();
                GUILayout.Label("prefabFolderPath:", GUILayout.Width(120));
                codeScriptsPathInfo.m_PrefabFolderPath = GUILayout.TextField(codeScriptsPathInfo.m_PrefabFolderPath);
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();

        }
    }
}
