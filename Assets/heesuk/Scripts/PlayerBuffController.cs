using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public enum STAT
{
    NONE,
    MOVE_SPEED,
    JUMP_POWER,
    JUMP_COUNT,
    HP,
    DAMAGE,
    WEAPON_CHANGE,
    ENDING_POINT,
}
public class Buff 
{
    public STAT stat = STAT.MOVE_SPEED;
    public float value = 0;
    public string statText = string.Empty;
    public Buff(STAT _stat, float _value, string _statText)
    {
        this.stat = _stat;
        this.value = _value;
        this.statText = _statText;
    }
    
}
public class PlayerBuffController : MonoBehaviour
{
    [SerializeField]
    private PlayerCtrl playerCtrl = null;
    [SerializeField]
    private SwordCtrl swordCtrl = null;
    [SerializeField]
    private TextMeshProUGUI messageText = null;

    [SerializeField]
    private string[] weaponNames = {"일반 검","긴 검","단검","00검","엑스칼리버","똥막대기","고등어"};
    private void Awake()
    {
        this.messageText.text = string.Empty;
        EventManager.on(EVENT_TYPE.PLAYER_BUFF, this.ShowBuff);
    }

    public void ShowBuff(EVENT_TYPE EventType, Component Sender, object Param = null)
    {
        this.transform.localPosition = this.playerCtrl.transform.localPosition + new Vector3(0f, 1f, 0f);
        this.messageText.transform.localScale = Vector3.zero;
        this.messageText.transform.DOScaleX(1f,0.2f).SetEase(Ease.OutBack);
        this.messageText.transform.DOScaleY(1f,0.2f).SetDelay(0.1f).SetEase(Ease.OutBack);
        this.messageText.text = string.Empty;
        Buff buff = (Buff)Param;
        
        this.messageText.text = buff.statText;
        switch (buff.stat)
        {
            case STAT.MOVE_SPEED:
                this.messageText.text = buff.statText;
                this.playerCtrl.moveSpeed = this.playerCtrl.moveSpeed * buff.value;
                break;
            case STAT.JUMP_POWER:
                this.playerCtrl.jumpPower = this.playerCtrl.jumpPower * buff.value;
                break;
            case STAT.JUMP_COUNT:
                this.playerCtrl.originjump = (int)buff.value;
                break;
            case STAT.HP:
                this.playerCtrl.PlayerLife += (int)buff.value;
                // ui에 hp 전송
                EventManager.emit(EVENT_TYPE.UPDATE_HP, this, this.playerCtrl.PlayerLife);
                break;
            case STAT.DAMAGE:
                this.swordCtrl.SetWeaponBuff(buff.value);
                break;
            case STAT.WEAPON_CHANGE:
                Weapon weapoonType = (Weapon)(int)buff.value;
                this.swordCtrl.weapon = weapoonType;
                this.swordCtrl.WeaponSelect();
                break;
            case STAT.ENDING_POINT:
                GameManager.Instance.endingPoint += 1;
                break;
            default:
                break;
        }
        this.messageText.transform.DOLocalMoveY(200f, 1.5f).OnComplete(this.Finish);
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
