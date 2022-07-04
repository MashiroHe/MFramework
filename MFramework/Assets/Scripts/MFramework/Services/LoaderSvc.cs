using MFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
namespace MFramework
{
    public class LoaderManager : MonoSingleton<LoaderManager>
    {
        #region//异步加载场景函数
        public void AsynLoadScene(string loadName, Action loadedMethod)
        {
            StartCoroutine(StartLoading(loadName, loadedMethod));
        }
        //开启异步加载协程
        private IEnumerator StartLoading(string loadName, Action loadedMethod)
        {
            //激活加载游戏UI界面
            //  GameRoot.Instance.loadingWnd.SetWndState();
            float displayProgress = 0f;
            float toProgress = 0f;
            AsyncOperation AO = SceneManager.LoadSceneAsync(loadName, LoadSceneMode.Single);
            AO.allowSceneActivation = false;
            while (AO.progress < 0.7f)
            {
                toProgress = AO.progress;
                while (displayProgress < toProgress)
                {
                    displayProgress += 0.001f;
                    //   GameRoot.Instance.loadingWnd.SetProgress(displayProgress);
                    yield return new WaitForEndOfFrame();
                }
            }
            toProgress = 0.98f;
            while (displayProgress < toProgress)
            {
                displayProgress += Time.deltaTime;
                //  GameRoot.Instance.loadingWnd.SetProgress(displayProgress);
                yield return new WaitForEndOfFrame();
            }
            if (displayProgress >= 0.98f)
            {
                //  GameRoot.Instance.loadingWnd.SetWndState(false);
                AO.allowSceneActivation = true;
                if (loadedMethod != null)
                {
                    //执行加载后的需要执行的函数方法
                    loadedMethod();
                }
            }
        }
        #endregion
        //加载音效资源函数
        private Dictionary<string, AudioClip> loadedAudioDic = new Dictionary<string, AudioClip>();
        public AudioClip LoadAudioSource(string path, bool isCache = false)
        {
            AudioClip audioClip = null;
            if (!loadedAudioDic.TryGetValue(path, out audioClip))
            {
                //数据字典中没有得到已经加载的音效资源是，进行加载
                audioClip = Resources.Load<AudioClip>(path);
                if (isCache)//如果需要缓存则存储到数据字典
                {
                    loadedAudioDic.Add(path, audioClip);
                }
            }
            return audioClip;
        }

        #region 加载角色预制体资源
        private Dictionary<string, GameObject> prefabDic = new Dictionary<string, GameObject>();
        public GameObject LoadPrefabAsset(string path, bool isCache = false)
        {
            GameObject prefab = null;
            GameObject go = null;
            if (!prefabDic.TryGetValue(path, out prefab))
            {
                GameObject tempPrefab = Resources.Load<GameObject>(path);
                if (isCache)
                {
                    prefabDic.Add(path, tempPrefab);
                }
                go = GameObject.Instantiate(tempPrefab);
                return go;
            }
            else
            {
                go = GameObject.Instantiate(prefab);
                return go;
            }
        }
        #region 加载角色特效预制体资源
        private Dictionary<string, GameObject> playerSkillPrefabDic = new Dictionary<string, GameObject>();
        public GameObject LoadPlayerSkillPrefabAsset(string path, bool isCache = false)
        {
            GameObject go = null;
            if (!playerSkillPrefabDic.TryGetValue(path, out GameObject prefab))
            {
                GameObject tempPrefab = Resources.Load<GameObject>(path);
                if (isCache)
                {
                    playerSkillPrefabDic.Add(path, tempPrefab);
                }
                go = GameObject.Instantiate(tempPrefab);
                return go;
            }
            else
            {
                go = GameObject.Instantiate(prefab);
                return go;
            }
        }
        #endregion
        #endregion
        #region 图片渲染资源加载
        private Dictionary<string, RenderTexture> renderTextureDic = new Dictionary<string, RenderTexture>();
        public RenderTexture LoadRenderTextureByPath(string path, bool isCache = false)
        {
            RenderTexture renderTexture;
            if (!renderTextureDic.TryGetValue(path, out renderTexture))
            {
                renderTexture = Resources.Load<RenderTexture>(path);
                if (isCache)
                {
                    renderTextureDic.Add(path, renderTexture);
                }
                return renderTexture;
            }
            else
            {
                return renderTexture;
            }
        }
        #endregion
        #region 加载图片资源 
        private Dictionary<string, Sprite> spritesDic = new Dictionary<string, Sprite>();
        public Sprite LoadSpriteAssetsByPath(string path, bool isCache = false)
        {
            Sprite tempSprite = null;
            if (!spritesDic.TryGetValue(path, out tempSprite))
            {
                tempSprite = Resources.Load<Sprite>(path);
                if (isCache)
                {
                    spritesDic.Add(path, tempSprite);
                }
            }
            return tempSprite;
        }
        #endregion

        public void LoadSceneSycn(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        public GameObject LoadPrefab(string path)
        {
            var prefab = Resources.Load<GameObject>(path);

            return prefab;
        }

        public GameObject LoadPrefabAndInstantiate(string path, Transform parent = null)
        {
            var prefab = LoadPrefab(path);
            var temp = Object.Instantiate(prefab, parent);
            return temp;
        }

        public T Load<T>(string path) where T : Object
        {
            var sprite = Resources.Load<T>(path);
            if (sprite == null)
            {
                Debug.LogError("未找到对应图片，路径：" + path);
                return null;
            }

            return sprite;
        }

        public T[] LoadAll<T>(string path) where T : Object
        {
            var sprites = Resources.LoadAll<T>(path);
            if (sprites == null || sprites.Length == 0)
            {
                Debug.LogError("当前路径下未找到对应资源，路径：" + path);
                return null;
            }

            return sprites;
        }

    }
}