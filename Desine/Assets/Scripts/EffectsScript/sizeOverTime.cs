using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sizeOverTime : MonoBehaviour
{
    public float rate;
    public LayerMask layers;
    bool touchedL;
    Collider2D[] cons = new Collider2D[5];
    ContactFilter2D tC;
    public Collider2D Con;
    public Rigidbody2D rb;
    public RigidbodyType2D t;
    bool ground;
    public RigidbodyConstraints2D constr;
    // Start is called before the first frame update
    void Start()
    {
        tC.SetLayerMask(layers);
    }

    // Update is called once per frame
    void Update()
    {
        if (touchedL)
        {
            transform.localScale -= (transform.localScale / 0.5f) * Time.deltaTime;
        }
        if(transform.localScale.x <= 0.5)
        {
            if(rb != null)
            {
                if (ground)
                {
                    rb.bodyType = t;
                    rb.constraints = constr;
                }
            }
            touchedL = false;
        }
    }
    private void FixedUpdate()
    {
        if(Con != null)
        {
            if (Physics2D.OverlapCollider(Con, tC, cons) != 0)
            {
                if(transform.localScale.x > 0.5)
                {
                    touchedL = true;
                }
                ground = true;
            }
            else
            {
                ground = false;
            }
        } 
    }
}
