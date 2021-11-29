using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class shooter : MonoBehaviour
{
    [HideInInspector]
    public GameObject target;
    public GameObject ammo;
    public UnityEvent flip;
    [HideInInspector]
    public bool done = true;
    float zRot;
    public Animator anim;
    public float speed;
    float offset = 180;
    Vector3 difference;
    public ParticleSystem bowP;

    // Start is called before the first frame update
    void Start()
    {
        done = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            difference = target.transform.position - transform.position;
            zRot = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRot + 90));
        }
        if (done)
        {
            StartCoroutine(shootanim(2));
        }
    }
    IEnumerator shootanim(float time)
    {
        done = false;
        yield return new WaitForSeconds(time);
        anim.SetTrigger("Attack");
        done = true;
    }
    public void shoot()
    {
        GameObject shot = Instantiate(ammo, transform.position, Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, zRot + offset)));
        Projectile p = shot.GetComponent<Projectile>();
        p.main = difference;
        p.speed = speed;
        StartCoroutine(SpawnParticle(bowP, gameObject));
    }
    IEnumerator SpawnParticle(ParticleSystem pOrg, GameObject g)
    {
        GameObject p = Instantiate(pOrg.gameObject, g.transform);
        yield return new WaitForSeconds(pOrg.main.duration);
        Destroy(p);
    }
    
}
