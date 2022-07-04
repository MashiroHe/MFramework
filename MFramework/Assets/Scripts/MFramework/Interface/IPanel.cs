using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPanel 
{
    void OnInit(IPanelData panelData= null);
    void OnAwake();
    // Start is called before the first frame update
    void OnStart();

    // Update is called once per frame
    void OnUpdate();
    void OnLateUpdate();
}
