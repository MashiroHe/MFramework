using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.Callbacks;
using System;
using System.Linq;
using System.Reflection;

namespace EditorExtensions
{
    public  class CreateComponentEditor:EditorWindow
    {
        #region Public MenuItem
        //绑定游戏物体数据信息
        private static List<BindInfo> m_BindInfoList = new List<BindInfo>();

        [MenuItem("Tools/EditorExtensions/Settings/Namespace")]
        public static void SetSpawnScriptsNamespace()
        {
            EditorWindow editorWindow = GetWindow(typeof(CreateComponentEditor),false, "Set Namespace",true);
            editorWindow.Show();
        }
        private string namspaceTxt ="";
        private static string Namespace_Key="";//用于存储获取显示修改后的命名空间
        private void OnEnable()
        {
            namspaceTxt = EditorPrefs.GetString(Namespace_Key, defaultValue: NamespaceSettings.Namespace);
            EditorPrefs.DeleteKey(Namespace_Key);
        }
        private void OnGUI()
        {
            GUILayout.Space(2);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Namespace:", GUILayout.Width(100));
            namspaceTxt =EditorGUILayout.TextField(namspaceTxt, GUILayout.Width(200), GUILayout.Height(20));
            GUILayout.EndHorizontal();
            GUILayout.Space(2);
            if (GUILayout.Button("Use and Spawn Scripts", GUILayout.Width(350), GUILayout.Height(20))&&Selection.activeGameObject)
            {
                if (!string.IsNullOrWhiteSpace(namspaceTxt))
                {
                    NamespaceSettings.Namespace = namspaceTxt;
                    CreateCode();
                    Close();
                }
                else
                {
                    Debug.Log("Input Namespace error,please reinput again!");
                }
            }
            GUILayout.Space(1);
            if (GUILayout.Button("Use", GUILayout.Width(350), GUILayout.Height(20)))
            {
                if (!string.IsNullOrWhiteSpace(namspaceTxt))
                {
                    NamespaceSettings.Namespace = namspaceTxt;
                    Debug.Log("Use Namespace: "+NamespaceSettings.Namespace+" success!");
                    Close();
                }
                else
                {
                    Debug.Log("Input Namespace error,please reinput again!");
                }
            }
        }

        [MenuItem("GameObject/EditScriptsPathInfo", priority = 11, validate =true)]
        public static bool ValidateAddCodeScriptsPathInfo()
        {
            return Selection.activeObject;
        }
        [MenuItem("GameObject/EditScriptsPathInfo %#E", priority = 11)]
        public static  void AddCodeScriptsPathInfo()
        {
            GameObject selectObj = Selection.activeGameObject;
            CodeScriptsPathInfo codeGenerateInfo= selectObj.GetComponent<CodeScriptsPathInfo>();
            if (codeGenerateInfo == null)
            {
                codeGenerateInfo = selectObj.AddComponent<CodeScriptsPathInfo>();
            }
            else
            {
                Debug.Log("This scripts had already added on selected gameobject!");
            }
        }

        [MenuItem("GameObject/SpawnOrAddScripts(GetChildNode)", priority = 12,validate =true)]
        public static bool ValidateCreateCode()
        {
            if (!Selection.activeObject)
            {
                Debug.Log("Don't select anyting!");
            }
            return Selection.activeObject;
        }
       [MenuItem("GameObject/SpawnOrAddScripts(GetChildNode) %#C", priority =12)]
        public static void CreateCode()
        {
            //所生成 脚本 存储路径
            string directoryPath = EditorExtensionsConfig.GetConfig().m_ScriptsFolderPath; //Application.dataPath + "/Scripts/UI/Common";
            GameObject[] gameObjects = Selection.gameObjects;
            GameObject rootGO= gameObjects[0];

            //获取生成文件路径编辑脚本
            CodeScriptsPathInfo codeScriptsPathInfo = rootGO.GetComponent<CodeScriptsPathInfo>();
            if (codeScriptsPathInfo != null)
            {
                directoryPath = codeScriptsPathInfo.m_ScriptsFolderPath;
            }
            //s是否存在存储路径
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string spawnScriptsPath = directoryPath + "/" + rootGO.name + ".cs";//或者 directoryPath +  $"/{rootGO.name}.cs"                                                                       
            string spawnDesignerScriptsPath = directoryPath + "/"+rootGO.name+".Designer.cs";//或者 directoryPath +  $"/{rootGO.name}.cs"
          //Spawn Scripts ,if exists directly add to gameobject ,if not exists create it then add to game object
            if (File.Exists(spawnScriptsPath))
            {
                AddScriptsToGameObject(rootGO.name);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(NamespaceSettings.Namespace))
                {
                    NamespaceSettings.Namespace = "DefaultNamespace";
                }
                //先添加绑定Bind脚本 进行标记 然后 再搜索绑定物体  进行信息添加
                AddBindScriptsToObject(rootGO);
                //然后 根据绑定物体 名字信息 依次创建 根脚本 字段变量，并对其进行赋值
               //直接挂在游戏物体上，进行逻辑代码编写以 ".cs"结尾 使用partial关键字 使一个脚本分为俩个脚本 使用,分别承担不同的作用，用以避免逻辑代码被覆盖
                ComponentTemplate.CreateScripts(spawnScriptsPath,rootGO.name);
                //不挂载在游戏物体身上，但是，可以用于 存储赋值变量字段以".Designer.cs"结尾
                ComponentDesignerTemplate.CreateScripts(spawnDesignerScriptsPath, rootGO.name, m_BindInfoList);
                EditorPrefs.SetString("Generate_Class_Name", rootGO.name);
                Debug.Log("Spawn Scripts success!");
                AssetDatabase.Refresh();
            }
        }
        //手动添加要获取子物体GameObjec以及Transform组件的绑定标记脚本
        [MenuItem("GameObject/AddBindScripts ", priority = 13, validate = true)]
        public static bool ValidateBindScripts()
        {
            if (!Selection.activeObject)
            {
                Debug.Log("Don't select anyting!");
            }
            return Selection.activeObject;
        }
        [MenuItem("GameObject/AddBindScripts %#B", priority = 13)]
        public static void BindScripts()
        {
            GameObject[] gameObjects = Selection.gameObjects;
            foreach(var bindGO in gameObjects)
            {
                Bind bind = bindGO.GetComponent<Bind>();
                if (bind == null)
                {
                    bindGO.AddComponent<Bind>();
                }
            }
        }
            #endregion

            #region  Private Method
            [DidReloadScripts]
        private static void AddComponentToGameObject()
        {
            string generateClassName = EditorPrefs.GetString("Generate_Class_Name");
            EditorPrefs.DeleteKey("Generate_Class_Name");
            if(string.IsNullOrWhiteSpace(generateClassName))
            {
                Debug.Log("DidReloadScripts!");
            }
            else
            {
                AddScriptsToGameObject(generateClassName);
            }
        }

        //Get exists scripts,then add to gameobject,if exist pop up  tips 
        private static void AddScriptsToGameObject(string spawnScriptsName)
        {
            //将自己生成的脚本 自动添加到 游戏物体身上
            Type type = null;
            AppDomain appDomain = AppDomain.CurrentDomain;
            //获取要添加脚本的类型 方式 1
            //Assembly assemblyCSharp = appDomain.GetAssemblies().First(assembly => assembly.GetName().Name == "Assembly-CSharp");
            //type = assemblyCSharp.GetType(generateClassName);

            //获取要添加脚本的类型 方式 2
            //or write this :
            foreach (Assembly assembly in appDomain.GetAssemblies())
            {

                if (assembly.GetName().Name == "Assembly-CSharp")
                {
                    string addNamespaceName = NamespaceSettings.Namespace + "." + spawnScriptsName;
                    type = assembly.GetType(addNamespaceName);
                    break;
                }
            }
            //error verification
            if (type != null)
            {
                GameObject go = GameObject.Find(spawnScriptsName);
                Component component = go.GetComponent(type);
                if (component == null)
                {
                    Component rootScripts = go.AddComponent(type);
                    Debug.Log(string.Format("AddComponent {0} To this GameObject success!", spawnScriptsName));
                    //获取 搜索绑定物体  进行信息添加
                    AddBindScriptsToObject(go);
                    //使用序列化对象赋值，以及生成与游戏物体预制体
                    SerializedObjectAndSpawnPrefab(go,rootScripts);
                }
                else
                {
                    Debug.Log(string.Format("This Component {0} had already been Added on GameObject successfully!", spawnScriptsName));
                }
            }
            //每次修改生成脚本 命名空间后，进行初始化操作，以避免再次生成其他脚本时，使用上一次修改的命名空间
            //when modifying scripts namespace,in order to use last modifyed scripts namespace ,here Initial namespace to  "DefaultNamespace"
            // NamespaceSettings.Namespace = "DefaultNamespace";
            EditorPrefs.SetString(Namespace_Key,NamespaceSettings.Namespace);
            Debug.Log(string.Format("Initial scripts namespace to {0}", NamespaceSettings.Namespace));
        }
        //使用序列化对象赋值，以及生成与游戏物体预制体
        private static void SerializedObjectAndSpawnPrefab(GameObject go ,Component rootScripts)
        {
            SerializedObject serializedObject = new SerializedObject(rootScripts);
            //然后根据绑定物体 信息得到其名字，根据名字进行序列化赋值操作
            foreach (var bindInfo in m_BindInfoList)
            {
                string objName = "m_"+bindInfo.Name;//bindInfo.ObjNamePath.Split('/').Last();
                if (!string.IsNullOrEmpty(serializedObject.FindProperty(objName).name))
                {
                    serializedObject.FindProperty(objName).objectReferenceValue = go.transform.Find(bindInfo.ObjNamePath).gameObject;// GameObject.Find(objName);
                    string componentName = (bindInfo.Name.ToLower() + bindInfo.ComponentName.Substring(0, 5));
                    
                    serializedObject.FindProperty(componentName).objectReferenceValue = go.GetComponent(bindInfo.ComponentName);
                   
                }
            }
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
            //赋值存储自身脚本生成路径
            CodeScriptsPathInfo codeScriptsPathInfo = go.GetComponent<CodeScriptsPathInfo>();
            if (codeScriptsPathInfo != null)
            {
                serializedObject.FindProperty("m_ScriptsFolderPath").stringValue = codeScriptsPathInfo.m_ScriptsFolderPath;
                //生成 prefab 到指定路径
                if (codeScriptsPathInfo.IsSpawnPrefab)
                {
                    serializedObject.FindProperty("m_PrefabFolderPath").stringValue = codeScriptsPathInfo.m_PrefabFolderPath;
                    serializedObject.FindProperty("IsSpawnPrefab").boolValue = codeScriptsPathInfo.IsSpawnPrefab;
                    serializedObject.ApplyModifiedPropertiesWithoutUndo();
                    string directoryPath = Application.dataPath + codeScriptsPathInfo.m_PrefabFolderPath.Replace("Assets", "");
                    string prefabFullPath = codeScriptsPathInfo.m_PrefabFolderPath + "/" + go.name + ".prefab";
                    //Remove  CodeScriptsPathInfo scripts from gameobject
                    if (codeScriptsPathInfo != rootScripts)
                    {
                        DestroyImmediate(codeScriptsPathInfo, false);
                    }
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    //spawn prefab
                    if (!File.Exists(prefabFullPath))
                    {
                        PrefabUtility.SaveAsPrefabAssetAndConnect(go, prefabFullPath, InteractionMode.AutomatedAction);
                        Debug.Log(string.Format("Create {0} Prefab Success!", go.name));
                    }
                    else
                    {
                        PrefabUtility.ApplyPrefabInstance(go, InteractionMode.AutomatedAction);
                        Debug.Log(string.Format("Override {0} Prefab Success!", go.name));
                    }
                }
                else
                {
                    //Remove  CodeScriptsPathInfo scripts from gameobject
                    if (codeScriptsPathInfo != rootScripts)
                    {
                        DestroyImmediate(codeScriptsPathInfo, false);
                    }
                }
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }
            else
            {
                serializedObject.FindProperty("m_ScriptsFolderPath").stringValue = EditorExtensionsConfig.GetConfig().m_ScriptsFolderPath;// "Assets/Scripts/UI/Common";
                serializedObject.FindProperty("m_PrefabFolderPath").stringValue = EditorExtensionsConfig.GetConfig().m_PrefabFolderPath;// "Assets/GameData/Prefabs";
                serializedObject.FindProperty("IsSpawnPrefab").boolValue = false;
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }
        }
        private static void AddBindScriptsToObject(GameObject go)
        {
            foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
            {
                if (trans.name != go.name && !trans.GetComponent<Bind>())
                {
                   // trans.gameObject.AddComponent<Bind>();
                  //  Debug.Log(string.Format("Add Bind Scripts to{0} success!", trans.name));
                }
            }
            //搜索绑定子对象脚本
            m_BindInfoList.Clear();
            SearchBindObject("", go.transform, m_BindInfoList);
        }
        private static void SearchBindObject(string path,Transform rootTrans,List<BindInfo> binds)
        {
            Bind bind= rootTrans.GetComponent<Bind>();
            bool isRootObj = string.IsNullOrWhiteSpace(path);
            if (bind!=null&& !isRootObj)
            {
                binds.Add(new BindInfo()
                {
                    ObjNamePath = path,
                    Name=rootTrans.name,
                    ComponentName=bind.ComponentName
                });
            }
            foreach(Transform childTrans in rootTrans)
            {
                SearchBindObject(isRootObj?childTrans.name: path + "/" + childTrans.name, childTrans, binds);
            }
        }
        #endregion
    }

}
