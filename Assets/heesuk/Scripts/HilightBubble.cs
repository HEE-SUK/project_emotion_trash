using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class HilightBubble : MonoBehaviour
{
    [SerializeField]
    private Image hilightImage = null;

    private TextBubble textBubble = null;
    private List<string> dialogs = new List<string>();
    private bool isShown = false;

    
    private void Awake()
    {
        this.hilightImage.transform.localScale = Vector3.zero;
        this.isShown = false;
    }
    public void Init(TextBubble _textBubble, List<string> _dialogs)
    {
        this.textBubble = _textBubble;
        this.dialogs = _dialogs;
    }

    public void On(List<Choice> _choices)
    {
        if(this.isShown) { return; }

        this.isShown = true;
        this.hilightImage.DOKill();
        this.hilightImage.transform.DOScaleX(1f, 0.1f).SetEase(Ease.OutBack);
        this.hilightImage.transform.DOScaleY(1f, 0.1f).SetDelay(0.05f).SetEase(Ease.OutBack);
        
        this.StartCoroutine(this.Stay(_choices));
    }

    private IEnumerator Stay(List<Choice> _choices)
    {
        bool isKeyDown = false;
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if(Input.GetKeyDown(KeyCode.F) && !isKeyDown)
            {
                AudioManager.PlaySfx(SFX.PRESS_F_KEY);
                isKeyDown = true;
                this.textBubble.On(this.dialogs, _choices);
                break;
            }
        }
        
        this.StopAllCoroutines();
        this.hilightImage.DOKill();
        this.hilightImage.transform.DOScaleX(0f, 0.1f).SetEase(Ease.OutBack);
        this.hilightImage.transform.DOScaleY(0f, 0.1f).SetDelay(0.05f).SetEase(Ease.OutBack);
    }

    public void Off()
    {
        if(!this.isShown) { return; }
        this.isShown = false;

        this.StopAllCoroutines();
        this.hilightImage.DOKill();
        this.hilightImage.transform.DOScaleX(0f, 0.1f).SetEase(Ease.OutBack);
        this.hilightImage.transform.DOScaleY(0f, 0.1f).SetDelay(0.05f).SetEase(Ease.OutBack);
    }

}
