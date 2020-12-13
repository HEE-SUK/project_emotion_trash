using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IntroHilight : MonoBehaviour
{
    // textbubble은 Prefab으로 생성되게 하자
    [SerializeField]
    private Transform Player = null;
    [SerializeField]
    private HilightBubble hilightBubble = null;

    [SerializeField]
    private DialogInfo[] dialogInfo = {};

    public int dialogCount = 0;
    

    [SerializeField]
    private TextBubble textBubble = null;

    private bool isFinished = false;
    
    private void Start()
    {
        this.isFinished = false;
        this.dialogCount = 0;
        this.hilightBubble.Init(this.textBubble, this.dialogInfo[this.dialogCount].dialogs);

        for (int i = 0; i < this.dialogInfo.Length; i++)
        {
            this.dialogInfo[i].Init(this.textBubble, this.NextTextBubble, this.Finish, (i == this.dialogInfo.Length - 1));
        }
    }

    private void NextTextBubble()
    {
        this.dialogCount += 1;
        this.textBubble.On(this.dialogInfo[this.dialogCount].dialogs, this.dialogInfo[this.dialogCount].choices);
    }
    private void Finish()
    {
        this.isFinished = true;
    }

    void Update()
    {
        // 대화중 예외처리
        if(GameManager.Instance.isTalk || this.isFinished) { return; }

        float distance = (this.Player.transform.localPosition - this.transform.localPosition).magnitude;
        if(distance < 2f)
        {
            this.hilightBubble.On(this.dialogInfo[this.dialogCount].choices);
        }
        else
        {
            this.hilightBubble.Off();
        }
    }
}
