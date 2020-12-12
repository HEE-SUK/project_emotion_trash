using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHilight : MonoBehaviour
{
    [SerializeField]
    private Transform Player = null;
    [SerializeField]
    private HilightBubble hilightBubble = null;


    // textbubble은 Prefab으로 생성되게하자
    [SerializeField]
    private TextBubble textBubble = null;

    [SerializeField]
    private List<string> dialogs = new List<string>();

    // 지문의 갯수가 됩니다.
    [SerializeField]
    private EMOTION[] emotionTypes = {EMOTION.A, EMOTION.A, EMOTION.A, EMOTION.A, EMOTION.A};
    [SerializeField]
    private string[] emotionText = {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty};
    [SerializeField]
    private STAT[] statTypes = {STAT.MOVE_SPEED, STAT.MOVE_SPEED, STAT.MOVE_SPEED, STAT.MOVE_SPEED, STAT.MOVE_SPEED};
    [SerializeField]
    private int[] statValues = {0, 0, 0, 0, 0};

    private List<Choice> choices = new List<Choice>();
    [SerializeField]
    private string[] answerText = {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty};
    private bool isFinished = false;
    void Start()
    {
        this.isFinished = false;
        this.hilightBubble.Init(this.textBubble, this.dialogs);

        for (int i = 0; i < emotionTypes.Length; i++)
        {
            EMOTION emotion = this.emotionTypes[i];
            string emotionText = this.emotionText[i];
            string answer = this.answerText[i];
            STAT stat = this.statTypes[i];
            int value = this.statValues[i];
            this.choices.Add(new Choice(emotion, emotionText, () => {

                this.textBubble.Answer(answer, new Buff(stat, value));
                this.Finish();
            }));
        }
    }

    private void Finish()
    {
        this.isFinished = true;
    }

    void Update()
    {
        // 대화중 예외처리
        if(GameManager.Instance.isTalk || this.isFinished) { return; }

        float distance = (this.Player.transform.localPosition - this.transform.localPosition).magnitude;
        if(distance < 2f)
        {
            this.hilightBubble.On(this.choices);
        }
        else
        {
            this.hilightBubble.Off();
        }
    }
}
