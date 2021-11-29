using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPEnemy : Entity
{
    public FlyPatrol movementF;
    float startSpeed;
    public float DazedSpeed;
    public Material normMat;
    public Material hitMat;
    public GameObject shooter;
    private void Start()
    {
        currentHealth = MaxHealth;
        startSpeed = movementF.speed;    
    }
    private void Update()
    {
        Dazed();
    }
    public override IEnumerator Dead()
    {
        isDead = true;
        if(shooter != null)
        {
            shooter.SetActive(false);
        }
        entityAnimator.SetBool(deadAnimation, true);
        if (collearly)
        {
            entityCollider.enabled = false;
        }
        if (useRb)
        {
            if (!collearly)
            {
                rb.bodyType = deadType;
            }
        }
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
        if (useRb)
        {
            rb.gravityScale = deathGravity;
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        yield return new WaitForSeconds(evaporateWaitTime);
        if (noEvapAnim)
        {
            entityAnimator.SetTrigger(evaporateAnimation);
        }
        StartCoroutine(SpawnParticle(true, Death_P, false, false));
        if (useRb)
        {
            if (!collearly)
            {
                rb.bodyType = evaporateType;
            }
        }
        yield return new WaitForSeconds(evaporateLength);
        entityCollider.enabled = false;
        sprite.enabled = false;
        for (int i = 0; i < drops.Length; i++)
        {
            if (drops[i] != null)
            {
                drops[i].SetActive(true);
                drops[i].transform.parent = null;
                drops[i].transform.localScale = new Vector2(drops[i].transform.localScale.x * transform.localScale.x, drops[i].transform.localScale.y * transform.localScale.y);
            }
        }
        yield return new WaitForSeconds(deathWT);
        Destroy(gameObject);

    }
    public override void Dazed()
    {
        if (!isDead)
        {
            if (dazedTime <= 0)
            {
                movementF.enabled = true;
                if(normMat != null)
                {
                    sprite.material = normMat;
                }
            }
            else
            {
                movementF.enabled = false;
                rb.MovePosition(new Vector2(transform.position.x + (DazedSpeed * transform.localScale.x / 2) * Time.deltaTime, transform.position.y));
                dazedTime -= Time.deltaTime;
                if(hitMat != null)
                {
                    sprite.material = hitMat;
                }
            }
        }
    }
}
