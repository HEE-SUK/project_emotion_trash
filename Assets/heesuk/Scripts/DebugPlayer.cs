using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlayer : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        // 대화중 예외처리
        if(GameManager.Instance.isTalk) { return; }
        
        if(Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(new Vector3(0f,0.1f,0f));
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            
            this.transform.Translate(new Vector3(0f,-0.1f,0f));
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            
            this.transform.Translate(new Vector3(-0.1f,0f,0f));
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            
            this.transform.Translate(new Vector3(0.1f,0f,0f));
        }
    }
}
