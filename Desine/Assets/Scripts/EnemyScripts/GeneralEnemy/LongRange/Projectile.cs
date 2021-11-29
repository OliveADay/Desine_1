using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public float gravityamount;
    public float gravMax;
    public float speed;
    public bool gravity;
    public int p = 11;
    public int g = 8;
    public int n = 10;
    public Vector3 main;
    public ParticleSystem destroyEff;
    public AudioManager audioM;
    public Sound Ondestroy;
    public Sound onS;
    public bool toP;
    GameObject player;
    public bool follow;
    float zRot;
    GameObject target;
    Vector3 difference;
    // Start is called before the first frame update
    void Start()
    {
        if(audioM != null && onS != null)
        {
            audioM.Play(onS);
        }
        if (toP)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (follow)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            difference = target.transform.position - transform.position;
            zRot = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRot));
        }
    }
    private void FixedUpdate()
    {
        if (toP)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position, speed));
        }
        else
        {
            rb.MovePosition(transform.position + main * speed * Time.deltaTime);
        }
        if (gravity)
        {
            rb.AddForce(new Vector2(0, gravityamount));
            gravityamount += (gravityamount / 10);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == p)
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(20, this.gameObject, false);
            Destroy(gameObject);
        }
        if(collision.gameObject.layer == g)
        {
            if(collision.transform.parent.gameObject.layer != n)
            {
                audioM.Play(Ondestroy);
                Destroy(Instantiate(destroyEff.gameObject, transform.position, transform.localRotation), destroyEff.main.duration);
                Destroy(gameObject);
            }
        }
    }
}
