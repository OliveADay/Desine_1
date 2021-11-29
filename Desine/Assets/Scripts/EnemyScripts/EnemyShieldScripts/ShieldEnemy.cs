using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ShieldEnemy : Entity
{

    public ShieldPatrol enemyMovement;
    float startspeed;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        startspeed = enemyMovement.speed;
    }

    void Update()
    {
        Dazed();
    }
    // Update is called once per frame
    public override void Dazed()
    {
        if (isDead == false)
        {
            if (dazedTime <= 0)
            {
                if (enemyMovement.isFlipped)
                {
                    enemyMovement.speed = -startspeed;
                }
                else
                {
                    enemyMovement.speed = startspeed;
                }
            }
            else
            {
                if (hitter.transform.localScale.z >= 0)
                {
                    enemyMovement.speed = startspeed * dazedTime *5;
                }
                else
                {
                    enemyMovement.speed = -startspeed * dazedTime * 5;
                }
                dazedTime -= Time.deltaTime;
            }
        }
    }
}
