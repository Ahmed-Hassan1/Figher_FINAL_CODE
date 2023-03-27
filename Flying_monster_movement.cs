using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flying_monster_movement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Transform player;
    Vector2 playerPos;
    Vector2 playerPosCache;

    
    bool isRight = true;

    bool getNewPoint = true;
    [SerializeField] float roamingSpeed = 1f;
    Vector2 randomRoamingPoint;

    bool getPlayerPos = true;
    [SerializeField] float attackSpeed = 1f;
    public GameObject gameOver;

    Animator anim;

    HealthSystem health;
    [SerializeField] Slider healthBar;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health= GetComponent<HealthSystem>();

        anim.SetTrigger("ToIdle");
    }

    bool playDeath = true;

    private void FixedUpdate()
    {
        healthBar.value = health.GetCurrentHealth();
        if (health.isDead() && playDeath)
        {
            playDeath = false;
            rb.velocity = Vector2.zero;
            anim.SetTrigger("Death");

            gameOver.SetActive(true);
            Time.timeScale = 0;
        }

        if (transform.position.y >= 2 && !health.isDead())
        {
            roamingSpeed = 0.5f;

            DoRandomAttack();
        }
    }


    public void DivingAttack()
    {
        if (getPlayerPos)
        {
            getPlayerPos = false;
            FlipToPlayer();
            playerPos = player.position - transform.position;
            playerPosCache= player.position;
            playerPos.Normalize();
        }

        if (!getPlayerPos)
        {
            
            rb.velocity = playerPos * attackSpeed;
        }

        if ((transform.position.y- playerPosCache.y) <= 0.1 && (transform.position.x- playerPosCache.x) <= 0.1)
        {
            rb.velocity = Vector2.zero;
            getPlayerPos = true;
            anim.SetTrigger("ToIdle");
            roamingSpeed = 3f;
        }

        
    }


    private void FlipToPlayer()
    {
        float DeltaXPos = player.position.x - transform.position.x;

        if (isRight && DeltaXPos > 0f || !isRight && DeltaXPos < 0f)
        {
            isRight = !isRight;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1;
            transform.localScale = localscale;
        }
    }

    public void Roaming()
    {
        if (getNewPoint)
        {
            getNewPoint = false;
            float randomX = Random.Range(8, -8);
            float randomY = Random.Range(4, 2);
            randomRoamingPoint = new Vector2(randomX, randomY);
        }

        if (!getNewPoint)
        {
            Vector2 posDiff;
            posDiff= (Vector3)randomRoamingPoint - transform.position;
            posDiff.Normalize();
            rb.velocity = posDiff * roamingSpeed;

            float DeltaXPos = randomRoamingPoint.x - transform.position.x;

            if (isRight && DeltaXPos > 0f || !isRight && DeltaXPos < 0f)
            {
                isRight = !isRight;
                Vector3 localscale = transform.localScale;
                localscale.x *= -1;
                transform.localScale = localscale;
            }
        }

        if (transform.position.x-randomRoamingPoint.x <=0.1 && transform.position.y - randomRoamingPoint.y <= 0.1)
        {
            getNewPoint = true;
        }
    }

    void DoRandomAttack()
    {
        int picker = Random.Range(1, 80);

        if (picker==2)
        {
            anim.SetTrigger("DiveAttack");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (collision != null)
            {
                collision.GetComponent<HealthSystem>().ReduceHP(20);
            }
        }
        catch (System.Exception)
        {

        }
        
        
    }
}
