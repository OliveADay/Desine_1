using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPatrol : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform[] posA;
    public float speed;
    bool OnT = false;
    Transform T;
    int index = 0;
    public bool isFlipped = false;
    public bool flip;
    [HideInInspector]
    public bool attacking;
    public bool attack;
    [HideInInspector]
    public GameObject p;
    public float attackS;
    public LayerMask colls;
    Vector3 lastPos;
    bool hit;
    public Vector3 minOffset;
    public Vector3 maxOffset;
    Vector3 offset;
    public avoidO av;
    bool dir = false;
    public bool goS;
    public GameObject shooter;
    // Start is called before the first frame update
    void Start()
    {
        T = posA[index];
        p = GameObject.FindGameObjectWithTag("Player");
        lastPos = transform.position;
        if (attack)
        {
            offset.x = Random.Range(minOffset.x, maxOffset.x);
            offset.y = Random.Range(minOffset.y, maxOffset.y);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (index >= posA.Length || index < 0)
        {
            if (goS)
            {
                dir = !dir;
                if (!dir)
                {
                    index += 1;
                }
                else
                {
                    index -= 1;
                }
            }
            else
            {
                index = 0;
            }
        }

        if (attack)
        {
            if (!attacking)
            {
                T = posA[index];
            }
            else
            {
                if(p != null)
                {
                    T = p.transform;
                }
                if(av != null)
                {
                    if (av.avoid)
                    {
                        print("obstacle");
                        attacking = false;
                    }
                }
            }
        }
        else
        {
            T = posA[index];
        }
        
        if (transform.position != T.position + offset)
        {
            OnT = false;
            if(transform.position.x - T.position.x + offset.x > 0)
            {
                if (!isFlipped)
                {
                    Flip();
                }
            }
            else if(transform.position.x - T.position.x + offset.x < 0)
            {
                if (isFlipped)
                {
                    Flip();
                }
            }
        }
        else
        {
            if (attacking)
            {
                offset.x = Random.Range(minOffset.x, maxOffset.x);
                offset.y = Random.Range(minOffset.y, maxOffset.y);
            }            
            if (goS)
            {
                if (!dir)
                {
                    index += 1;
                }
                else
                {
                    index -= 1;
                }
            }
            else
            {
                index += 1;
            }
            OnT = true;
        }
    }
    private void FixedUpdate()
    {
        if (!OnT)
        {
            if (attacking)
            {
                if(shooter != null)
                {
                    if (!shooter.activeSelf)
                    {
                        shooter.SetActive(true);
                    }
                }
                rb.MovePosition(Vector2.MoveTowards(transform.position, T.position + offset, attackS * Time.deltaTime));
            }
            else
            {
                if(shooter != null)
                {
                    if (shooter.activeSelf)
                    {
                        shooter.SetActive(false);
                    }
                }                
                rb.MovePosition(Vector2.MoveTowards(transform.position, T.position, speed * Time.deltaTime));
            }
        }
    }
    void Flip()
    {
        if (flip)
        {
            isFlipped = !isFlipped;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}
