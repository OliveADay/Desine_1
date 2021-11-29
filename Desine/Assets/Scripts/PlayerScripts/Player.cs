using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Animator player_Animator;
    public Rigidbody2D player_rb;
    public PlayerCloseRangeCombat player_Combat;
    public PlayerController player_Movement;
    [HideInInspector]
    public int currentHealth;
    public int maxHealth;
    public static int origMaxHealth;
    float dazedTime;
    public float startDazedTime;
    public float deadDur;
    public static bool isKilled = false;
    public float dazedSpeed = -1;
    [HideInInspector]
    public bool death = false;
    public RigidbodyType2D deathType;
    bool canHurt;
    public AudioManager audioM;
    public Sound theme;
    GameObject hitter;
    int moveleft;
    public Sound Hurt;
    Rigidbody2D rb;
    int through = 0;
    public ParticleSystem bloodP;
    public float timeBefRes;
    public SpriteRenderer p_sp;
    GameObject targetOfTeleportation;
    public Collider2D mainColl;
    public Collider2D secondColl;
    bool inWall = false;
    public LayerMask wallL;
    Collider2D[] grounds;
    float origDazedSpeed;
    public List<GameObject> inventory;

    // Start is called before the first frame update
    void Start()
    {
        targetOfTeleportation = GameObject.Find("eaftos");
        if(targetOfTeleportation != null)
        {
            transform.position = targetOfTeleportation.transform.position;
        }
        audioM.Play(theme);
        isKilled = false;
        if(origMaxHealth <= 0)
        {
            origMaxHealth = maxHealth;
        }
        currentHealth = origMaxHealth;
        rb = player_rb;
        origDazedSpeed = dazedSpeed;
        if(SceneManager.GetActiveScene().buildIndex == PlayerPrefs.GetInt("lastlevel_ind" + SaveManager.saveName))
        {
            print("justLoaded in");
            if(PlayerPrefs.GetInt("PH" + SaveManager.saveName) != 0)
            {
                currentHealth = PlayerPrefs.GetInt("PH" + SaveManager.saveName);
            }
        }
        else
        {
            print(PlayerPrefs.GetInt("lastlevel_ind" + SaveManager.saveName));
        }
    }

    // Update is called once per frame
    void Update()
    {
        SaveManager.SaveInt("PH" + SaveManager.saveName, currentHealth);
        grounds = Physics2D.OverlapCircleAll(transform.position, 0.1f, wallL);
        if (grounds.Length < 1)
        {
            inWall = false;
        }
        else
        {
            inWall = true;
        }
        if (inWall)
        {
            print("inwall");
            secondColl.isTrigger = true;
            mainColl.isTrigger = true;
        }
        else
        {
            secondColl.isTrigger = false;
            mainColl.isTrigger = false;
        }
        if (grounds == null)
        {
            inWall = false;
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            GameObject leveltransitioner = GameObject.FindGameObjectWithTag("LevelTransitioner");
            if(leveltransitioner != null)
            {
                transform.position = leveltransitioner.transform.position;
            }
        }
        if (!death)
        {
            if (dazedTime <= 0)
            {
                canHurt = true;
                moveleft = 0;
                through = 0;
                player_Movement.horizMove = true;
                mainColl.enabled = true;
                secondColl.gameObject.SetActive(false);
            }
            else
            {
                canHurt = false;
                if(through < 1)
                {
                    if (hitter != null)
                    {
                        if (hitter.transform.position.x - transform.position.x < 0)
                        {
                            bool oppose = Physics2D.Raycast(transform.position, Vector2.left, origDazedSpeed * 12, wallL);
                            if (oppose)
                            {
                                moveleft = 0;
                            }
                            else
                            {
                                moveleft = 1;
                            }
                        }
                        else
                        {
                            bool oppose = Physics2D.Raycast(transform.position, Vector2.right, origDazedSpeed * 12, wallL);
                            if (oppose)
                            {
                                moveleft = 0;
                            }
                            else
                            {
                                moveleft = 1;
                            }
                            
                        }
                    }
                }
                through += 1;
                dazedTime -= Time.deltaTime;
                secondColl.gameObject.SetActive(true);
            }
        }
    }
    private void FixedUpdate()
    {
        
        if (moveleft == 2)
        {
            if(dazedSpeed < 0)
            {
                dazedSpeed = -dazedSpeed;
            }
            Debug.Log("move right");
        }
        if(moveleft == 1)
        {
            if(dazedSpeed > 0)
            {
                dazedSpeed = -dazedSpeed;
            }
            print("move left");
        }

        if(moveleft != 0)
        {
            rb.MovePosition(new Vector2(transform.position.x + dazedSpeed, transform.position.y));
        }
    }
    public void TakeDamage(int damageAmount, GameObject enemy, bool sub)
    {
        if (!death)
        {
            if (canHurt)
            {
                hitter = enemy;
                StartCoroutine(BloodP(bloodP));
                audioM.Play(Hurt);
                dazedTime = startDazedTime;
                secondColl.gameObject.SetActive(true);
                mainColl.enabled = false;
                currentHealth -= damageAmount;
                player_Animator.SetTrigger("Hurt");
                if (currentHealth <= 0)
                {
                    mainColl.enabled = false;
                    secondColl.enabled = false;
                    player_rb.bodyType = deathType;
                    StartCoroutine(Killed());
                }
            }
            if (sub)
            {
                origMaxHealth -= damageAmount / 5;
            }
        }
    }
    IEnumerator Wait(bool done, bool b, bool value, float time)
    {
        done = false;
        yield return new WaitForSeconds(time);
        b = value;
        done = true;
    }
    IEnumerator Killed()
    {
        if(isKilled == false)
        {
            death = true;
            player_Animator.SetBool("isDead", true);
            player_Combat.enabled = false;
            player_Movement.enabled = false;
            yield return new WaitForSeconds(deadDur);
            p_sp.enabled = false;
            yield return new WaitForSeconds(timeBefRes);
            isKilled = true;
            Destroy(gameObject);
        }
    }
    IEnumerator BloodP(ParticleSystem par)
    {
        GameObject p = Instantiate(par.gameObject, transform.position, transform.rotation);
        yield return null;
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
