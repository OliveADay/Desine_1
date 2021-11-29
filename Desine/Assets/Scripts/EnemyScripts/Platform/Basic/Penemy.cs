using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penemy : Entity
{
    public float Dazedspeed;
    bool pushBack;
    float startDazed;
    public BasePatrol movementM;
    float startS;
    // Start is called before the first frame update
    void Start()
    {
        startDazed = Dazedspeed;
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Dazed();
        }
    }
    public override void Dazed()
    {
        if(dazedTime <= 0)
        {
            pushBack = false;
        }
        else
        {
            pushBack = true;
            dazedTime -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        startS = movementM.speed;
        if (pushBack)
        {
            if (transform.localScale.x < 0)
            {
                dazedTime = -dazedTime;
            }
            else if(startDazed != Dazedspeed)
            {
                Dazedspeed = startDazed;
            }
            rb.AddForce(new Vector2(Dazedspeed, 0));
        }
        else
        {
            movementM.speed = startS;
            rb.AddForce(new Vector2(0, 0));
        }
    }
}
