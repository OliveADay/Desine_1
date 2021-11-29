using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAttack : MonoBehaviour
{
    public float checkR;
    public int damage;
    public LayerMask checkL;
    public FlyPatrol fM;
    bool attack;
    bool attackOrig;
    bool done = true;
    public avoidO avO;
    public Collider2D coll;
    ContactFilter2D con;
    Collider2D[] res = new Collider2D[1];
    bool Attanim;
    public Animator anim;
    public string attanim_n;
    public bool runAnim;
    public string runAnimN;
    bool pastAttack;
    public AudioManager audioM;
    public Sound activeate;
    bool hasOrigAttackTrue;
    public void Start()
    {
        con.SetLayerMask(checkL);
    }
    private void FixedUpdate()
    {
        attackOrig = Physics2D.OverlapCircle(transform.position, checkR, checkL);
        if (Physics2D.OverlapCollider(coll, con, res) > 0)
        {
            Attanim = true;
        }
        else
        {
            Attanim = false;
        }
    }
    public void Hit()
    {
        if (Physics2D.OverlapCollider(coll, con, res) > 0)
        {
            for (int i = 0; i < res.Length; i++)
            {
                if(res[i] != null)
                {
                    Player p = res[i].GetComponent<Player>();
                    if(p != null)
                    {
                        p.TakeDamage(damage, gameObject, false);
                    }
                }
            }
        }
    }
    private void Update()
    {
        if (attackOrig)
        {
            attack = attackOrig;
            hasOrigAttackTrue = true;
            if (runAnim)
            {

            }
        }
        else if(hasOrigAttackTrue)
        {
            if (done)
            {
                StartCoroutine(Wait(3));
            }
        }
        if(pastAttack != attack)
        {
            audioM.Play(activeate);
        }
        if (!avO.avoid)
        {
            fM.attacking = attack;
        }
        if (Attanim)
        {
            fM.enabled = false;
            anim.SetTrigger(attanim_n);
        }
        pastAttack = attack;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, checkR);
    }
    IEnumerator Wait(float time)
    {
        done = false;
        yield return new WaitForSeconds(time);
        attack = attackOrig;
        done = true;
    }
}
