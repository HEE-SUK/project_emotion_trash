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
    public Transform npcParnet = null;
    private Vector3 targetPosition = new Vector3();

    public void Init(EMOTION _emotion, Transform _npcParent, Vector3 _targetPosition)
    {
        this.image.sprite = this.sprites[(int)_emotion];
        this.transform.localScale = Vector3.zero;
        this.transform.DOScaleY(1f, 0.15f).SetEase(Ease.OutBack);
        this.transform.DOScaleX(1f, 0.15f).SetDelay(0.05f).SetEase(Ease.OutBack);
    }

    public void Trash(Buff _buff) 
    {   
        this.transform.SetParent(this.npcParnet, false);
        
        // 던지기
        // this.transform.DOScaleY(1f, 0.15f).SetEase(Ease.OutBack);
        // this.transform.DOScaleX(0.5f, 0.15f).SetDelay(0.05f).SetEase(Ease.OutBack);

        Debug.Log("Trash");
        this.transform.DOMove(this.targetPosition, 2f).OnComplete(() => {
            EventManager.emit(EVENT_TYPE.PLAYER_BUFF, this, _buff);
            this.Finish();
        });
    }
    public void Finish() 
    {
        Debug.Log("finish");
        // 제거
        this.transform.DOScaleY(0f, 0.15f);
        this.transform.DOScaleX(0f, 0.15f).SetDelay(0.05f).SetEase(Ease.OutBack).OnComplete(() => {
            Destroy(this.gameObject);
        });
    }
}
