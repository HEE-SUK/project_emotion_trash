using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInfo : MonoBehaviour
{
    public List<string> dialogs = new List<string>();

    public List<Choice> choices = new List<Choice>();
    // 지문의 갯수가 됩니다.
    public EMOTION[] emotionTypes = {EMOTION.A, EMOTION.A, EMOTION.A, EMOTION.A, EMOTION.A};
    public string[] emotionText = {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty};

    public string[] answerText0 = {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty};
    public string[] answerText1 = {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty};
    public string[] answerText2 = {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty};
    public string[] answerText3 = {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty};
    public string[] answerText4 = {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty};


    public void Init(TextBubble _textBubble, CallbackEvent _nextCallback, CallbackEvent _finishCallback, bool _isFinish)
    {

        for (int i = 0; i < this.emotionTypes.Length; i++)
        {
            EMOTION emotion = this.emotionTypes[i];
            string emotionText = this.emotionText[i];

            List<string> answers = new List<string>();
            switch (i)
            {
                case 0:
                    foreach (var item in this.answerText0)
                    {
                        answers.Add(item);
                    }
                    break;
                case 1:
                    foreach (var item in this.answerText1)
                    {
                        answers.Add(item);
                    }
                    break;
                case 2:
                    foreach (var item in this.answerText2)
                    {
                        answers.Add(item);
                    }
                    break;
                case 3:
                    foreach (var item in this.answerText3)
                    {
                        answers.Add(item);
                    }
                    break;
                case 4:
                    foreach (var item in this.answerText4)
                    {
                        answers.Add(item);
                    }
                    break;
            }

            Vector3 position = this.transform.position;
            this.choices.Add(new Choice(emotion, emotionText, () => {
                // 하이라이트 누름
                _finishCallback();
                _textBubble.IntroAnswer(answers, _nextCallback, _isFinish);
            }, position));
        }
    }
}
