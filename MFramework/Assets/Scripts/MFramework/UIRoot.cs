using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MFramework;
public class UIRoot :MonoSingleton<UIRoot>
{
    protected Dictionary<int, PanelBase> panelsDict = null;

    private  DeductionUIPanel m_DeductionUIPanel;
    private TargetSelectPanel m_TargetSelectPanel;
    public void Init()
    {
        if (panelsDict == null)
        {
            panelsDict = new Dictionary<int, PanelBase>();
        }

        m_DeductionUIPanel =FindObjectOfType<DeductionUIPanel>();
        RegisterPanel(PanelType.DeductionPanel, m_DeductionUIPanel);
        m_DeductionUIPanel.OnInit();

        m_TargetSelectPanel = FindObjectOfType<TargetSelectPanel>();
        RegisterPanel(PanelType.TargetSelectPanel,m_TargetSelectPanel);
        m_TargetSelectPanel.OnInit();
        Debug.Log("2");
    }
    private void RegisterPanel(PanelType panelType,PanelBase panelScript)
    {
        if (!panelsDict.ContainsKey((int)panelType))
        {
            int keyVal = (int)panelType;
            panelsDict.Add(keyVal,panelScript);
        }
    }
    private void UnRegisterPanel(PanelType panelType, PanelBase panelScript)
    {
        if (panelsDict.ContainsKey((int)panelType))
        {
            int keyVal = (int)panelType;
            panelsDict.Remove(keyVal);
        }
    }
    private void UnRegistreAll()
    {
        panelsDict.Clear();
    }
    public void OpenPanel(GameObject go)
    {
       go.SetActive(true);
    }
    public void OpenPanel(PanelType panelType, params object[] objs)
    {
        PanelState(panelType, true, objs);
    }
    public void ClosePanel(GameObject go)
    {
        go.SetActive(false);
    }
    public void ClosePanel(PanelType panelType, params object[] objs)
    {
        PanelState(panelType, false, objs);
    }
    public void CloseAll()
    {
        foreach (var panel in panelsDict.Values)
        {
            panel.gameObject.SetActive(false);
        }
    }
    private void PanelState(PanelType panelType, bool isActive = true, params object[] objs)
    {
        if (panelsDict.TryGetValue((int)panelType, out PanelBase panelScript))
        {
            panelScript.gameObject.SetActive(isActive);
        }
    }
    private void OnDestroy()
    {
        m_DeductionUIPanel = null;
    }
}
