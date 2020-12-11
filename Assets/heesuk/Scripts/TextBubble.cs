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
    public void Init(List<string> _dialogs)
    {
        this.ReadyImage.gameObject.SetActive(false);
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
        this.dialogText.text = _text;
        this.isReadyNext = true;
        this.ReadyImage.gameObject.SetActive(true);
        
    }
}
