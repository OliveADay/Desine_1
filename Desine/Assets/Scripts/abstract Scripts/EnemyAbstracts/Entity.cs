using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class Entity : MonoBehaviour
{
    public BoxCollider2D entityCollider;
    public Animator entityAnimator;
    public float MaxHealth = 100;
    [HideInInspector]
    public float currentHealth;
    public float startDazedTime;
    [HideInInspector]
    public float dazedTime;
    public Rigidbody2D rb;
    public string hurtAnimation;
    public string deadAnimation;
    [HideInInspector]
    public bool isDead;
    public MonoBehaviour[] scripts;
    public float deathGravity;
    public float waitTime;
    public SpriteRenderer sprite;
    public string evaporateAnimation;
    public float evaporateWaitTime;
    public float evaporateLength;
    public RigidbodyType2D deadType;
    public RigidbodyType2D evaporateType;
    public bool useRb = true;
    public bool collearly = false;
    public ParticleSystem bloodP;
    public ParticleSystem Death_P;
    [HideInInspector]
    public GameObject hitter;
    public float deathWT;
    public GameObject[] drops;
    public bool noEvapAnim;
    
    public virtual void TakeDamage(float damagedAmount, GameObject attacker)
    {
        if(isDead == false)
        {
            hitter = attacker;
            dazedTime = startDazedTime;
            if (bloodP != null)
            {
                StartCoroutine(SpawnParticle(false, bloodP, true, true));
            }
            currentHealth -= damagedAmount;
            entityAnimator.SetTrigger(hurtAnimation);
            if (currentHealth <= 0)
            {
                StartCoroutine(Dead());
            }
        }   
    }
    public virtual IEnumerator Dead()
    {
        isDead = true;
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
        foreach(MonoBehaviour script in scripts)
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
            if(drops[i] != null)
            {
                drops[i].SetActive(true);
                drops[i].transform.parent = null;
                drops[i].transform.localScale = new Vector2(drops[i].transform.localScale.x * transform.localScale.x, drops[i].transform.localScale.y * transform.localScale.y);
            }
        }
        yield return new WaitForSeconds(deathWT);
        Destroy(gameObject);
        
    }
    public virtual void Dazed()
    {

    }
    public IEnumerator SpawnParticle(bool becomeInd, ParticleSystem par, bool dir, bool del)
    {
        if(par != null)
        {
            GameObject p = Instantiate(par.gameObject, gameObject.transform);
            if (dir)
            {
                p.transform.localScale = new Vector3(hitter.transform.localScale.x / 2, p.transform.localScale.y, p.transform.localScale.z);
            }
            if (becomeInd)
            {
                p.transform.parent = null;
            }
            if (!dir)
            {
                p.transform.localScale = par.transform.localScale * 2;
            }
            yield return new WaitForSeconds(bloodP.main.duration + 0.2f);
            if (del)
            {
                Destroy(p);
            }
        }
    }
}