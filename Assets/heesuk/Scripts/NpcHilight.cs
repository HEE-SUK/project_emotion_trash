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
    private float[] statValues = {0, 0, 0, 0, 0};

    private List<Choice> choices = new List<Choice>();
    [SerializeField]
    private string[] answerText0 = {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty};
    [SerializeField]
    private string[] answerText1 = {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty};
    [SerializeField]
    private string[] answerText2 = {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty};
    [SerializeField]
    private string[] answerText3 = {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty};
    [SerializeField]
    private string[] answerText4 = {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty};

    [SerializeField]
    private string[] statTexts = {};
    private bool isFinished = false;
    void Start()
    {
        this.isFinished = false;
        this.hilightBubble.Init(this.textBubble, this.dialogs);

        for (int i = 0; i < emotionTypes.Length; i++)
        {
            EMOTION emotion = this.emotionTypes[i];
            string emotionText = this.emotionText[i];

            List<string> answers = new List<string>();
            switch (i)
            {
                case 0:
                    foreach (var item in answerText1)
                    {
                        answers.Add(item);
                    }
                    break;
                case 1:
                    foreach (var item in answerText1)
                    {
                        answers.Add(item);
                    }
                    break;
                case 2:
                    foreach (var item in answerText1)
                    {
                        answers.Add(item);
                    }
                    break;
                case 3:
                    foreach (var item in answerText1)
                    {
                        answers.Add(item);
                    }
                    break;
                case 4:
                    foreach (var item in answerText1)
                    {
                        answers.Add(item);
                    }
                    break;
            }
            STAT stat = this.statTypes[i];
            float value = this.statValues[i];
            string statText = this.statTexts[i];
            this.choices.Add(new Choice(emotion, emotionText, () => {

                this.textBubble.Answer(answers, new Buff(stat, value, statText));
                this.Finish();
            }, this.transform.position));
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
