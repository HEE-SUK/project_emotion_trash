using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isTalk = false;
    
    public GameManager()
    {
        // 초기화
        this.isTalk = false;
    }
}
