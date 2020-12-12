using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ChoiceButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI buttonText = null;
    [SerializeField]
    private Image emotionImage = null;
    [SerializeField]
    private Sprite[] emotionsprites = {};

    CallbackEvent callback = null;
    public void Init(Choice _choice)
    {
        this.transform.localScale = Vector3.zero;
        this.transform.DOScaleY(1f, 0.15f);
        this.transform.DOScaleX(1f, 0.15f).SetDelay(0.1f).SetEase(Ease.OutBack);

        this.buttonText.text = _choice.text;
        this.emotionImage.sprite = this.emotionsprites[(int)_choice.emotion];
        // this.emotionImage.SetNativeSize();
        this.callback = _choice.callback;
    }

    public void OnButton()
    {
        this.callback();
    }
}
