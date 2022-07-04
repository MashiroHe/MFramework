/********************************************************************
    created: 2022/06/24
    filename: GameRoot.cs
    author:     Mashiro Shiina
    e-mail address:1967407707@qq.com 
    date: 8:9:2021   14:12
    purpose: 应用启动脚本
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MFramework;
public class AppRoot : MonoSingleton<AppRoot>
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
     
        Debug.Log("1");
    }
    // Start is called before the first frame update
    void Start()
    {
        //初始化UIRoot
        UIRoot.Instance.Init();
        TimerSvc.Instance.Init();
        ResSvc.Instance.InitResSvc();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
