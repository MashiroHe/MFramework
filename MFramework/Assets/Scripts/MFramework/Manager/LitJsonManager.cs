/********************************************************************
created:  2022/6/30  22:58
filename: LitJsonManager.cs
author:	  Mashiro Shiina
e-mail address:1967407707@qq.com
filefullpath:F:/Mashiro(C)/PracticePrograms/MFramework/MFramework/Assets/Scripts/MFramework/Manager/LitJsonManager.cs
purpose:    
********************************************************************/
using LitJson;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace MFramework
{
    public class LitJsonManager : NormalSingleton<LitJsonManager>
    {
        private readonly string directoryPath = Application.streamingAssetsPath + "/Json";
        /// <summary>
        /// ��ȡJson�ļ�����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public T LoadFromJson<T>(string fileName) where T : new()
        {
            T dataClass = new T();
            string filePath = "";
            filePath = directoryPath + "/" + fileName + ".json";
            //��������
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                string jsonStr = sr.ReadToEnd();
                sr.Close();
                dataClass = JsonMapper.ToObject<T>(jsonStr);
                return dataClass;
            }
            else
            {
                Debug.Log($"{fileName} ��ȡʧ��! �����ڴ�Json�ļ�");
            }
            return default(T);
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="t"></param>
        public void SaveToJson<T>(string fileName, T t)
        {
            T dataClass = t;
            string filePath = directoryPath + "/" + fileName + ".json";
            string saveJsonStr = JsonMapper.ToJson(dataClass);
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(saveJsonStr);
            sw.Close();
        }
    }
}