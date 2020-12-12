using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHilight : MonoBehaviour
{
    [SerializeField]
    private Transform Player = null;
    [SerializeField]
    private HilightBubble hilightBubble = null;


    // textbubble은 PREFAB으로 생성되게하자
    [SerializeField]
    private TextBubble textBubble = null;

    [SerializeField]
    private List<string> dialogs = new List<string>();

    // 지문의 갯수가 됩니다.
    [SerializeField]
    private EMOTION[] emotionTypes = {EMOTION.A};

    void Start()
    {
        // this.dialogs.Add("안녕하세요");
        // this.dialogs.Add("저는 테스트입니다.");
        // this.dialogs.Add("만나서 반가워요 지문이 얼마나\r\n 길어져도 이상이없는지 확인해볼까요?");
        this.hilightBubble.Init(this.textBubble, this.dialogs);
    }

    void Update()
    {
        // 대화중 예외처리
        if(GameManager.Instance.isTalk) { return; }

        float distance = (this.Player.transform.localPosition - this.transform.localPosition).magnitude;
        if(distance < 2f)
        {
            this.hilightBubble.On();
        }
        else
        {
            this.hilightBubble.Off();
        }
    }
}
