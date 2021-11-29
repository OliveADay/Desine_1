using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    public Transform whenAttackPoint;
    public float attackRate = 1f;
    float timeUntilNextAttack = 0f;
    public Transform attackPoint;
    public float whenAttackRandius = 1f;
    public int damageGiven = 20;
    public float attackRadius = 1f;
    Player player;
    public Animator animator;
    bool attack;
    bool shouldAttack;
    public LayerMask playerLayer;
    public Patrol movement;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        shouldAttack = Physics2D.OverlapCircle(whenAttackPoint.position, whenAttackRandius, playerLayer);
        if (timeUntilNextAttack <= 0)
        {
            if (shouldAttack)
            {
                AttackAnimation();
                timeUntilNextAttack = attackRate;
            }
        }
        else
        {
            timeUntilNextAttack -= Time.deltaTime;
        }
        if (animator.GetBool("IsAttacking"))
        {
            movement.mustPatrol = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            movement.mustPatrol = true;
        }
    }
    void AttackAnimation()
    {
        animator.SetBool("IsAttacking", true);
    }
    public void Attack()
    {
        attack = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
        if (attack)
        {
            player.TakeDamage(damageGiven, this.gameObject, attack);
        }
        animator.SetBool("IsAttacking", false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        Gizmos.DrawWireSphere(whenAttackPoint.position, whenAttackRandius);
    }
}
