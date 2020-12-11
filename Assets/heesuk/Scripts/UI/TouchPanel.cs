using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void OnStick(Vector2 _position);
public delegate void OffStick();

public class TouchPanel : MonoBehaviour, IPointerEnterHandler, IDragHandler, IPointerExitHandler
{
    private OnStick onStick = null;
    private OffStick offStick = null;
    
    private Vector2 startPosition = new Vector2();
    private Vector2 stayPosition = new Vector2();

    public void Init(OnStick _onStick, OffStick _offStick)
    {
        // UI(부모 오브젝트)에서 StickCallback을 매개변수로 초기화해줘야 합니다.
        this.onStick = _onStick;
        this.offStick = _offStick;
    }


    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        // touch 시작
        this.startPosition = eventData.position;
        this.StartCoroutine(this.OnStay());
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        // touch 유지
        this.stayPosition = eventData.position - this.startPosition;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        // touch 중단
        this.StopAllCoroutines();
        
        this.offStick();
        // 초기화
        this.startPosition = Vector2.zero;
        this.stayPosition = Vector2.zero;
    }

    private IEnumerator OnStay()
    {
        // touch 유지
        Vector2 movePosition = new Vector2();
        while (true)
        {
            movePosition = (this.stayPosition.magnitude > 20f) ? this.stayPosition.normalized : Vector2.zero;
            this.onStick(movePosition);
            Quaternion currentAngle = CommonManager.GetDirection(Vector2.zero, movePosition);
            yield return new WaitForEndOfFrame();
        }
    }
}