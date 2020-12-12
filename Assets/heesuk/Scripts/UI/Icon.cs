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
        this.transform.DOScaleY(1f, 0.15f).SetEase(Ease.OutBack);
        this.transform.DOScaleX(1f, 0.15f).SetDelay(0.05f).SetEase(Ease.OutBack);
    }

    public void Trash(Buff _buff) 
    {   
        // 던지기
        this.transform.DOScaleY(1f, 0.15f).SetEase(Ease.OutBack);
        this.transform.DOScaleX(0.5f, 0.15f).SetDelay(0.05f).SetEase(Ease.OutBack);
        this.transform.DOBlendableRotateBy(Vector3.zero, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => {
            this.StartCoroutine(this.Fade());
        });

        this.transform.DOJump(this.targetPosition + new Vector3(0f, 0.2f, 0f), 10f, 1, 1f).SetEase(Ease.OutBounce).OnComplete(() => {
            
            EventManager.emit(EVENT_TYPE.PLAYER_BUFF, this, _buff);
            this.Finish();
        });
    }

    private IEnumerator Fade()
    {
        this.canvasGroup.alpha = 1f;
        while (this.canvasGroup.alpha > 0)
        {
            this.canvasGroup.alpha -= 0.2f;
            yield return new WaitForSeconds(0.025f);
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
