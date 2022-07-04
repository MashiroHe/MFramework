using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EditorExtensions
{
    public class UIRoot : MonoBehaviour
    {
        #region Public  Field
        public Transform m_BG;
        public Transform m_Common;
        public Transform m_Forward;
        public Transform m_PopUI;

        public Canvas m_Canvas;
        #endregion

        #region Private Field
        [SerializeField]
        private EventSystem m_EventSystem;

        #endregion
    }
}
