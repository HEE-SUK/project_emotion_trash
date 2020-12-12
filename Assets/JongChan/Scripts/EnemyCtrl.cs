using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    public enum EnemyType
    {
        slime = 0,
        goblin,
        bet,
        earthWorm,
        cube,
        ghost,
        dragon
    }

    public EnemyType enemyType;

    Rigidbody2D rb;
    GameObject traceTarget;
    CircleCollider2D cc;
    
    
    public float movePower;
    int movementFlag = 0;
    bool isTracing = false;
    public float enemyHp = 1;

    public static EnemyCtrl Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CheakType();

        rb = GetComponent<Rigidbody2D>();
        cc = GetComponentInChildren<CircleCollider2D>();
        gameObject.GetComponent<Pathfinding.AIDestinationSetter>().enabled = true;

        StartCoroutine("ChangeMovement");

        SwordCtrl.Instance.weaponBuff = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (this.enemyHp <= 0)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            traceTarget = col.gameObject;

            if(enemyType == EnemyType.bet || enemyType == EnemyType.ghost)
            {
                gameObject.GetComponent<Pathfinding.AIDestinationSetter>().enabled = false;
            }
        }

        if (col.CompareTag("Weapon"))
        {
            enemyHp -= SwordCtrl.Instance.weaponDamage + SwordCtrl.Instance.weaponBuff;
            Debug.Log("fdsa");
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isTracing = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isTracing = false;
        }
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        string dist = "";

        if(isTracing)
        {
            Vector3 playerPos = traceTarget.transform.position;

            if (playerPos.x < transform.position.x)
                dist = "Left";
            else if (playerPos.x > transform.position.x)
                dist = "Right";
        }

        else
        {
            if (movementFlag == 1)
                dist = "Left";
            else if (movementFlag == 2)
                dist = "Right";
        }

        if (dist == "Left")
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dist == "Right")
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    IEnumerator ChangeMovement()
    {
        movementFlag = Random.Range(0, 3);

        yield return new WaitForSeconds(3f);

        StartCoroutine("ChangeMovement");
    }

    void CheakType()
    {
        switch(enemyType)
        {
            case EnemyType.slime:
                enemyHp = 2;
                movePower = 3;
                cc.radius = 5;
                break;

            case EnemyType.goblin:
                enemyHp = 3;
                movePower = 3;
                cc.radius = 5;
                break;

            case EnemyType.bet:
                enemyHp = 7;
                movePower = 3;
                cc.radius = 5;
                break;

            case EnemyType.earthWorm:
                enemyHp = 8;
                movePower = 3;
                cc.radius = 5;
                break;

            case EnemyType.cube:
                enemyHp = 20;
                movePower = 3;
                cc.radius = 5;
                break;

            case EnemyType.ghost:
                enemyHp = 7;
                movePower = 3;
                cc.radius = 5;
                break;

            case EnemyType.dragon:
                enemyHp = 20;
                movePower = 3;
                cc.radius = 5;
                break;
        }
    }
}
