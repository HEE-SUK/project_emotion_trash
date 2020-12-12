using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    public float moveSpeed;
    public float jumpPower;
    public int originjump = 2;
    private int isjump = 2;
    public int PlayerLife = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 대화중 예외처리
        if(GameManager.Instance.isTalk) { return; }

        Move();

        LookAt();

        if (PlayerLife == 0)
        {
            Debug.Log("dead");
            anim.SetTrigger("isDeath");
            PlayerLife = -1;
        }
    }

    void Move()
    {
        Debug.Log(isjump);

        float Horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonUp("Horizontal"))
        {
            rb.velocity = new Vector2(rb.velocity.normalized.x * 0f, rb.velocity.y);
        }

        rb.AddForce(Vector2.right * Horizontal, ForceMode2D.Impulse);

        if (rb.velocity.x > moveSpeed)
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        else if (rb.velocity.x < moveSpeed * (-1))
            rb.velocity = new Vector2(moveSpeed * (-1), rb.velocity.y);

        if (isjump >= 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (isjump == 2)
                {
                    anim.SetTrigger("isJump");
                }

                else if (isjump == 1)
                {
                    anim.SetTrigger("isDoubleJump");
                }
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                isjump--;
            }
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("isRun", true);
        }

        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("isRun", true);
        }

        else
        {
            anim.SetBool("isRun", false);
        }
    }

    void LookAt()
    {
        Vector3 mPosition = Input.mousePosition;

        if (mPosition.x <= Screen.width / 2)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (mPosition.x >= Screen.width / 2)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
        {
            this.isjump = this.originjump;
        }

        if (col.collider.CompareTag("Enemy"))
        {
            PlayerLife--;
        }
    }
}
