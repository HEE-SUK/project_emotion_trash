using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Icon : MonoBehaviour
{
    [SerializeField]
    private Image image = null;
    [SerializeField]
    private Sprite[] sprites = {};
    private Vector3 targetPosition = new Vector3();
    public CanvasGroup canvasGroup = null;

    public void Init(EMOTION _emotion, Vector3 _targetPosition)
    {
        this.image.sprite = this.sprites[(int)_emotion];
        this.targetPosition = Camera.main.WorldToScreenPoint(_targetPosition);
        this.transform.localScale = Vector3.zero;
        this.transform.DOScaleY(1f, 0.2f).SetEase(Ease.OutBack);
        this.transform.DOScaleX(1f, 0.2f).SetDelay(0.05f).SetEase(Ease.OutBack);
    }

    public void Trash(Choice _choice) 
    {   
        AudioManager.PlaySfx(SFX.SELECT_EMOTION);
        // 던지기
        this.transform.DOScaleY(0.8f, 0.2f).SetEase(Ease.OutBack);
        this.transform.DOScaleX(0.8f, 0.2f).SetDelay(0.05f).SetEase(Ease.OutBack);
        this.transform.DOBlendableRotateBy(new Vector3(0f, 0f, 60f), 0.5f).SetEase(Ease.OutBounce).OnComplete(() => {
            this.StartCoroutine(this.Fade());
        });
        
        Vector3 offset = new Vector3(10f, 10f, 0f);
        this.transform.DOJump(this.targetPosition + offset, 10f, 1, 1f).SetEase(Ease.OutBounce).OnComplete(() => {
            _choice.callback();
            this.Finish();
        });
    }

    private IEnumerator Fade()
    {
        this.canvasGroup.alpha = 1f;
        while (this.canvasGroup.alpha > 0)
        {
            this.canvasGroup.alpha -= 0.2f;
            yield return new WaitForSeconds(0.015f);
        }
        this.canvasGroup.alpha = 0;
    }
    public void Finish() 
    {
        this.StopAllCoroutines();
        // 제거
        this.transform.DOScaleY(0f, 0.15f);
        this.transform.DOScaleX(0f, 0.15f).SetDelay(0.05f).SetEase(Ease.OutBack).OnComplete(() => {
            Destroy(this.gameObject);
        });
    }
}
