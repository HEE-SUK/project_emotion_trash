using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HeartPanel : MonoBehaviour
{
    [SerializeField]
    private Image[] heartImages = {};

    public void Awake()
    {
        EventManager.on(EVENT_TYPE.UPDATE_HP, this.Update_UI);
    }

    public void Update_UI(EVENT_TYPE EventType, Component Sender, object Param = null)
    {
        int hp = (int)Param;
        
        for (int i = 0; i < hp; i++)
        {
            // this.heartImages.
        }
    }

    private void OnDestroy() {
        
        EventManager.off(EVENT_TYPE.UPDATE_HP, this.Update_UI);
    }
}
