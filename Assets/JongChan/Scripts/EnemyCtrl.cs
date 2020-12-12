using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject traceTarget;

    Vector3 movement;
    public float movePower;
    public int creatureType = 0;
    int movementFlag = 0;
    bool isTracing = false;
    public int enemyHp = 1;

    public static EnemyCtrl Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine("ChangeMovement");
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (enemyHp <= 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (creatureType == 0)
            return;

        if (col.CompareTag("Player"))
        {
            traceTarget = col.gameObject;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (creatureType == 0)
            return;

        if (col.CompareTag("Player"))
        {
            isTracing = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (creatureType == 0)
            return;

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
}
