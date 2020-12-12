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
    public Weapon weapon = Weapon.poopBranch;

    public static SwordCtrl Instance;
    BoxCollider2D bc;
    Animator anim;

    public float delayTime = 0f;
    public float maxDelayTime = 0.3f;
    private bool readyAttack = true;
    public float weaponDamage;

    [HideInInspector]
    public float weaponBuff = 1f;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        this.weaponBuff = 1f;

        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        //bc.size = new Vector2(3f, 2f);
        //bc.offset = new Vector2(-1.2f, 0f);

        bc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
      //  WeaponSelect();

        delayTime += Time.deltaTime;

        if (delayTime >= maxDelayTime)
        {
            readyAttack = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (readyAttack)
            {
                anim.SetBool("isAttack", true);
                bc.enabled = true;

                Invoke("BoxColliderOn", 0.1f);
                readyAttack = false;
                delayTime = 0f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            // 공격 데미지 * 무기 버프
            EnemyCtrl.Instance.enemyHp -= weaponDamage * this.weaponBuff;
            Debug.Log("fdsa");
        }
    }

    void BoxColliderOn()
    {
        bc.enabled = false;
        bc.size = new Vector2(3f, 2f);
        bc.offset = new Vector2(-1.2f, 0f);
        anim.SetBool("isAttack", false);
    }

    public void WeaponSelect()
    {
        switch (weapon)
        {
            case Weapon.spir:
                weaponDamage = 2f;
                maxDelayTime = 0.3f;
                break;

            case Weapon.longspir:
                weaponDamage = 2f;
                maxDelayTime = 0.5f;
                break;

            case Weapon.dagger:
                weaponDamage = 3f;
                maxDelayTime = 0.3f;
                break;

            case Weapon.longsword:
                weaponDamage = 5f;
                maxDelayTime = 0.6f;
                break;

            case Weapon.excalibur:
                weaponDamage = 10f;
                maxDelayTime = 0.4f;
                break;

            case Weapon.poopBranch:
                weaponDamage = 2f;
                maxDelayTime = 0.2f;
                break;

            case Weapon.fish:
                weaponDamage = 2f;
                maxDelayTime = 0.2f;
                break;
        }
    }

    public void SetWeaponBuff(float _value)
    {
        this.weaponBuff *= _value;
    }
}
