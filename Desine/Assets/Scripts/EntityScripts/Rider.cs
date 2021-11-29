using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rider : MonoBehaviour
{
    public string pName;
    bool origRiding = false;
    bool Riding = false;
    Collider2D ride;
    public Rigidbody2D rb;
    public Transform rideP;
    public float rideR;
    public LayerMask rideL;
    public PlayerController pCon;
    bool finalR;
    float origS;
    public float rSpeed;
    Vector3 rVel;
    Vector3 lastRP;
    float origRS;

    private void Start()
    {
        origRS = rSpeed;
    }
    private void FixedUpdate()
    {
        origRiding = Physics2D.OverlapCircle(rideP.position, rideR, rideL);
        ride = Physics2D.OverlapCircle(rideP.position, rideR, rideL);
        if (finalR)
        {
            if (ride != null)
            {
                rb.MovePosition(new Vector2(ride.transform.parent.position.x, transform.position.y));
            }
        }
        if(lastRP != null && ride != null)
        {
            rVel = ride.transform.parent.position - lastRP;
        }
        if (ride != null)
        {
            lastRP = ride.transform.parent.position;
        }
        
    }
    private void Update()
    {
        if (origRiding)
        {
            if(ride != null)
            {
                if (ride.CompareTag(pName))
                {
                    Riding = true;
                }
                else
                {
                    Riding = false;
                }
            }
        }
        else
        {
            Riding = false;
        }
        finalR = Riding;
        if (pCon.moving)
        {
            finalR = false;
        }
        if(!finalR && Riding)
        {
            if(pCon.speed != rSpeed)
            {
                origS = pCon.speed;
            }
            if((rSpeed - rVel.magnitude) < 0.5f)
            {
                rSpeed += 0.4f;
            }                
            pCon.speed = rSpeed;
        }
        else
        {
            if(pCon.speed == rSpeed)
            {
                pCon.speed = origS;
            }
            if (rSpeed != origRS)
            {
                rSpeed = origRS;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(rideP.position, rideR);
    }
}
