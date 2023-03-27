using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float horiz;
    private float speed = 10f;
    private float jumpPower = 16f;
    private bool isRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundchk;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] Slider healthBar;

    private Animator anim;
    private HealthSystem health;

    public GameObject gameOver;

    private void Start()
    {
        anim = GetComponent<Animator>();
        health = GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        horiz = Input.GetAxisRaw("Horizontal");
        healthBar.value = health.GetCurrentHealth();

        if (isGrounded())
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", false);
        }
        else
        {
            anim.SetBool("falling", true);
        }
        if (Input.GetKeyDown(KeyCode.W) && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            anim.SetBool("jumping", true);
        }

        if (horiz==0 || !isGrounded())
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        Flip();

        if (health.isDead())
        {
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horiz * speed, rb.velocity.y);
    }

    private void Flip()
    {
        if (isRight && horiz<0f  ||  !isRight && horiz>0f)
        {
            isRight = !isRight;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1;
            transform.localScale = localscale;
        }
    }

    private bool isGrounded()
    {

        return Physics2D.OverlapCircle(groundchk.position, 0.2f, groundlayer);
    }
}
