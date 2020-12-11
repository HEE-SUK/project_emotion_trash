using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float moveSpeed;
    public float jumpPower;
    Rigidbody2D rb;
    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonUp("Horizontal"))
        {
            rb.velocity = new Vector2(rb.velocity.normalized.x * 0f, rb.velocity.y);
        }

        rb.AddForce(Vector2.right * Horizontal, ForceMode2D.Impulse);

        if (rb.velocity.x > moveSpeed)
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        else if (rb.velocity.x < moveSpeed * (-1))
            rb.velocity = new Vector2(moveSpeed * (-1), rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        if (Input.GetAxisRaw("Horizontal") < 0)
            sr.flipX = false;
        else if (Input.GetAxisRaw("Horizontal") > 0)
                sr.flipX = true;
    }
}
