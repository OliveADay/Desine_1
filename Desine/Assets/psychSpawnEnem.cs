using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psychSpawnEnem : Entity
{
    public override void TakeDamage(float damagedAmount, GameObject attacker)
    {
        if (!isDead)
        {
            SpawnParticle(true, Death_P, false, true);
            isDead = true;
            Destroy(gameObject);
        }
    }
}
