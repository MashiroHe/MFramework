using UnityEngine;
using UnityEditor;
using System.IO;

namespace EditorExtensions
{
    public class SelfDefineScriptsEditor : EditorWindow
    {
        //judge whether edtior window is opening
        private static bool isShowWnd = false;
        //spawn self define scripts
        [MenuItem("Tools/EditorExtensions/Create/SelfDefineScripts %#D")]//Crl+Shift+D
        public static void CreateSelfDefineScripts()
        {
            EditorWindow editorWindow = GetWindow(typeof(SelfDefineScriptsEditor), false, "CreateSelfDefineScripts", true);
            if (isShowWnd)
            {
                editorWindow.Close();
                isShowWnd = false;
            }
            else
            {
                editorWindow.Show();
                isShowWnd = true;
            }
        }
        private string namspaceTxt = "";
        private string classNameTxt = "";
        private string spawnPath = "Scripts/Common";
        private void OnEnable()
        {
            spawnPath = EditorPrefs.GetString("SpawnPath_Key", defaultValue: "Scripts/Common");
            namspaceTxt = NamespaceSettings.Namespace;
        }
        private void OnGUI()
        {
            GUILayout.Space(2);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Namespace:", GUILayout.Width(100));
            namspaceTxt = EditorGUILayout.TextField(namspaceTxt, GUILayout.Width(200), GUILayout.Height(20));
            GUILayout.EndHorizontal();

            GUILayout.Space(2);
            GUILayout.BeginHorizontal();
            GUILayout.Label("ClassName:", GUILayout.Width(100));
            classNameTxt = EditorGUILayout.TextField(classNameTxt, GUILayout.Width(200), GUILayout.Height(20));
            GUILayout.EndHorizontal();

            GUILayout.Space(2);
            GUILayout.BeginHorizontal();
            GUILayout.Label("SpawnPath:", GUILayout.Width(100));
            spawnPath = EditorGUILayout.TextField(spawnPath, GUILayout.Width(200), GUILayout.Height(20));
            GUILayout.EndHorizontal();

            GUILayout.Space(2);
            if (GUILayout.Button("Spawn Scripts", GUILayout.Width(330), GUILayout.Height(20)))
            {
                if (!string.IsNullOrWhiteSpace(classNameTxt) && !string.IsNullOrWhiteSpace(spawnPath))
                {
                    if (!NamespaceSettings.Namespace.Equals(namspaceTxt))
                    {
                        NamespaceSettings.Namespace = namspaceTxt;
                    }
                    string spawnScriptsPath = Application.dataPath + "/" + spawnPath + "/" + classNameTxt + ".cs";
                    string directoryPath = Application.dataPath + "/" + spawnPath;
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    if (!File.Exists(spawnScriptsPath))
                    {
                        SelfDefineScriptsTemplate.CreateScripts(spawnScriptsPath, classNameTxt);
                        EditorPrefs.SetString("SpawnPath_Key", spawnPath);
                        Debug.Log(string.Format("Spawn {0} script success!",classNameTxt));
                        AssetDatabase.Refresh();
                        Close();
                    }
                    else
                    {
                       Debug.Log(string.Format("Exists the same {0} file in {1} directory!", classNameTxt, directoryPath));
                    }
                }
                else
                {
                   Debug.Log("Input information error,please check ClassName reinput again!");
                }
            }
        }
        #region CreateSelfDefineScripts

        #endregion
    }
}
