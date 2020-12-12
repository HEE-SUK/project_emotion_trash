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

    private bool isFinished = false;

}
