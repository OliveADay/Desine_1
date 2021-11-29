using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPatrol : MonoBehaviour
{
    public float speed;
    public Transform groundDetection;
    private bool isGrounded;
    public LayerMask whatIsGround;
    public float groundCheckRadius;
    bool isSomethingInFront;
    public Transform somethingInFrontPoint;
    public float somethingInFrontRange = 0.5f;
    public Animator animator;
    public Rigidbody2D rb;
    [HideInInspector]
    public bool mustPatrol;
    [HideInInspector]
    public bool isFlipped = false;
    public ShieldEnemy enemy;
    [HideInInspector]
    public bool isAttacking;
    float startspeed;
    public Collider2D baseBlock;
    public Collider2D AttackBlock;
    public float attackSpeed;
    public AudioManager audioManager;
    public Sound regularStep;
    bool isdone = true;
    public bool groundCheck;
    [HideInInspector]
    public int move;


    // Start is called before the first frame update
    void Start()
    {
        startspeed = speed;
        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }
    }
    void Patrol()
    {
        if (enemy.isDead == false)
        {
            if(isAttacking == false)
            {
                if (isGrounded == false || isSomethingInFront)
                {
                    Flip();
                }
                move = 1;
                animator.SetBool("IsWalking", true);
                if (isdone)
                {
                    StartCoroutine(Step(regularStep, Random.Range(1f, 2f)));
                }
            }
            else
            {
                if (isGrounded == false || isSomethingInFront)
                {
                    Flip();
                }
                move = 2;
                animator.SetBool("IsWalking", false);
            }
        }        
    }
    public IEnumerator Step(Sound sound, float  time)
    {
        isdone = false;
        yield return new WaitForSeconds(time);
        audioManager.Play(sound);
        isdone = true;
    }
    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            isGrounded = Physics2D.OverlapCircle(groundDetection.position, groundCheckRadius, whatIsGround);
            isSomethingInFront = Physics2D.OverlapCircle(somethingInFrontPoint.position, somethingInFrontRange, whatIsGround);
            if(move == 1)
            {
                rb.MovePosition(new Vector2(transform.position.x + speed, transform.position.y));
            }
            else if(move == 2)
            {
                rb.MovePosition(new Vector2(transform.position.x + attackSpeed, transform.position.y));
            }
        }
    }
    void Flip()
    {
        mustPatrol = false;
        isFlipped = !isFlipped;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        speed = -speed;
        attackSpeed = -attackSpeed;
        mustPatrol = true;
    }
}
