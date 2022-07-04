/*** 
Author: Mashiro Shiina
***/
using UnityEngine;
namespace MFramework
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject(typeof(T).Name);
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<T>();
                }

                return _instance;
            }
        }

    //    private static T _instance;
    //public static T Instance
    //{
    //    get { return _instance; }
    //}
    //protected virtual void Awake()
    //{
    //    _instance = this as T;
    //}
    }
}
