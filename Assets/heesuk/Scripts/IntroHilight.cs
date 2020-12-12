using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IntroHilight : MonoBehaviour
{
    // textbubble은 Prefab으로 생성되게 하자
    [SerializeField]
    private Transform Player = null;
    [SerializeField]
    private HilightBubble hilightBubble = null;

    [SerializeField]
    private DialogInfo[] info = {};

    public int dialogCount = 0;
    

    [SerializeField]
    private TextBubble textBubble = null;

    private bool isFinished = false;
    
    private void Start()
    {
        this.isFinished = false;
        this.hilightBubble.Init(this.textBubble, this.info[this.dialogCount].dialogs);

        for (int i = 0; i < this.info[this.dialogCount].emotionTypes.Length; i++)
        {
            EMOTION emotion = this.info[this.dialogCount].emotionTypes[i];
            string emotionText = this.info[this.dialogCount].emotionText[i];

            List<string> answers = new List<string>();
            switch (i)
            {
                case 0:
                    foreach (var item in this.info[this.dialogCount].answerText0)
                    {
                        answers.Add(item);
                    }
                    break;
                case 1:
                    foreach (var item in this.info[this.dialogCount].answerText1)
                    {
                        answers.Add(item);
                    }
                    break;
                case 2:
                    foreach (var item in this.info[this.dialogCount].answerText2)
                    {
                        answers.Add(item);
                    }
                    break;
                case 3:
                    foreach (var item in this.info[this.dialogCount].answerText3)
                    {
                        answers.Add(item);
                    }
                    break;
                case 4:
                    foreach (var item in this.info[this.dialogCount].answerText4)
                    {
                        answers.Add(item);
                    }
                    break;
            }

            Vector3 position = this.transform.position;
            this.info[this.dialogCount].choices.Add(new Choice(emotion, emotionText, () => {

                this.textBubble.IntroAnswer(answers);
                this.Finish();
            }, position));
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
            this.hilightBubble.On(this.info[this.dialogCount].choices);
        }
        else
        {
            this.hilightBubble.Off();
        }
    }
}
