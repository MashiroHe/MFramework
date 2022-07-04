/********************************************************************
created:  2022/1/16  12:46
filename: DefaultFilePathConfig.cs
author:	  Mashiro Shiina
e-mail address:1967407707@qq.com
filefullpath:G:/MGDemo/EditorExtensions/Assets/EditorExtensions/Editor/DefaultFilePathConfig.cs
purpose:    
********************************************************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
namespace EditorExtensions
{
	//�״δ��� ����·�������ļ�.assetʱ ʹ��
	//[CreateAssetMenu(fileName = "DefaultFilePathConfig",menuName = "CreateDefaultFilePathConfig",order =0)]
	public class DefaultFilePathConfig :ScriptableObject
	{
		[Header("�༭����չ�����ɽű�Ĭ�ϴ洢·����")]
		public string m_ScriptsFolderPath;//"Assets/Scripts/UI/Common";

		[Header("�༭����չ������Ԥ����Ĭ�ϴ洢·����")]
		public string m_PrefabFolderPath;//"Assets/GameData/Prefabs";
	}
	[CustomEditor(typeof(DefaultFilePathConfig))]
	public class DefaultFilePathConfigInspector : Editor
    {
		public SerializedProperty m_ScriptsFolderPath;
		public SerializedProperty m_PrefabFolderPath;
        private void OnEnable()
        {
			m_ScriptsFolderPath = serializedObject.FindProperty("m_ScriptsFolderPath");
			m_PrefabFolderPath = serializedObject.FindProperty("m_PrefabFolderPath");

		}
        public override void OnInspectorGUI()
        {
			serializedObject.Update();
			EditorGUILayout.PropertyField(m_ScriptsFolderPath, new GUIContent("path:"));
			GUILayout.Space(2);
			EditorGUILayout.PropertyField(m_PrefabFolderPath, new GUIContent("path:"));
			serializedObject.ApplyModifiedProperties();
		}
	}
	//�����ṩ�� ��ȡĬ��·�����ò����� �ӿ�
	public class EditorExtensionsConfig
    {
		private const string m_ConfigAssetFilePath = "Assets/Scripts/MFramework/EditorExtensions/Editor/DefaultFilePathConfig.asset";
		public static DefaultFilePathConfig GetConfig()
        {
			DefaultFilePathConfig m_Config = AssetDatabase.LoadAssetAtPath<DefaultFilePathConfig>(m_ConfigAssetFilePath);
			return m_Config;
		}
	}
}
