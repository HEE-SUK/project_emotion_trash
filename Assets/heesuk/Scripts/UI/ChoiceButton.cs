﻿using System.Collections;
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
    public void Init(Choice _choice, CallbackEvent _callback)
    {
        this.transform.localScale = Vector3.zero;
        this.transform.DOScaleY(1f, 0.15f);
        this.transform.DOScaleX(1f, 0.15f).SetDelay(0.05f).SetEase(Ease.OutBack);

        this.buttonText.text = _choice.text;
        this.emotionImage.sprite = this.emotionsprites[(int)_choice.emotion];
        // this.emotionImage.SetNativeSize();
        this.callback = () => {
            _choice.callback();
            _callback();
        };
    }

    public void OnButton()
    {
        this.callback();
    }

    public void Finish()
    {
        this.transform.DOScaleY(0f, 0.15f);
        this.transform.DOScaleX(0f, 0.15f).SetDelay(0.05f).SetEase(Ease.OutBack).OnComplete(() => {
            Destroy(this.gameObject);
        });
    }
}
