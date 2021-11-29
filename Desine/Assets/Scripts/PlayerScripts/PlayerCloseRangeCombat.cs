using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloseRangeCombat : MonoBehaviour
{
    public Animator animator;
    public float damageGiven;
    float nextAttackTime = 0f;
    public float attackRate = 2f;
    public Transform attackPoint;
    public Transform attackPointUp;
    public LayerMask enemyLayers;
    public LayerMask blockingLayers;
    public LayerMask destructL;
    public float attackRange = 2f;
    [HideInInspector]
    public bool blocked = false;
    public AudioManager audioManager;
    public Sound attack;
    public Sound hitEnemy;
    public Sound shieldHit;
    public PlayerController pCon;
    

    // Update is called once per frame
    void Update()
    {
        
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    AttackUp();
                    nextAttackTime = Time.time + 1f / attackRate;
                    return;
                }
                else
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
            
        }

    }
    void Attack()
    {
        audioManager.Play(attack);
        animator.SetTrigger("Attack");
        bool attackAble = true;

        bool blocked = Physics2D.OverlapBox(attackPoint.transform.position, new Vector2(attackRange, attackRange), blockingLayers);
        if (blocked)
        {
            Collider2D[] blocks = Physics2D.OverlapBoxAll(attackPoint.transform.position, new Vector2(attackRange, attackRange), blockingLayers);
            foreach (Collider2D block in blocks)
            {
                ShieldPatrol blockMove = block.gameObject.GetComponentInParent<ShieldPatrol>();
                ShieldEnemy blockE = block.gameObject.GetComponentInParent<ShieldEnemy>();
                if(blockE != null)
                {
                    if(block.gameObject.GetComponent<BlockParticleAffect>() != null)
                    {
                        if (!blockE.isDead)
                        {
                            /*if(transform.position.x - block.gameObject.transform.position.x < transform.position.x - block.gameObject.transform.parent.position.x)
                            {*/
                                if (blockMove.isFlipped != !pCon.facingRight || block.tag != "AttackShield")
                                {
                                    attackAble = false;
                                    audioManager.Play(shieldHit);
                                    StartCoroutine(block.gameObject.GetComponent<BlockParticleAffect>().BlockEffect());
                                }
                            //}                            
                        }
                        else
                        {
                            attackAble = false;
                        }
                    }
                }
                else
                {
                    print("blockE is null");
                }
            }
        }
        if(attackAble)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                Entity enemyM = enemy.GetComponent<Entity>();
                if (enemyM != null)
                {
                    audioManager.Play(hitEnemy);
                    enemyM.TakeDamage(damageGiven, gameObject);
                }
            }
            Collider2D Dustructable = Physics2D.OverlapCircle(attackPoint.position, attackRange, destructL);
            if(Dustructable != null)
            {
                destructable des = Dustructable.GetComponent<destructable>();
                if(des != null)
                {
                    des.destruct(true);
                }
            }
        }        
    }
    void AttackUp()
    {
        animator.SetTrigger("UpAttack");

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(attackRange, attackRange), blockingLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Entity enemyM = enemy.GetComponent<Entity>();
            if (enemyM != null)
            {
                enemyM.TakeDamage(damageGiven, gameObject);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(attackPoint.position, new Vector3(attackRange, attackRange, attackRange));
        Gizmos.DrawWireCube(attackPointUp.position, new Vector3(attackRange, attackRange, attackRange));
    }
}
