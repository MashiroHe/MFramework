/********************************************************************
created:  2022/7/1  0:19
filename: NewtonsoftJsonManager.cs
author:	  Mashiro Shiina
e-mail address:1967407707@qq.com
filefullpath:F:/Mashiro(C)/PracticePrograms/MFramework/MFramework/Assets/Scripts/MFramework/Manager/NewtonsoftJsonManager.cs
purpose:    
********************************************************************/
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;

namespace MFramework
{
    public class NewtonsoftJsonManager : NormalSingleton<NewtonsoftJsonManager>
    {
        private readonly string directoryPath = Application.streamingAssetsPath + "/Json";
        /// <summary>
        /// 读取Json文件数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public T LoadFromJson<T>(string fileName) where T : new()
        {
            T dataClass = new T();
            string filePath = "";
            filePath = directoryPath + "/" + fileName + ".json";
            //正常解析
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                string jsonStr = sr.ReadToEnd();
                sr.Close();
                dataClass = JsonConvert.DeserializeObject<T>(jsonStr); 
                return dataClass;
            }
            else
            {
                Debug.Log($"{fileName} 读取失败! 不存在此Json文件");
            }
            return default(T);
        }
        /// <summary>
        /// 存贮
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="t"></param>
        public void SaveToJson<T>(string fileName, T t)
        {
            T dataClass = t;
            string filePath = directoryPath + "/" + fileName + ".json";
            if (File.Exists(filePath))
            {
                File.Delete(filePath); 
            }
            string saveJsonStr = JsonConvert.SerializeObject(dataClass);
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(saveJsonStr);
            sw.Close();
        }
    }
}
