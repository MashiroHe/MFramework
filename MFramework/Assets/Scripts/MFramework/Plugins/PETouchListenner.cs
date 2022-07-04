/********************************************************************
	filename: 	PETouchListenner.cs
	created:	2021/09/15  18:28
	file path:	d:\taikr_unityproject\3dgame\darkgod
	file base:	PETouchListenner
	file ext:	cs
	author:		Mashiro
	email:  196740770@qq.com or zhidanhe92@gmail.com
	purpose:	自封装 触摸 点击事件监听
*********************************************************************/
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class PETouchListenner : MonoBehaviour,IPointerClickHandler, IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    public Action<PointerEventData> OnTouchDrag=null;
    public Action<PointerEventData> OnTouchDown = null;
    public Action<PointerEventData> OnTouchUp = null;
    public Action<PointerEventData, object> OnClick = null;
    public object args=null;
    public void OnDrag(PointerEventData eventData)
    {
        if (OnTouchDrag != null)
        {
            OnTouchDrag(eventData);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClick != null)
        {
            OnClick(eventData, args);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnTouchDown != null)
        {
            OnTouchDown(eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnTouchUp != null)
        {
            OnTouchUp(eventData);
        }
    }
}
