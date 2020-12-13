using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon
{
    spir = 0,
    longspir,
    dagger,
    longsword,
    excalibur,
    poopBranch,
    fish
}

public class SwordCtrl : MonoBehaviour
{

    [SerializeField]
    public Weapon weapon;

    public static SwordCtrl Instance;
    BoxCollider2D bc;
    Animator anim;
    SpriteRenderer sr;

    public float delayTime = 0f;
    public float maxDelayTime = 0.3f;
    private bool readyAttack = true;
    public float weaponDamage;
    [SerializeField] Sprite[] weaponSprite = new Sprite[7];
    public Sprite ewar;

    [HideInInspector]
    public float weaponBuff = 1f;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        weapon = Weapon.spir;
        //WeaponSelect();

        //this.weaponBuff = 1f;

        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        //bc.size = new Vector2(3f, 2f);
        //bc.offset = new Vector2(-1.2f, 0f);

        bc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        WeaponSelect();
        delayTime += Time.deltaTime;

        if (delayTime >= maxDelayTime)
        {
            readyAttack = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (readyAttack)
            {
                AudioManager.PlaySfx(SFX.PLAYER_ATTACK);
                anim.SetBool("isAttack", true);
                bc.enabled = true;

                Invoke("BoxColliderOn", 0.1f);
                readyAttack = false;
                delayTime = 0f;
            }
        }
    }

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.CompareTag("Enemy"))
    //    {
    //        // 공격 데미지 * 무기 버프
    //        EnemyCtrl.Instance.enemyHp -= weaponDamage + this.weaponBuff;
            
    //        Debug.Log("fdsa");
    //    }
    //}

    void BoxColliderOn()
    {
        bc.enabled = false;
        //bc.size = new Vector2(3f, 2f);
        //bc.offset = new Vector2(-1.2f, 0f);
        anim.SetBool("isAttack", false);
    }

    public void WeaponSelect()
    {
        switch (weapon)
        {
            case Weapon.spir:
                weaponDamage = 2f;
                maxDelayTime = 0.3f;
                sr.sprite = weaponSprite[0];
                break;

            case Weapon.longspir:
                weaponDamage = 2f;
                maxDelayTime = 0.5f;
                sr.sprite = weaponSprite[1];
                break;

            case Weapon.dagger:
                weaponDamage = 3f;
                maxDelayTime = 0.3f;
                sr.sprite = weaponSprite[2];
                break;

            case Weapon.longsword:
                weaponDamage = 5f;
                maxDelayTime = 0.6f;
                sr.sprite = weaponSprite[3];
                break;

            case Weapon.excalibur:
                weaponDamage = 10f;
                maxDelayTime = 0.4f;
                sr.sprite = weaponSprite[4];
                break;

            case Weapon.poopBranch:
                weaponDamage = 2f;
                maxDelayTime = 0.2f;
                sr.sprite = weaponSprite[5];
                break;

            case Weapon.fish:
                weaponDamage = 2f;
                maxDelayTime = 0.2f;
                sr.sprite = weaponSprite[6];
                break;
        }
    }

    public void SetWeaponBuff(float _value)
    {
        this.weaponBuff += _value;
    }
}
