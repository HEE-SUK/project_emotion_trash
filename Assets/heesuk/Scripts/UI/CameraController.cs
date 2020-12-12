using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera = null;
    [SerializeField]
    private Transform player = null;

    private readonly float endSize = 3f;
    private readonly float originSize = 7f;
    private float currentSize = 0f;

    private readonly float cameraZ = -10f;

    private void Start()
    {
        // 이벤트 초기화
        EventManager.on(EVENT_TYPE.CLOSE_UP, this.CloseUp);
        EventManager.on(EVENT_TYPE.PLAYER_ATTACKED, this.PlayerAttacked);
        this.Init(this.player.localPosition);
    }

    public void Init(Vector3 _position)
    {
        // 초기위치
        this.transform.localPosition = _position;
        // 시야각awaww
        this.currentSize = this.originSize;
        // 시작 연출
        this.DOKill();
        this.mainCamera.orthographicSize = this.originSize;

        // 카메라 팔로우 시작
        this.StopAllCoroutines();
        this.StartCoroutine(this.MoveCamera());
    }

    private IEnumerator MoveCamera()
    {
        while (true)
        {
            // 타겟위치 초기화
            Vector3 playerPosition = this.player.transform.localPosition;
            Vector3 targetPosition = new Vector3();
            targetPosition.x = playerPosition.x;
            targetPosition.y = playerPosition.y + 3;
            targetPosition.z = this.cameraZ;
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, targetPosition, 0.05f);

            yield return new WaitForFixedUpdate();
        }
    }

    private void CloseUp(EVENT_TYPE eventType, Component sender, object param = null)
    {
        this.StopAllCoroutines();
        Vector3 playerPosition = this.player.transform.localPosition;
        Vector3 targetPosition = new Vector3();
        targetPosition.x = playerPosition.x;
        targetPosition.y = playerPosition.y;
        targetPosition.z = this.cameraZ;
        
        this.transform.DOKill();
        this.transform.DOLocalMove(targetPosition,0.4f);
        this.mainCamera.orthographicSize = this.originSize;
        this.mainCamera.DOOrthoSize(this.endSize, 1f).SetEase(Ease.OutBack).SetUpdate(true).OnComplete(() => {
            SceneManager.LoadScene(GameManager.Instance.nextSceneName);
        });
    }

    private void PlayerAttacked(EVENT_TYPE eventType, Component sender, object param = null)
    {
        this.transform.DOKill();
        this.transform.DOShakePosition(0.25f, 0.75f, 50).SetUpdate(true);
    }

    public void Finish()
    {
        this.StopAllCoroutines();
        EventManager.off(EVENT_TYPE.CLOSE_UP, this.CloseUp);
        EventManager.off(EVENT_TYPE.PLAYER_ATTACKED, this.PlayerAttacked);
    }
    private void OnDestroy()
    {
        this.Finish();
    }
}
