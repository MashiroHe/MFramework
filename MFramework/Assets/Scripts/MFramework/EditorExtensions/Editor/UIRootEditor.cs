using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

namespace EditorExtensions
{
    public class UIRootEditor : EditorWindow
    {
        #region MenuItem Method
        //创建UIRoot 并生成预制体
        [MenuItem("Tools/EditorExtensions/UI/CreateUIRoot", validate = true)]//校验UIRoot是否已经创建，存在则失效函数SetupUIRoot点击
        public static bool ValidateUIRoot()
        {
            return !GameObject.Find("UIRoot");
        }
        [MenuItem("Tools/EditorExtensions/UI/CreateUIRoot")]
        public static void CreateUIRoot()
        {
            EditorWindow window = GetWindow(typeof(UIRootEditor), false, "CreateUIRoot", true);
            // EditorWindow window1 = GetWindow<UIRootEditor>();
            window.Show();
        }

        //创建选中物体预制体
        [MenuItem("Tools/EditorExtensions/UI/CreatePrefab", true)]
        public static bool ValidateCreatePrefab()
        {
            return Selection.activeGameObject;
        }
        [MenuItem("Tools/EditorExtensions/UI/CreatePrefab")]
        public static void CreateToPrefab()
        {
            GameObject go = Selection.activeGameObject;
            if (go != null)
            {
                SaveToPrefab(go.name, go);
            }
        }
        //删除选中游戏物体以及其对应预制体
        [MenuItem("Tools/EditorExtensions/UI/Delete(Include its Prefab)", true)]
        public static bool ValidateDeletePrefab()
        {
            return Selection.activeGameObject;
        }
        [MenuItem("Tools/EditorExtensions/UI/Delete(Include its Prefab)", priority =500)]
        public static void DeletePrefab()
        {
            GameObject go = Selection.activeGameObject;
            if (go != null)
            {
                DeletePrefabFile(go.name);
                Editor.DestroyImmediate(go);
            }
        }
        #endregion

        private string width = "";
        private string height = "";
        private void OnGUI()
        {
            GUILayout.Space(5);//设置上下间距
            GUILayout.Label("Set Canvas Resolution:");//设置标签
            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Width", GUILayout.Width(35));
            width = EditorGUILayout.TextField("", width, GUILayout.Width(100), GUILayout.Height(20));//获取到将用户编辑器面板填入的数值

            GUILayout.Label("Height", GUILayout.Width(35));
            height = EditorGUILayout.TextField("", height, GUILayout.Width(100), GUILayout.Height(20));
            GUILayout.EndHorizontal();

            GUILayout.Space(2);
            //绘制显示编辑器点击按钮
            if (GUILayout.Button("Create and Set Resolution", GUILayout.Width(300), GUILayout.Height(20)) && !GameObject.Find("UIRoot"))
            {
                if (!string.IsNullOrEmpty(width) && !string.IsNullOrEmpty(height))
                {
                    float w = 0f;
                    float h = 0;
                    float.TryParse(width, out w);
                    float.TryParse(height, out h);
                    CreateSetupUIRoot(w, h, true);
                    Debug.Log("Create and Set Resolution Success!");
                    Close();
                }
                else
                {
                    Debug.LogError("Width or height is null!");
                }
            }
            GUILayout.Space(2);
            if (GUILayout.Button("(Only)Create", GUILayout.Width(300), GUILayout.Height(20)) && !GameObject.Find("UIRoot"))
            {
                CreateSetupUIRoot(0, 0);
                Debug.Log("Create UIRoot Success!");
                Close();
            }
        }

        #region EditorExtensions Method
        private static void CreateSetupUIRoot(float width, float height, bool isSetResolution = false)
        {
            //UIRoot
            GameObject uiRootObj = new GameObject("UIRoot");
            uiRootObj.layer = LayerMask.NameToLayer("UI");
            UIRoot uiRoot = uiRootObj.AddComponent<UIRoot>();
            //Canvas
            GameObject canvas = new GameObject("Canvas");
            canvas.layer = LayerMask.NameToLayer("UI");
            canvas.transform.SetParent(uiRootObj.transform);
            canvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

            //CanvasScaler 
            CanvasScaler canvasScaler = canvas.AddComponent<CanvasScaler>();
            if (isSetResolution)
            {
                canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                canvasScaler.referenceResolution = new Vector2(width, height);
                canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                canvasScaler.matchWidthOrHeight = 1;
            }
            canvas.AddComponent<GraphicRaycaster>();
            uiRoot.m_Canvas = canvas.GetComponent<Canvas>();

            //<EventSystem
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.layer = LayerMask.NameToLayer("UI");
            eventSystem.transform.SetParent(uiRootObj.transform);
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
            //uiRoot.m_EventSystem = eventSystem.GetComponent<EventSystem>();
            //BG
            GameObject bgUI = new GameObject("BG");
            bgUI.AddComponent<RectTransform>();
            bgUI.transform.SetParent(canvas.transform);
            bgUI.transform.localPosition = Vector3.zero;
            uiRoot.m_BG = bgUI.transform;
            //Common
            GameObject common = new GameObject("Common");
            common.AddComponent<RectTransform>();
            common.transform.SetParent(canvas.transform);
            common.transform.localPosition = Vector3.zero;
            uiRoot.m_Common = common.transform;
            //PopUI
            GameObject popUI = new GameObject("PopUI");
            popUI.AddComponent<RectTransform>();
            popUI.transform.SetParent(canvas.transform);
            popUI.transform.localPosition = Vector3.zero;
            uiRoot.m_PopUI = popUI.transform;
            //Forward
            GameObject forward = new GameObject("Forward");
            forward.AddComponent<RectTransform>();
            forward.transform.SetParent(canvas.transform);
            forward.transform.localPosition = Vector3.zero;
            uiRoot.m_Forward = forward.transform;

            //给脚本 私有字段对象 赋值
            SerializedObject so = new SerializedObject(uiRoot);
            so.FindProperty("m_EventSystem").objectReferenceValue = eventSystem.GetComponent<EventSystem>();
            so.ApplyModifiedPropertiesWithoutUndo();

            SaveToPrefab(uiRootObj.name, uiRootObj);
        }

        private static void SaveToPrefab(string name,GameObject go)
        {
            string directoryPath = Application.dataPath + "/GameData/Prefabs";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string prefabPath = directoryPath + "/" + name + ".prefab";
            if (!File.Exists(prefabPath))
            {
                PrefabUtility.SaveAsPrefabAssetAndConnect(go, prefabPath,InteractionMode.AutomatedAction);
                Debug.Log(string.Format("Create {0} Prefab Success!", name));
            }
            else
            {
                PrefabUtility.ApplyPrefabInstance(go,InteractionMode.AutomatedAction);
                Debug.Log(string.Format("{0} prefab already exists, override Success!", name));
            }
        }
        private static void DeletePrefabFile(string name)
        {
            string directoryPath = Application.dataPath + "/GameData/Prefabs";
            string prefabPath = directoryPath + "/" + name + ".prefab";
            if (File.Exists(prefabPath))
            {
                File.Delete(prefabPath);
                Debug.Log(string.Format("Delete {0} and its prefab Success!", name));
                AssetDatabase.Refresh();
            }
        }
        #endregion
    }
}