/********************************************************************
	filename: 	TimerSvc.cs
	created:	2021/09/27 12:17
	file path:	D:\taikr_unityproject\3DGame\DarkGod
	file base:	 TimerSvc
	file ext:	cs
	author:		Mashiro
	email:      196740770@qq.com or zhidanhe92@gmail.com
	purpose:	计时服务 
*********************************************************************/
namespace MFramework
{
    using System;
    using UnityEngine;
    public class TimerSvc : MonoSingleton<TimerSvc>
    {
        private PETimer pt;
        public void Init()
        {
            pt = new PETimer();
            pt.SetLog((string info) =>
            {
                // Debug.Log(info);
            });
        }
        private void Update()
        {
            if (pt != null)
            {
                pt.Update();
            }
        }
        public int AddTaskTimer(Action<int> callback, double delayTime, PETimeUnit pETimeUnit = PETimeUnit.Millisecond, int loopCount = 1)
        {
            return pt.AddTimeTask(callback, delayTime, pETimeUnit, loopCount);
        }
        public double GetCurrentTime()
        {
            return pt.GetMillisecondsTime();
        }
        public void DeleteTimerTask(int tId)
        {
            pt.DeleteTimeTask(tId);
        }
    }

}
