using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public string unHideAnim;
    public string hideAnim;
    public Animator anim;
    bool pIn1;
    public float pIn1R;
    public LayerMask pIn1L;
    bool inUnhide;
    bool done = true;
    public GameObject bow;
    public BoxCollider2D coll;
    [HideInInspector]
    public GameObject p;
    shooter s;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (done)
        {
            StartCoroutine(Wait(3));
        }
    }
    IEnumerator Wait(float time)
    {
        done = false;
        yield return new WaitForSeconds(time);
        if (pIn1)
        {
            if (!inUnhide)
            {
                inUnhide = true;
                anim.SetTrigger(unHideAnim);
            }
        }
        else
        {
            if (inUnhide)
            {
                inUnhide = false;
                anim.SetTrigger(hideAnim);
            }
        }
        done = true;
    }
    private void FixedUpdate()
    {
        pIn1 = Physics2D.OverlapCircle(transform.position, pIn1R, pIn1L);
        Collider2D pColl = Physics2D.OverlapCircle(transform.position, pIn1R, pIn1L);
        if(pColl != null)
        {
            p = pColl.gameObject;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, pIn1R);
    }
    public void unhide()
    {
        bow.SetActive(true);
        s = bow.GetComponent<shooter>();
        s.target = p;
        coll.enabled = true;
    }
    public void hide()
    {
        s.done = true;
        bow.SetActive(false);
        coll.enabled = false;
    }
    public void flip()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
}
