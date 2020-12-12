using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCtrl : MonoBehaviour
{
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

    Weapon weapon = Weapon.poopBranch;

    public static SwordCtrl Instance;
    BoxCollider2D bc;

    public float delayTime = 0f;
    public float maxDelayTime = 2f;
    private bool readyAttack = true;
    float weaponDamage;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();

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
            EnemyCtrl.Instance.enemyHp -= weaponDamage;
            Debug.Log("fdsa");
        }
    }

    void BoxColliderOn()
    {
        bc.enabled = false;
        bc.size = new Vector2(3f, 2f);
        bc.offset = new Vector2(-1.2f, 0f);
    }

    void WeaponSelect()
    {
        switch (weapon)
        {
            case Weapon.spir:
                weaponDamage = 2f;
                maxDelayTime = 1f;
                break;

            case Weapon.longspir:
                weaponDamage = 2f;
                maxDelayTime = 0.6f;
                break;

            case Weapon.dagger:
                weaponDamage = 3f;
                maxDelayTime = 1.4f;
                break;

            case Weapon.longsword:
                weaponDamage = 5f;
                maxDelayTime = 1f;
                break;

            case Weapon.excalibur:
                weaponDamage = 10f;
                maxDelayTime = 1.5f;
                break;

            case Weapon.poopBranch:
                weaponDamage = 2f;
                maxDelayTime = 1.5f;
                break;

            case Weapon.fish:
                weaponDamage = 2f;
                maxDelayTime = 1.2f;
                break;
        }
    }
}
