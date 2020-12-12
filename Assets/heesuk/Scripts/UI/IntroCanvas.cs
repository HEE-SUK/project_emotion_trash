using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IntroCanvas : MonoBehaviour
{
    public void Start()
    {
        AudioManager.PlayBgm(BGM.INTRO);
    }
    public void OnStartButton()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void OnExitButton()
    {
        Application.Quit();
    }
}
