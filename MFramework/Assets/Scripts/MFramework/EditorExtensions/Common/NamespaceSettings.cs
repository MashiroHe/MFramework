using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EditorExtensions
{
    public class NamespaceSettings
    {
        private readonly static string Namespace_Key = Application.productName + "_NameSpace";
        public static string Namespace 
        {
            get => UnityEditor.EditorPrefs.GetString(Namespace_Key, defaultValue: "DefaultNamespace");
            set => UnityEditor.EditorPrefs.SetString(Namespace_Key, value);
        } 
        public static bool IsDefaultNamespace => Namespace == "DefaultNamespace";
    }
}

