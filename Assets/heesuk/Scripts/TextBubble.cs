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


    private void Awake() 
    {
        this.ReadyImage.gameObject.SetActive(false);
        this.bubbleImage.transform.localScale = Vector3.zero;
    }
    
    public void On(List<string> _dialogs, List<Choice> _choices)
    {
        GameManager.Instance.isTalk = true;
        this.ReadyImage.gameObject.SetActive(false);
        
        this.dialogs = _dialogs;
        this.StartCoroutine(this.Dialog(this.dialogs, _choices));
    }

    public void Answer(List<string> _dialogs, Buff _buff)
    {
        GameManager.Instance.isTalk = true;
        this.ReadyImage.gameObject.SetActive(false);
        
        this.StartCoroutine(this.AnswerDialog(_dialogs, _buff));
    }
    
    private IEnumerator AnswerDialog(List<string> _dialogs, Buff _buff)
    {
        this.bubbleImage.transform.localScale = Vector3.zero;
        this.bubbleImage.DOKill();
        this.bubbleImage.transform.DOScaleX(1f, 0.15f).SetEase(Ease.OutBack);
        this.bubbleImage.transform.DOScaleY(1f, 0.15f).SetDelay(0.05f).SetEase(Ease.OutBack);

        for (int i = 0; i < _dialogs.Count; i++)
        {
            // 대화문 리스트만큼 반복
            yield return this.Typing(_dialogs[i]);
            yield return new WaitForFixedUpdate();
        }

        EventManager.emit(EVENT_TYPE.PLAYER_BUFF, this, _buff);

        this.bubbleImage.DOKill();
        this.bubbleImage.transform.DOScaleX(0f, 0.15f).SetEase(Ease.OutBack);
        this.bubbleImage.transform.DOScaleY(0f, 0.15f).SetDelay(0.05f).SetEase(Ease.OutBack);
        GameManager.Instance.isTalk = false;
    }

    private IEnumerator Dialog(List<string> _dialogs, List<Choice> _choices)
    {
        this.bubbleImage.transform.localScale = Vector3.zero;
        this.bubbleImage.DOKill();
        this.bubbleImage.transform.DOScaleX(1f, 0.15f).SetEase(Ease.OutBack);
        this.bubbleImage.transform.DOScaleY(1f, 0.15f).SetDelay(0.05f).SetEase(Ease.OutBack);

        for (int i = 0; i < _dialogs.Count; i++)
        {
            // 대화문 리스트만큼 반복
            yield return this.Typing(_dialogs[i]);
            yield return new WaitForFixedUpdate();
        }

        EventManager.emit(EVENT_TYPE.START_CHOICE, this, _choices);
    }

    private IEnumerator Typing(string _text)
    {
        this.ReadyImage.gameObject.SetActive(false);
        this.ReadyImage.transform.localScale = Vector3.zero;
        this.dialogText.text = string.Empty;

        for(int i = 0; i < _text.Length; i++ )
        {
            AudioManager.PlaySfx(SFX.TYPING);
            this.dialogText.text = _text.Substring(0,i);
            yield return new WaitForSeconds(0.05f);
        }
        this.dialogText.text = _text;

        this.ReadyImage.gameObject.SetActive(true);
        this.ReadyImage.DOKill();
        this.ReadyImage.transform.DOScaleX(1f, 0.15f).SetEase(Ease.OutBack);
        this.ReadyImage.transform.DOScaleY(1f, 0.15f).SetDelay(0.05f).SetEase(Ease.OutBack);
        
        // 대기
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if(Input.GetKeyDown(KeyCode.F))
            {
                AudioManager.PlaySfx(SFX.PRESS_F_KEY);
                break;
            }
        }

        this.ReadyImage.DOKill();
        this.ReadyImage.transform.DOScaleX(0f, 0.15f).SetEase(Ease.OutBack);
        this.ReadyImage.transform.DOScaleY(0f, 0.15f).SetDelay(0.05f).SetEase(Ease.OutBack);
        this.ReadyImage.gameObject.SetActive(false);
    }

    public void Off()
    {
        this.ReadyImage.gameObject.SetActive(false);
        this.StopAllCoroutines();
        this.bubbleImage.DOKill();
        this.bubbleImage.transform.DOScaleX(0f, 0.15f).SetEase(Ease.OutBack);
        this.bubbleImage.transform.DOScaleY(0f, 0.15f).SetDelay(0.05f).SetEase(Ease.OutBack);
    }
}
