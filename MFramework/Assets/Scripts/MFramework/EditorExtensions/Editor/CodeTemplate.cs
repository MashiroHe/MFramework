using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

namespace EditorExtensions
{
    public class ComponentDesignerTemplate
    {
       //不挂载在游戏物体身上，但是，可以用于 存储赋值变量字段以".Designer.cs"结尾
        public static void CreateScripts(string spawnDesignerScriptsPath, string className, List<BindInfo> m_BindInfoList)
        {
            //创建脚本文件，并写入代码内容
            using (StreamWriter sw = File.CreateText(spawnDesignerScriptsPath))
            {
                //每次生成新的Guid,触发编译功能
                // sw.WriteLine("//Generate Id:{0}", Guid.NewGuid().ToString());//sw.WriteLine($"//Generate Id:{ Guid.NewGuid().ToString()}");
                sw.WriteLine("using UnityEngine;");
                sw.WriteLine("using UnityEngine.UI;");
                sw.WriteLine();
                sw.WriteLine("namespace {0}",NamespaceSettings.Namespace);
                sw.WriteLine("{");
                sw.WriteLine("\tpublic partial class {0} ", className);//或者 $"public class {className}: MonoBehaviour"
                sw.WriteLine("\t{");
                //存储对应路径信息
                // sw.WriteLine("\t\t[HideInInspector]");
                sw.WriteLine("\t\t[SerializeField] private string m_ScriptsFolderPath;");
                //sw.WriteLine("\t\t[HideInInspector]");
                sw.WriteLine("\t\t[SerializeField] private bool IsSpawnPrefab = false;");
                //sw.WriteLine("\t\t[HideInInspector]");
                sw.WriteLine("\t\t[SerializeField] private string m_PrefabFolderPath;");

                foreach (BindInfo bindInfo in m_BindInfoList)
                {
                    //  sw.WriteLine("\t\tpublic GameObject {0};", bindInfo.ObjNamePath.Split('/').Last());
                    sw.WriteLine("\t\tpublic GameObject {0};", "m_"+bindInfo.Name);
                    sw.WriteLine("\t\tpublic {0} {1};",bindInfo.ComponentName ,(bindInfo.Name.ToLower()+ bindInfo.ComponentName.Substring(0,5)));
                }
                sw.WriteLine("\t}");
                sw.WriteLine("}");
            }
        }
    }
    public  class ComponentTemplate
    {
        //直接挂在游戏物体上，进行逻辑代码编写以 ".cs"结尾 使用partial关键字 使一个脚本分为俩个脚本 使用,分别承担不同的作用，用以避免逻辑代码被覆盖
        public static void CreateScripts(string spawnScriptsPath, string className)
        {
            //创建脚本文件，并写入代码内容
            using (StreamWriter sw = File.CreateText(spawnScriptsPath))
            {
                sw.WriteLine("/********************************************************************");
                sw.WriteLine("created:  {0}/{1}/{2}  {3}:{4}", System.DateTime.Now.Date.Year, System.DateTime.Now.Date.Month, System.DateTime.Now.Date.Day, System.DateTime.Now.Hour, System.DateTime.Now.Minute);
                sw.WriteLine("filename: {0}.cs", className);
                sw.WriteLine("author:	  Mashiro Shiina");
                sw.WriteLine("e-mail address:1967407707@qq.com");
                sw.WriteLine("filefullpath:{0}", spawnScriptsPath);
                sw.WriteLine("purpose:    ");
                sw.WriteLine("********************************************************************/");
                sw.WriteLine("using UnityEngine;");
                sw.WriteLine("using UnityEngine.UI;");
                sw.WriteLine("using EditorExtensions;");
                sw.WriteLine();
                sw.WriteLine("namespace {0}", NamespaceSettings.Namespace);
                sw.WriteLine("{");
                sw.WriteLine("\tpublic  class {0} :IPanelData ", className+"Data");//MonoBehaviour//或者 $"public class {className}: MonoBehaviour"
                sw.WriteLine("\t{");

                sw.WriteLine("\t}");

                sw.WriteLine("\tpublic partial class {0}  : PanelBase", className);//MonoBehaviour//或者 $"public class {className}: MonoBehaviour"
                sw.WriteLine("\t{");
                sw.WriteLine("\t\t private {0}  m_Data;", className + "Data");
                sw.WriteLine("\t\t public override void OnInit(IPanelData data=null)");
                sw.WriteLine("\t\t {");
                sw.WriteLine("\t\t\t  base.OnInit();");
                sw.WriteLine("\t\t\t  this.m_Data= data as {0};", className + "Data");
                sw.WriteLine("\t\t\t //Here code ui logic :");
                sw.WriteLine();
                sw.WriteLine("\t\t }");

                sw.WriteLine("\t}");
                sw.WriteLine("}");
            }
        }
    }
    #region Tools
    public class SelfDefineScriptsTemplate
    {
        //直接挂在游戏物体上，进行逻辑代码编写以 ".cs"结尾 使用partial关键字 使一个脚本分为俩个脚本 使用,分别承担不同的作用，用以避免逻辑代码被覆盖
        public static void CreateScripts(string spawnScriptsPath, string className)
        {
            //创建脚本文件，并写入代码内容
            using (StreamWriter sw = File.CreateText(spawnScriptsPath))
            {
                sw.WriteLine("/********************************************************************");
                sw.WriteLine("created:  {0}/{1}/{2}  {3}:{4}",System.DateTime.Now.Date.Year,System.DateTime.Now.Date.Month, System.DateTime.Now.Date.Day ,System.DateTime.Now.Hour, System.DateTime.Now.Minute);
                sw.WriteLine("filename: {0}.cs",className);
                sw.WriteLine("author:	  Mashiro Shiina");
                sw.WriteLine("e-mail address:1967407707@qq.com");
                sw.WriteLine("filefullpath:{0}", spawnScriptsPath);
                sw.WriteLine("purpose:    ");
                sw.WriteLine("********************************************************************/");
                sw.WriteLine("using UnityEngine;");
                sw.WriteLine("using UnityEngine.UI;");
                sw.WriteLine();
                if (!string.IsNullOrWhiteSpace(NamespaceSettings.Namespace))
                {
                    sw.WriteLine("namespace {0}", NamespaceSettings.Namespace);
                    sw.WriteLine("{");
                    sw.WriteLine("\tpublic class {0} : MonoBehaviour", className);//或者 $"public class {className}: MonoBehaviour"
                    sw.WriteLine("\t{");
                    sw.WriteLine("\t\t private void Start()");
                    sw.WriteLine("\t\t {");
                    sw.WriteLine();
                    sw.WriteLine("\t\t }");
                    sw.WriteLine("\t}");
                    sw.WriteLine("}");
                }
                else
                {
                    sw.WriteLine("public class {0} : MonoBehaviour", className);//或者 $"public class {className}: MonoBehaviour"
                    sw.WriteLine("{");
                    sw.WriteLine("\t private void Start()");
                    sw.WriteLine("\t {");
                    sw.WriteLine();
                    sw.WriteLine("\t }");
                    sw.WriteLine("}");
                }   
            }
        }
    }
    #endregion
}
