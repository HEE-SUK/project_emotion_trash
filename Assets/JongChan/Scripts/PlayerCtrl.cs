using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    public float moveSpeed;
    public float jumpPower;
    public int originjump = 2;
    private int isjump = 2;
    [HideInInspector] public int PlayerLife = 5;
    private float invincibilityTime = 1;
    private bool isHit = false;

    private bool isDead = false;
    void Start()
    {
        this.isDead = false;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        EventManager.emit(EVENT_TYPE.UPDATE_HP, this, this.PlayerLife);
    }

    void Update()
    {
        // 대화중 예외처리
        if(GameManager.Instance.isTalk) { return; }

        Move();

        LookAt();

        if (PlayerLife == 0 && !this.isDead)
        {
            this.isDead = true;
            Debug.Log("dead");
            anim.SetTrigger("isDeath");
            PlayerLife = -1;
            EventManager.emit(EVENT_TYPE.PLAYER_DEAD, this);
            AudioManager.PlaySfx(SFX.PLAYER_DEAD);
        }

        if (isHit)
        {
            invincibilityTime += Time.deltaTime;
        }

        if (invincibilityTime >= 1)
        {
            sr.color = new Color(1, 1, 1, 1f);
            gameObject.layer = 0;
            invincibilityTime = 0;
            isHit = false;
        }
    }

    void Move()
    {
        if(this.isDead) { return; }
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
                AudioManager.PlaySfx(SFX.PLAYER_JUMP);
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
        if(this.isDead) { return; }
        
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

        else if (col.collider.CompareTag("Enemy"))
        {
            AudioManager.PlaySfx(SFX.PLAYER_ATTACKED);
            EventManager.emit(EVENT_TYPE.PLAYER_ATTACKED, this);
            isHit = true;
            if (isHit)
                gameObject.layer = 11;
            PlayerLife--;
            sr.color = new Color(1, 1, 1, 0.5f);


            // ui에 hp 전송
            EventManager.emit(EVENT_TYPE.UPDATE_HP, this, this.PlayerLife);
        }
    }
}
