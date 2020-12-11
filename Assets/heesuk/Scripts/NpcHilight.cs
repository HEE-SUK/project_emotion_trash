using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHilight : MonoBehaviour
{
    [SerializeField]
    private Transform Player = null;
    [SerializeField]
    private HilightBubble hilightBubble = null;

    void Start()
    {
        
    }

    void Update()
    {
        float distance = (this.Player.transform.localPosition - this.transform.localPosition).magnitude;
        if(distance < 2f)
        {
            this.hilightBubble.On();
        }
        else
        {
            this.hilightBubble.Off();
        }
    }
}
