using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AppearButton : MonoBehaviour
{
    private void OnEnable()
    {
        this.transform.localScale = Vector3.zero;
        this.transform.DOScaleY(1f, 0.2f);
        this.transform.DOScaleX(1f, 0.2f).SetDelay(0.05f).SetEase(Ease.OutBack);
    }
}
