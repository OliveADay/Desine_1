using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePatrol : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    [HideInInspector]
    public bool isFlipped = false;
    [HideInInspector]
    public bool mustPatrol;
    public bool checkGround;
    bool willGrounded;
    bool isGrounded;
    public Transform groundP;
    public Transform groundP2;
    public float groundR;
    public LayerMask terrainL;
    public Transform sideP;
    public float sideR;
    bool side;
    ContactFilter2D terrainC;
    public RigidbodyType2D baseT;
    public RigidbodyType2D falling;
    bool move;

    private void Start()
    {
        mustPatrol = true;
    }
    private void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }
        if (isGrounded)
        {
            rb.bodyType = baseT;
        }
        else
        {
            rb.bodyType = falling;
        }
    }
    public void Patrol()
    {
        if (checkGround)
        {
            if (!willGrounded || side)
            {
                Flip();
            }
        }
        else if (side)
        {
            Flip();
        }
        move = true;
    }

    public void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        speed = -speed;
        isFlipped = !isFlipped;
        mustPatrol = true;
    }
    private void FixedUpdate()
    {
        if (checkGround)
        {
            willGrounded = Physics2D.CircleCast(groundP.position, groundR, Vector2.down, 1, terrainL);
            isGrounded = Physics2D.CircleCast(groundP2.position, groundR, Vector2.down, 1, terrainL);
        }
        side = Physics2D.OverlapCircle(sideP.position, sideR, terrainL);
        if (move)
        {
            rb.MovePosition(new Vector2(transform.position.x + speed, transform.position.y));
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(sideP.position, sideR);
    }
}
