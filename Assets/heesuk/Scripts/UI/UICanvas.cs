using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UICanvas : MonoBehaviour
{
    [SerializeField]
    private ChoicePanel choicePanel = null;

    [SerializeField]
    private TextBubble textBubble = null;
    public void Start()
    {
        List<string> texts = new List<string>();
        texts.Add("1번 선택");
        texts.Add("2번 선택");
        texts.Add("3번 선택");
        texts.Add("4번 선택");
        texts.Add("5번 선택");
        // this.choicePanel.Init(texts);

        this.TestTextBubble();
        EventManager.on(EVENT_TYPE.START_CHOICE, this.ShowChoicePanel);
    }

    public void ShowChoicePanel(EVENT_TYPE EventType, Component Sender, object Param = null)
    {

    }
    
    public void TestTextBubble()
    {
        List<string> texts = new List<string>();
        texts.Add("what is this");
        texts.Add("this is TextMesh Pro");
        texts.Add("oh my god");
        this.textBubble.Init(texts);
    }

    private void OnDestroy() {
        
        EventManager.off(EVENT_TYPE.START_CHOICE, this.ShowChoicePanel);
    }
}
