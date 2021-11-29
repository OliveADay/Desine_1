using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtOnImpact : MonoBehaviour
{
    public int damage;
    public LayerMask pLayer;
    ContactFilter2D pConF;
    public Collider2D coll;
    Collider2D[] colls = new Collider2D[2];
    Player coll_en;
    bool done = true;
    public bool ChangeTTrig;
    public LayerMask pL;
    public float pR;
    bool pCheck;
    public bool sub;
    

    private void Start()
    {
        pConF.SetLayerMask(pLayer);
    }

    private void Update()
    {
        if(Physics2D.OverlapCollider(coll, pConF, colls) > 0)
        {
            foreach(Collider2D coll in colls)
            {
                if(coll_en == null)
                {
                    coll_en = coll.transform.gameObject.GetComponent<Player>();
                }
                if(coll_en != null)
                {
                    if (done)
                    {
                        StartCoroutine(Wait());
                    }
                }
            }
        }
        if (pCheck)
        {
            coll.isTrigger = true;
        }
        else
        {
            coll.isTrigger = false;
        }
    }
    private void FixedUpdate()
    {
        if (ChangeTTrig)
        {
            pCheck = Physics2D.OverlapCircle(transform.position, pR, pL);
        }
    }
    IEnumerator Wait()
    {
        done = false;
        coll_en.TakeDamage(damage, this.gameObject, sub);
        yield return new WaitForSeconds(2);
        done = true;
    }
    private void OnDrawGizmosSelected()
    {
        if (ChangeTTrig)
        {
            Gizmos.DrawWireSphere(transform.position, pR);
        }
    }

}
