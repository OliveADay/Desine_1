using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAttack : MonoBehaviour
{
    public Transform ShieldPoint;
    public float shieldRaduis;
    public Animator animator;
    public Transform agroPoint;
    public float agroRaduis;
    public LayerMask whatisAgro;
    bool isAgro;
    public float noticeTime = 1f;
    bool isAttacking;
    public ShieldPatrol movement;
    public int damageGiven;
    float nextAttackTime = 0f;
    public float attackRate = 1f;
    public float attackSpeed;
    public Rigidbody2D rb;
    bool isdone = true;
    bool stillAgro = true;
    public AudioManager audioM;
    public Sound activate;
    bool mainAgro;
    bool lateMainAgro;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isAgro = Physics2D.OverlapCircle(agroPoint.position, agroRaduis, whatisAgro);
        if (nextAttackTime <= Time.time)
        {
            if (isAgro)
            {
                mainAgro = isAgro;
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        if(isAgro == false && movement.isAttacking)
        {
            if (isdone)
            {
                StartCoroutine(TimeUp(isAgro, 10));
            }
        }
        if(mainAgro && !lateMainAgro)
        {
            audioM.Play(activate);
        }
        lateMainAgro = mainAgro;
    }
    void Attack()
    {
        movement.isAttacking = true;
        animator.SetBool("Isattacking", true);


        isAttacking = Physics2D.OverlapCircle(ShieldPoint.position, shieldRaduis, whatisAgro);
        if(isAttacking)
        {
            FindObjectOfType<Player>().TakeDamage(damageGiven, this.gameObject, false);
        }
        
    }
    IEnumerator TimeUp(bool variable, float time)
    {
        isdone = false;
        yield return new WaitForSeconds(time);
        if (!variable)
        {
            stillAgro = false;
            mainAgro = stillAgro;
        }
        if (!stillAgro)
        {
            movement.isAttacking = false;
            animator.SetBool("Isattacking", false);
        }
        isdone = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(agroPoint.position, agroRaduis);
        Gizmos.DrawWireSphere(ShieldPoint.position, shieldRaduis);
    }
}
