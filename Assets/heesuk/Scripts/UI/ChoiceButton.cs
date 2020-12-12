using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public delegate void ChoiceButtonCallabck(ChoiceButton _button);
public class ChoiceButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI buttonText = null;

    private ChoiceButtonCallabck callback = null;
    public Choice choice = null;

    public Button button = null;
    public CanvasGroup canvasGroup = null;
    public void Init(Choice _choice, ChoiceButtonCallabck _callback)
    {
        this.transform.localScale = Vector3.zero;
        this.transform.DOScaleY(1f, 0.2f);
        this.transform.DOScaleX(1f, 0.2f).SetDelay(0.05f).SetEase(Ease.OutBack);

        this.choice = _choice;
        this.buttonText.text = this.choice.text;
        this.callback = _callback;
    }

    public void OnButton()
    {
        // 깜박깜박 좋을듯
        // 효과 부여 
        this.button.interactable = false;
        this.StartCoroutine(this.Blink());


        // 전체 제거 콜백
        this.callback(this);
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            this.canvasGroup.alpha = 0.5f;
            yield return new WaitForSeconds(0.25f);
            this.canvasGroup.alpha = 1f;
            yield return new WaitForSeconds(0.25f);
        }
    }
    
    public void Finish()
    {
        this.StopAllCoroutines();
        canvasGroup.alpha = 1f;

        this.transform.DOScaleX(0f, 0.2f);
        this.transform.DOScaleY(0f, 0.2f).SetDelay(0.05f).SetEase(Ease.OutBack).OnComplete(() => {
            Destroy(this.gameObject);
        });
    }
}
