using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Cronstrum : Entity
{
    public Material hitMat;
    public Material regMat;
    bool active;
    public float projSpeed;
    public GameObject projectile;
    GameObject player;
    bool flip = false;
    GameObject proj_inst;
    public Transform wallPos;
    public LayerMask wallL;
    public float wallR;
    [HideInInspector]
    public bool wallCheck;
    List<GameObject> Drops = new List<GameObject>();
    [HideInInspector]
    public bool secondPhase;
    public Material secondPhaseM;
    public GameObject healthbar_c;
    public Slider healthbar;
    [HideInInspector]
    public bool charge;
    public float speed;
    public UnityEvent startActive;
    public UnityEvent dead;
    int once;
    public UnityEvent secondPhaseEv;
    public Transform handPos;
    public RigidbodyType2D sleepType;
    public RigidbodyType2D origtype;
    public MonoBehaviour[] noneSleepScript;
    [HideInInspector]
    public bool turn = true;
    public override void TakeDamage(float damagedAmount, GameObject attacker)
    {
        if (!active)
        {
            active = true;
            startActive.Invoke();
        }
        sprite.material = hitMat;
        hitter = attacker;
        dazedTime = startDazedTime;
        if (bloodP != null)
        {
            StartCoroutine(SpawnParticle(false, bloodP, true, true));
        }
        currentHealth -= damagedAmount;
        if (currentHealth <= 0)
        {
            StartCoroutine(Dead());
        }
    }
    public override IEnumerator Dead()
    {
        active = false;
        rb.bodyType = deadType;
        if(entityCollider != null)
        {
            entityCollider.enabled = false;
        }
        StartCoroutine(SpawnParticle(true, Death_P, false, false));
        yield return new WaitForSeconds(evaporateLength);
        SaveManager.SaveInt(gameObject.name + SaveManager.saveName, 1);
        for (int i = 0; i < Drops.ToArray().Length; i++)
        {
            if(Drops.ToArray()[i] != null)
            {
                Drops.ToArray()[i].transform.parent = null;
                Drops.ToArray()[i].SetActive(true);
            }
        }
        dead.Invoke();
        Destroy(gameObject);
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = MaxHealth;
        if(PlayerPrefs.GetInt(gameObject.name + SaveManager.saveName) == 1)
        {
            gameObject.SetActive(false);
        }
        if(drops != null)
        {
            for (int i = 0; i < drops.Length; i++)
            {
                if (drops[i] != null)
                {
                    GameObject drop = Instantiate(drops[i], transform);
                    if (drop)
                    {
                        drop.transform.localScale = new Vector2(drop.transform.localScale.x / transform.localScale.x, drop.transform.localScale.y / transform.localScale.y);
                        if (!Drops.Contains(drop))
                        {
                            Drops.Add(drop);
                        }
                        drop.SetActive(false);
                        print(drop.name);
                    }
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if(wallPos != null)
        {
            wallCheck = Physics2D.OverlapCircle(wallPos.position, wallR, wallL);
        }
        if (charge)
        {
            if (transform.localScale.x > 0)
            {
                rb.MovePosition(transform.position + new Vector3(1, 0, 0) * speed);
            }
            else if (transform.localScale.x < 0)
            {
                rb.MovePosition(transform.position + new Vector3(-1, 0, 0) * speed);
            }
        }
    }
    public void Update()
    {
        if (active)
        {
            if (turn)
            {
                if (transform.position.x - player.transform.position.x < 0)
                {
                    if (flip)
                    {
                        Flip();
                    }
                }
                else if (transform.position.x - player.transform.position.x > 0)
                {
                    if (!flip)
                    {
                        Flip();
                    }
                }
            }           
            entityAnimator.SetTrigger(hurtAnimation);
            healthbar_c.SetActive(true);
            healthbar.value = currentHealth / MaxHealth;
            for (int i = 0; i < noneSleepScript.Length; i++)
            {
                noneSleepScript[i].enabled = true;
            }
            rb.bodyType = origtype;
        }
        else
        {
            for (int i = 0; i < noneSleepScript.Length; i++)
            {
                noneSleepScript[i].enabled = false;
            }
            rb.bodyType = sleepType;
        }
        Dazed();
        if (currentHealth <= MaxHealth / 2)
        {
            if (once == 0)
            {
                once = 1;
                secondPhaseEv.Invoke();
            }
            secondPhase = true;
            if(sprite.material == hitMat)
            {
                sprite.material = secondPhaseM;
            }
        }
    }
    public override void Dazed()
    {
        if(dazedTime <= 0)
        {
            if (secondPhase)
            {
                sprite.material = secondPhaseM;
            }
            else
            {
                if(regMat != null)
                {
                    sprite.material = regMat;
                }
            }
        }
        else
        {
            dazedTime -= Time.deltaTime;
        }
    }
    public void spawnProjectile(float time)
    {
        StartCoroutine(spawnProjectileC(time));
    }
    IEnumerator spawnProjectileC(float time)
    {
        yield return new WaitForSeconds(time);
        if (!flip)
        {
            if (projectile != null)
            {
                proj_inst = Instantiate(projectile, handPos.position, Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Debug.LogError("projectile is null");
            }
        }
        else
        {
            if(projectile != null)
            {
                proj_inst = Instantiate(projectile, handPos.position, Quaternion.Euler(0, 0, 180));

            }            
        }
        if (projectile != null)
        {
            Projectile proj_s = proj_inst.GetComponent<Projectile>();
            if (proj_s != null)
            {
                if (secondPhase)
                {
                    proj_s.speed = projSpeed * 2;
                }
                else
                {
                    proj_s.speed = projSpeed;
                }
                if (this.transform.localScale.x < 0)
                {
                    proj_s.main.x = -1;
                }
                else if (this.transform.localScale.x > 0)
                {
                    proj_s.main.x = 1;
                }
            }
        }
    }
    public void Flip()
    {
        flip = !flip;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
    private void OnDrawGizmosSelected()
    {
        if(wallPos != null)
        {
            Gizmos.DrawWireSphere(wallPos.position, wallR);
        }       
    }
}
