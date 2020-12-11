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

    CallbackEvent callback = null;
    public void Init(string _text, CallbackEvent _callback)
    {
        this.transform.localScale = Vector3.zero;
        this.transform.DOScaleX(1f, 0.3f);
        this.transform.DOScaleY(1f, 0.3f).SetDelay(0.1f);

        this.buttonText.text = _text;
        this.callback = _callback;
    }

    public void OnButton()
    {
        this.callback();
    }
}
