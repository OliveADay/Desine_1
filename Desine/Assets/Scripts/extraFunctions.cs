using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class extraFunctions : MonoBehaviour
{
    public Collider2D target;
    public float speed;
    public Rigidbody2D rb;
    public RigidbodyType2D rbType;
    bool moveTowards;
    [HideInInspector]
    public bool finished;
    public UnityEvent whenFin;
    bool flipped = false;
    public bool facingR;
    bool once = true;
    public UnityEvent whenStart;
    bool firstf = true;
    int i = 0;
    Vector3 targetLastPos;
    public Transform groundCheckPos;
    public float groundCheckRaduis;
    public LayerMask groundCheckLayer;
    bool groundCheck;
    public Animator animator;
    public UnityEvent outOfBonds;
    bool externalflip = false;
    bool once1 = false;

    private void Start()
    {
        if(transform.localScale.x > 0)
        {
            if (facingR)
            {
                flipped = true;
            }
            else
            {
                flipped = false;
            }
        }
        else if(transform.localScale.x < 0)
        {
            if (facingR)
            {
                flipped = false;
            }
            else
            {
                flipped = true;
            }
        }
        if(target != null)
        {
            targetLastPos = target.transform.position;
        }        
    }
    public void IndirectActive(GameObject g)
    {
        g.SetActive(true);
    }
    public void Indirectdeactive(GameObject g)
    {
        g.SetActive(false);
    }
    public void MoveTowards()
    {
        Debug.Log(gameObject.name + " is moving towards " + target.gameObject);
        moveTowards = true;
    }
    public void changeTarget(Collider2D coll)
    {
        target = coll;
    }
    public void changeSpeed(float speedN)
    {
        speed = speedN;
    }
    public void SecondTimeIndirectActive(GameObject g)
    {
        if(i == 2)
        {
            IndirectActive(g);
        }
    }
    public void SecondTimeDestructRb(Destruct_rb destruct)
    {
        if(i == 2)
        {
            destruct.b = true;
        }
    }
    public void SecondTimeChangeRbBody()
    {
        if(rb != null)
        {
            if(i == 2)
            {
                rb.bodyType = rbType;
            }
        }
    }
    public void SecondTimeSetTrigger(string name)
    {
        if(animator != null)
        {
            if(i == 2)
            {
                animator.SetTrigger(name);
            }
        }
    }
    public void SecondTimeFlip()
    {
        if(i == 2)
        {            
            externalflip = true;
        }
    }

    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    private void FixedUpdate()
    {
        if(groundCheckPos != null)
        {
            groundCheck = Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRaduis, groundCheckLayer);
        }
        if (moveTowards)
        {
            if (firstf)
            {
                whenStart.Invoke();
            }
            if (target != null)
            {
                if (!externalflip)
                {
                    if (transform.position.x - target.gameObject.transform.position.x < 0)
                    {
                        if (facingR)
                        {
                            if (!flipped)
                            {
                                flip();
                            }
                        }
                        else
                        {
                            if (flipped)
                            {
                                flip();
                            }
                        }

                    }
                    else if (transform.position.x - target.gameObject.transform.position.x > 0)
                    {
                        if (facingR)
                        {
                            if (flipped)
                            {
                                flip();
                            }
                        }
                        else
                        {
                            if (!flipped)
                            {
                                flip();
                            }
                        }
                    }
                }
                else
                {
                    if (!once1)
                    {
                        flip();
                    }
                    once1 = true;
                }
                if (rb != null)
                {
                    if(target.transform.position.x != transform.position.x)
                    {
                        if (groundCheck)
                        {
                            print(gameObject.name + "moving");
                            rb.MovePosition(Vector3.MoveTowards(transform.position, target.gameObject.transform.position, speed));
                        }
                    }
                    else
                    {
                        finished = true;                      
                    }
                }
                else
                {
                    Debug.LogError(gameObject.name + "'s rigidbody is null");
                }
            }
            else
            {
                Debug.LogError(gameObject.name + "'s target is null");
            }
            firstf = false;
        }
    }
    private void Update()
    {
        if (transform.localScale.x > 0)
        {
            flipped = true;
        }
        else if (transform.localScale.x < 0)
        {
            flipped = false;
        }
        if(target != null)
        {
            if (target.transform.position != targetLastPos)
            {
                once = true;
                finished = false;
                firstf = true;
                i = 2;
            }
        }
        if (finished)
        {
            if (once)
            {
                whenFin.Invoke();
                once = false;
                
            }
        }
        if(target != null)
        {
            targetLastPos = target.transform.position;
        }
        if(transform.position.y <= -55)
        {
            outOfBonds.Invoke();
        }
    }
    public void flip()
    {
        flipped = !flipped;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    private void OnDrawGizmosSelected()
    {
        if(groundCheckPos != null)
        {
            Gizmos.DrawWireSphere(groundCheckPos.position, groundCheckRaduis);
        }
    }
}
