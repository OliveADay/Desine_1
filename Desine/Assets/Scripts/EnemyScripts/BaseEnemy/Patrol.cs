using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Patrol : MonoBehaviour
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
    public Transform playerCheckPoint;    
    bool playerCheck;
    public float playerCheckRaduis;
    public LayerMask whatisPlayer;
    [HideInInspector]
    public bool isFlipped = false;
    public Enemy enemy;
    Transform player;
    bool attack = false;
    bool laterAttack;
    float startSpeed;
    public AudioManager audioManager;
    public Sound regularStep;
    bool isdone = true;
    public float Attackspeed;
    public bool agro;
    public string Walkanim;
    public string Runanim;
    public bool walkanim;
    public bool ground;
    int move;
    public Sound activeate;

    // Start is called before the first frame update
    void Start()
    {
        mustPatrol = true;
        startSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            PatrolFunction();
        }
    }
    void PatrolFunction()
    {
        if (ground)
        {
            if (!isGrounded)
            {
                Flip();
            }
        }
        if (isSomethingInFront)
        {
            Flip();
        }
        if (agro)
        {
            if (!attack)
            {
                move = 1;
                if (walkanim)
                {
                    animator.SetTrigger(Walkanim);
                }
                animator.SetBool(Runanim, false);
            }
            else
            {
                if (laterAttack)
                {
                    audioManager.Play(activeate);
                }
                move = 2;
                animator.SetBool(Runanim, true);
            }
        }

        else
        {
            move = 1;
            if (walkanim)
            {
                animator.SetTrigger(Walkanim);
            }
        }
        if (isdone)
        {
            StartCoroutine(step(regularStep, audioManager, Random.Range(1f, 2f)));
        }
        laterAttack = attack;
    }
    IEnumerator step (Sound sound, AudioManager audiomanager, float time)
    {
        isdone = false;
        yield return new WaitForSeconds(time);
        audiomanager.Play(sound);
        isdone = true;
    }
    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            if (agro)
            {
                attack = Physics2D.OverlapCircle(playerCheckPoint.position, playerCheckRaduis, whatisPlayer);
            }

            if (ground)
            {
                isGrounded = Physics2D.OverlapCircle(groundDetection.position, groundCheckRadius, whatIsGround);
            }
            isSomethingInFront = Physics2D.OverlapCircle(somethingInFrontPoint.position, somethingInFrontRange, whatIsGround);
            if(move == 1)
            {
                rb.MovePosition(new Vector2(transform.position.x + speed, transform.position.y));
            }
            else if(move == 2)
            {
                rb.MovePosition(new Vector2(transform.position.x + Attackspeed, transform.position.y));
            }
        }
    }
    void Flip()
    {
        mustPatrol = false;
        isFlipped = !isFlipped;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        speed = -speed;
        Attackspeed = -Attackspeed;
        mustPatrol = true;
    }
    private void OnDrawGizmosSelected()
    {
        if (agro)
        {
            Gizmos.DrawWireSphere(playerCheckPoint.position, playerCheckRaduis);
        }
        if (ground)
        {
            Gizmos.DrawWireSphere(groundDetection.position, groundCheckRadius);
        }      
        Gizmos.DrawWireSphere(somethingInFrontPoint.position, somethingInFrontRange);
    }
}