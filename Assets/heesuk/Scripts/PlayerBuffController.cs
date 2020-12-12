using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public enum STAT
{
    MOVE_SPEED,
    JUMP_POWER,
    JUMP_COUNT,
    HP,
    DAMAGE,
    WEAPON_CHANGE,
}
public class Buff 
{
    public STAT stat = STAT.MOVE_SPEED;
    public int value = 0;
    public Buff(STAT _stat, int _value)
    {
        this.stat = _stat;
        this.value = _value;
    }
    
}
public class PlayerBuffController : MonoBehaviour
{
    [SerializeField]
    private Transform player = null;
    [SerializeField]
    private TextMeshProUGUI messageText = null;

    private void Awake()
    {
        this.messageText.text = string.Empty;
        EventManager.on(EVENT_TYPE.PLAYER_BUFF, this.ShowBuff);
    }

    public void ShowBuff(EVENT_TYPE EventType, Component Sender, object Param = null)
    {
        this.transform.localPosition = this.player.transform.localPosition + new Vector3(0f, 1f, 0f);
        this.messageText.text = string.Empty;
        Buff buff = (Buff)Param;
        switch (buff.stat)
        {
            case STAT.MOVE_SPEED:
                this.messageText.text = $"이동속도 + {buff.value}";
                break;
            case STAT.JUMP_POWER:
                this.messageText.text = $"점프력 + {buff.value}";
                break;
            case STAT.JUMP_COUNT:
                this.messageText.text = $"점프 횟수 + {buff.value}";
                break;
            case STAT.HP:
                this.messageText.text = $"체력 + {buff.value}";
                break;
            case STAT.DAMAGE:
                this.messageText.text = $"공격력 + {buff.value}";
                break;
            case STAT.WEAPON_CHANGE:
                this.messageText.text = $"무기 변경 [{buff.value}]";
                break;
        }
        this.messageText.transform.DOLocalMoveY(100f, 1f).OnComplete(this.Finish);
    }
    private void Finish()
    {
        this.messageText.text = string.Empty;
        this.messageText.transform.localPosition = Vector3.zero;
    }
    private void OnDestroy() {
        
        EventManager.off(EVENT_TYPE.PLAYER_BUFF, this.ShowBuff);
    }
}
