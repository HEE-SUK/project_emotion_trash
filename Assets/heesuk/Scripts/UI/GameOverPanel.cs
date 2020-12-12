using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField]
    private Button gameoverButton = null;


    public void Init()
    {

    }   
     
    public void OnButtonRetry()
    {
        SceneManager.LoadScene("Intro");
    }
}
