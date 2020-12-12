using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UICanvas : MonoBehaviour
{
    [SerializeField]
    private ChoicePanel choicePanel = null;

    [SerializeField]
    private GameObject gameOverPrefab = null;
    public void Start()
    {
        AudioManager.SetVolumeBgm(0.3f);
        AudioManager.PlayBgm(BGM.MAIN);
        EventManager.on(EVENT_TYPE.START_CHOICE, this.ShowChoicePanel);
        EventManager.on(EVENT_TYPE.PLAYER_DEAD, this.ShowGameOverPanel);
        EventManager.on(EVENT_TYPE.GO_GOOD, this.GoGood);
        EventManager.on(EVENT_TYPE.GO_BAD, this.GoBad);
    }

    public void ShowChoicePanel(EVENT_TYPE EventType, Component Sender, object Param = null)
    {
        List<Choice> choices = (List<Choice>)Param;

        this.choicePanel.Init(choices);
    }
    public void ShowGameOverPanel(EVENT_TYPE EventType, Component Sender, object Param = null)
    {
        GameOverPanel gameOverPanel = Instantiate(this.gameOverPrefab).GetComponent<GameOverPanel>();
        gameOverPanel.transform.SetParent(this.transform, false);
        gameOverPanel.Init();
    }
    public void GoGood(EVENT_TYPE EventType, Component Sender, object Param = null)
    {
        // 굿엔딩
        GameManager.Instance.nextSceneName = "Intro";
        EventManager.emit(EVENT_TYPE.CLOSE_UP, this);
    }
    public void GoBad(EVENT_TYPE EventType, Component Sender, object Param = null)
    {
        // 베드엔딩
        GameManager.Instance.nextSceneName = "Intro";
        EventManager.emit(EVENT_TYPE.CLOSE_UP, this);
    }

    private void OnDestroy() {
        
        EventManager.off(EVENT_TYPE.START_CHOICE, this.ShowChoicePanel);
        EventManager.off(EVENT_TYPE.PLAYER_DEAD, this.ShowGameOverPanel);
        EventManager.off(EVENT_TYPE.GO_GOOD, this.GoGood);
        EventManager.off(EVENT_TYPE.GO_BAD, this.GoBad);
    }
}
