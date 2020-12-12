using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IntroGameCanvas : MonoBehaviour
{
    [SerializeField]
    private ChoicePanel choicePanel = null;

    void Start()
    {
        AudioManager.SetVolumeBgm(0.3f);
        AudioManager.PlayBgm(BGM.MAIN);
        EventManager.on(EVENT_TYPE.GO_MAIN, this.GoMainScene);
        EventManager.on(EVENT_TYPE.START_CHOICE, this.ShowChoicePanel);
    }

    public void ShowChoicePanel(EVENT_TYPE EventType, Component Sender, object Param = null)
    {
        List<Choice> choices = (List<Choice>)Param;

        this.choicePanel.Init(choices);
    }
    public void GoMainScene(EVENT_TYPE EventType, Component Sender, object Param = null)
    {
        GameManager.Instance.nextSceneName = "Intro";
        EventManager.emit(EVENT_TYPE.CLOSE_UP, this);
    }
    private void OnDestroy() {
        
        EventManager.off(EVENT_TYPE.START_CHOICE, this.ShowChoicePanel);
        EventManager.off(EVENT_TYPE.GO_MAIN, this.GoMainScene);
    }
}
