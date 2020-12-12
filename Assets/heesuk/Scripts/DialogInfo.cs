using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInfo : MonoBehaviour
{
    [SerializeField]
    private List<string> dialogs = new List<string>();

    // 지문의 갯수가 됩니다.
    [SerializeField]
    private EMOTION[] emotionTypes = {EMOTION.A, EMOTION.A, EMOTION.A, EMOTION.A, EMOTION.A};
    [SerializeField]
    private string[] emotionText = {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty};

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

    private bool isFinished = false;

}
