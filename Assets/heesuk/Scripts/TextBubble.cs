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

        foreach (var dialog in this.dialogs)
        {
            // 대화문 리스트만큼 반복
            this.StartCoroutine(this.Typing(dialog));
        }
        // TODO: 전부 읽으면 종료 효과
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
