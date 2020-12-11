using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TextBubble : MonoBehaviour
{
    [SerializeField]
    private Image bubbleImage = null;
    [SerializeField]
    private Image ReadyImage = null;
    [SerializeField]
    private TextMeshProUGUI dialogText = null;

    private List<string> dialogs = new List<string>();

    private bool isReadyNext = false;

    private void Awake() 
    {
        this.ReadyImage.gameObject.SetActive(false);
        this.bubbleImage.transform.localScale = Vector3.zero;
    }
    
    public void On(List<string> _dialogs)
    {
        this.ReadyImage.gameObject.SetActive(false);
        
        this.dialogs = _dialogs;
        this.StartCoroutine(this.Dialog(this.dialogs));
    }

    // private void Update() {
        
    //     if(Input.GetKeyDown(KeyCode.F))
    //     {
    //         this.OnButtonSkip();
    //     }
    // }

    // public void OnButtonSkip()
    // {
    //     if(!this.isReadyNext) { return; }
    //     // 다음 글
    // }
    
    private IEnumerator Dialog(List<string> _dialogs)
    {
        this.bubbleImage.transform.localScale = Vector3.one;

        foreach (var dialog in _dialogs)
        {
            // 대화문 리스트만큼 반복
            this.isReadyNext = false;
            yield return this.Typing(dialog);
            yield return new WaitForFixedUpdate();
        }
        EventManager.emit(EVENT_TYPE.START_CHOICE, this);
    }
    private IEnumerator Typing(string _text)
    {
        this.ReadyImage.gameObject.SetActive(false);
        this.dialogText.text = string.Empty;

        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < _text.Length; i++ )
        {
            this.dialogText.text = _text.Substring(0,i);
            yield return new WaitForSeconds(0.05f);
        }
        this.dialogText.text = _text;
        this.isReadyNext = true;
        this.ReadyImage.DOKill();
        this.ReadyImage.transform.DOScaleX(1f, 0.1f).SetEase(Ease.OutBack);
        this.ReadyImage.transform.DOScaleY(1f, 0.1f).SetDelay(0.05f).SetEase(Ease.OutBack);
        
        // 대기
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if(Input.GetKeyDown(KeyCode.F))
            {
                break;
            }
        }
        this.ReadyImage.DOKill();
        this.ReadyImage.transform.DOScaleX(0f, 0.1f).SetEase(Ease.OutBack);
        this.ReadyImage.transform.DOScaleY(0f, 0.1f).SetDelay(0.05f).SetEase(Ease.OutBack);
    }

    public void Off()
    {
        this.ReadyImage.gameObject.SetActive(false);
        this.StopAllCoroutines();
        this.bubbleImage.DOKill();
        this.bubbleImage.transform.DOScaleX(0f, 0.1f).SetEase(Ease.OutBack);
        this.bubbleImage.transform.DOScaleY(0f, 0.1f).SetDelay(0.05f).SetEase(Ease.OutBack);
    }
}
