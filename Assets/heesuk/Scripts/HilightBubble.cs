using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class HilightBubble : MonoBehaviour
{
    [SerializeField]
    private Image hilightImage = null;

    private bool isShown = false;
    private void Awake()
    {
        this.hilightImage.transform.localScale = Vector3.zero;
        this.isShown = false;
    }

    public void On()
    {
        if(this.isShown) { return; }
        this.isShown = true;
        // this.hilightImage.transform.localScale = Vector3.zero;
        this.hilightImage.DOKill();
        this.hilightImage.transform.DOScaleX(1f, 0.1f).SetEase(Ease.OutBack);
        this.hilightImage.transform.DOScaleY(1f, 0.1f).SetDelay(0.05f).SetEase(Ease.OutBack);
        
        this.StartCoroutine(this.Stay());
    }

    private IEnumerator Stay()
    {
        bool isKeyDown = false;
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if(Input.GetKeyDown(KeyCode.F) && !isKeyDown)
            {
                isKeyDown = true;
                Debug.Log("텍스트");
            }
        }
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
