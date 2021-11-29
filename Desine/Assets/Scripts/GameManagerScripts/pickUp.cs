using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class pickUp : MonoBehaviour
{
    public Rigidbody2D rb;
    public RigidbodyType2D fallT;
    public RigidbodyConstraints2D constr;
    public Collider2D coll;
    public LayerMask ground;
    public LayerMask player;
    bool grounded;
    public UnityEvent onPickUp;
    Player p;
    Collider2D[] collsG = new Collider2D[1];
    Collider2D[] collsP = new Collider2D[1];
    ContactFilter2D con;
    bool pickup;
    public GameObject playerG;
    public AudioManager audioM;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (grounded)
        {
            rb.bodyType = fallT;
            coll.isTrigger = true;
            rb.constraints = constr;
        }
        if (pickup)
        {
            onPickUp.Invoke();
        }
    }
    private void FixedUpdate()
    {
        con.SetLayerMask(ground);
        if (!grounded)
        {
            if (Physics2D.OverlapCollider(coll, con, collsG) > 0)
            {
                for (int i = 0; i < collsG.Length; i++)
                {
                    if (collsG[i] != null)
                    {
                        if (!collsG[i].CompareTag("MovingP"))
                        {
                            grounded = true;
                        }
                    }
                }
            }
        }
        con.SetLayerMask(player);
        p = FindObjectOfType<Player>();
        if (!pickup)
        {
            if (Physics2D.OverlapCollider(coll, con, collsP) > 0)
            {
                for (int i = 0; i < collsP.Length; i++)
                {
                    if (collsP[i] != null)
                    {
                        pickup = true;
                    }
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer ==  ground.value)
        {
            grounded = true;
            print("touching G");
        }
        if (collision.collider.gameObject.layer == player.value)
        {
            p = FindObjectOfType<Player>();
            onPickUp.Invoke();
        }
    }
    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
    public void SepPlay(Sound sound)
    {
        playerG.transform.parent = null;
        audioM.Play(sound);
    }
    public void Heal(int amount)
    {
        if(p != null)
        {
            if (p.currentHealth != p.maxHealth)
            {
                if(p.currentHealth + amount >= p.maxHealth)
                {
                    p.currentHealth = p.maxHealth;
                }
                else
                {
                    p.currentHealth += amount;
                }
            }
        }
    }
    public void mapActiveate()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            MapActivator mapActivator = player.GetComponent<MapActivator>();
            if(mapActivator != null)
            {
                mapActivator.activeate = true;
            }
        }  
    }
    public void NParent(Transform child) => child.parent = null;
}
