using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frictionChanger : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool equalize;
    float totalF;
    float regColF;
    float trigColF;
    float startfric;
    public bool fricOnL;
    public LayerMask layers;
    public float rate;
    bool layerC;

    private void Start()
    {
        if (equalize)
        {
            if (rb != null)
            {
                if (rb.sharedMaterial != null)
                {
                    startfric = rb.sharedMaterial.friction;
                }
            }
        }
    }
    private void Update()
    {
        if (equalize)
        {
            totalF = startfric + trigColF + regColF;
            if (totalF != startfric)
            {
                float fric = startfric - (trigColF + regColF);
                rb.sharedMaterial.friction = fric;
            }
        }
        if (layerC)
        {
            rb.sharedMaterial.friction += rate / 10 * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (equalize)
        {
            Rigidbody2D colrb = collision.collider.gameObject.GetComponent<Rigidbody2D>();
            if(colrb != null)
            {
                if(colrb.sharedMaterial != null)
                {
                    colrb.sharedMaterial.friction = regColF;
                }
            }
        }
        if (fricOnL)
        {
            if(collision.collider.gameObject.layer == layers)
            {
                layerC = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (equalize)
        {
            Rigidbody2D colrb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (colrb != null)
            {
                if (colrb.sharedMaterial != null)
                {
                    colrb.sharedMaterial.friction = trigColF;
                }
            }
        }
    }
    private void OnDisable()
    {
        if(rb != null)
        {
            rb.sharedMaterial.friction = startfric;
        }
    }
}
