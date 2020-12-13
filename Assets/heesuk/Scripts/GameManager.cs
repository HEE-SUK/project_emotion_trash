using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public string nextSceneName = string.Empty;
    public bool isTalk = false;
    public int endingPoint = 0;
    public GameManager()
    {
        // 초기화
        this.isTalk = false;
    }
}
