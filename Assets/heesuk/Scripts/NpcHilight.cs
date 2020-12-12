using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHilight : MonoBehaviour
{
    [SerializeField]
    private Transform Player = null;
    [SerializeField]
    private HilightBubble hilightBubble = null;

    [SerializeField]
    private TextBubble textBubble = null;

    [SerializeField]
    private List<string> dialogs = new List<string>();
    void Start()
    {
        this.dialogs = new List<string>();
        this.dialogs.Add("what is this");
        this.dialogs.Add("this is TextMesh Pro");
        this.dialogs.Add("oh my god");
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
