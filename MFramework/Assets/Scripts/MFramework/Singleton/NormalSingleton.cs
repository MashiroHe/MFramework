/*** 
Author: Mashiro Shiina
***/
using UnityEngine;

namespace MFramework
{
    public class NormalSingleton<T> where T : class, new()
    {
        protected static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var t = new T();
                    if (t is MonoBehaviour)
                    {
                        Debug.LogError("Mono类请使用MonoSingleton");
                        return null;
                    }

                    _instance = t;
                }

                return _instance;
            }
        }
    }
}