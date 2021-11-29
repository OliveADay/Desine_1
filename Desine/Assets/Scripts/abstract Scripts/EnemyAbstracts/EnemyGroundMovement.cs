using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class EnemyGroundMovement : MonoBehaviour
{
    public float speed = 40;
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
    public string hurtAnimation;
    public string runAnimation;
    bool patrol = true;
    float startSpeed;
    public bool agro;
    bool shouldAgro;
    public Transform agroPoint;
    public float agroRaduis;
    public LayerMask whatToAgro;
    Transform playerPos;
    public float chaseGravity = 50f;
    public float patrolGravity = 1f;


    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        startSpeed = speed;
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
        if (patrol)
        {
            if (isGrounded == false || isSomethingInFront)
            {
                Flip();
            }
            rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
            animator.SetTrigger(hurtAnimation);
        }        
    }
    void AgroFunction()
    {
        if (shouldAgro)
        {
            patrol = false;
            rb.MovePosition(Vector2.MoveTowards(transform.position, playerPos.position, startSpeed * 0.3f * Time.deltaTime));
            rb.gravityScale = chaseGravity;
            animator.SetTrigger(runAnimation);
        }
        else
        {
            patrol = true;
            rb.gravityScale = patrolGravity;
        }
    }
    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            if (agro)
            {
                AgroFunction();
            }
            shouldAgro = Physics2D.OverlapCircle(agroPoint.position, agroRaduis, whatToAgro);
            isGrounded = Physics2D.OverlapCircle(groundDetection.position, groundCheckRadius, whatIsGround);
            isSomethingInFront = Physics2D.OverlapCircle(somethingInFrontPoint.position, somethingInFrontRange, whatIsGround);
        }
    }
    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        speed = -speed;
        mustPatrol = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(agroPoint.position, agroRaduis);
        Gizmos.DrawWireSphere(groundDetection.position, groundCheckRadius);
        Gizmos.DrawWireSphere(somethingInFrontPoint.position, somethingInFrontRange); 
    }
}
