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
        this.bubbleImage.DOKill();
        this.bubbleImage.transform.DOScaleX(1f, 0.1f).SetEase(Ease.OutBack);
        this.bubbleImage.transform.DOScaleY(1f, 0.1f).SetDelay(0.05f).SetEase(Ease.OutBack);
        
        this.dialogs = _dialogs;
        this.StartCoroutine(this.Dialog(this.dialogs));
    }

    private void Update() {
        
        if(Input.GetKeyDown(KeyCode.F))
        {
            this.OnButtonSkip();
        }
    }

    public void OnButtonSkip()
    {
        this.isReadyNext = true;
    }
    
    private IEnumerator Dialog(List<string> _dialogs)
    {
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
        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < _text.Length; i++ )
        {
            if(this.isReadyNext) break;
            this.dialogText.text = _text.Substring(0,i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);
        this.dialogText.text = _text;
        this.isReadyNext = true;
        this.ReadyImage.gameObject.SetActive(true);
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
