using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCtrl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            EventManager.emit(EVENT_TYPE.GO_ENDING, this);
        }
    }
}
