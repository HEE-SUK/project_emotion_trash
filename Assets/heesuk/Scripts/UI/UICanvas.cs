using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UICanvas : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera = null;
    [SerializeField]
    private ChoicePanel choicePanel = null;

    [SerializeField]
    private GameObject textBubblePrefab = null;
    [SerializeField]
    private Transform textBubbleParent = null;
    public void Start()
    {
        EventManager.on(EVENT_TYPE.START_CHOICE, this.ShowChoicePanel);

        
        List<string> texts = new List<string>();
        texts.Add("what is this");
        texts.Add("this is TextMesh Pro");
        texts.Add("oh my god");

        this.CreateTextBubble(texts);
    }

    public void ShowChoicePanel(EVENT_TYPE EventType, Component Sender, object Param = null)
    {
        List<string> texts = new List<string>();
        texts.Add("1번 선택");
        texts.Add("2번 선택");
        texts.Add("3번 선택");
        texts.Add("4번 선택");
        texts.Add("5번 선택");

        this.choicePanel.Init(texts);
    }

    public void CreateTextBubble(List<string> _texts)
    {
        // 말풍선 생성
        TextBubble textBubble = Instantiate(this.textBubblePrefab).GetComponent<TextBubble>();
        textBubble.transform.SetParent(this.textBubbleParent, false);
        textBubble.Init(_texts);
    }

    private void OnDestroy() {
        
        EventManager.off(EVENT_TYPE.START_CHOICE, this.ShowChoicePanel);
    }
}
