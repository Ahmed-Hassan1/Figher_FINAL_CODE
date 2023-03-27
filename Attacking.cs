using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    Animator anim;
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask enemyTag;
    float attackRange = 1f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            anim.SetTrigger("Attack_1");
        }

    }

    public void AttackAnimEvent()
    {
        Collider2D[] detectEnemy = Physics2D.OverlapCircleAll(attackPoint.position,attackRange, enemyTag);

        foreach(Collider2D enemy in detectEnemy)
        {
            enemy.GetComponent<HealthSystem>().ReduceHP(10f);
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
