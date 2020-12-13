using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class UICanvas : MonoBehaviour
{
    [SerializeField]
    private ChoicePanel choicePanel = null;

    [SerializeField]
    private GameObject gameOverPrefab = null;
    public void Start()
    {
        GameManager.Instance.endingPoint = 0;
        AudioManager.SetVolumeBgm(0.3f);
        AudioManager.PlayBgm(BGM.MAIN);
        EventManager.on(EVENT_TYPE.START_CHOICE, this.ShowChoicePanel);
        EventManager.on(EVENT_TYPE.PLAYER_DEAD, this.ShowGameOverPanel);
        EventManager.on(EVENT_TYPE.GO_ENDING, this.GoEnding);
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

    public void GoEnding(EVENT_TYPE EventType, Component Sender, object Param = null)
    {   
        Debug.Log("asd");
        if(GameManager.Instance.endingPoint >=2)
        {
            SceneManager.LoadScene("GoodEndingScene");

        }
        else
        {
            SceneManager.LoadScene("BadEndingScene");
        }
        // 굿엔딩
    }

    private void OnDestroy() {
        
        EventManager.off(EVENT_TYPE.START_CHOICE, this.ShowChoicePanel);
        EventManager.off(EVENT_TYPE.PLAYER_DEAD, this.ShowGameOverPanel);
        EventManager.off(EVENT_TYPE.GO_ENDING, this.GoEnding);
    }
}
