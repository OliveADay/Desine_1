using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avoidO : MonoBehaviour
{
    public LayerMask Obstacle;
    [HideInInspector]
    public bool avoid;
    public FlyAttack fA;
    public FlyPatrol fP;
    Vector2 origDir;
    Vector2 dir;
    float dirM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fP != null)
        {
            if (fP.p != null)
            {
                origDir = new Vector2(fP.p.transform.position.x - transform.position.x, fP.p.transform.position.y - transform.position.y);
                dir = origDir.normalized;
                dirM = origDir.x / dir.x;
            }
        }
        
    }

    private void FixedUpdate()
    {
        if(fP != null)
        {
            if (fP.p != null)
            {
                avoid = Physics2D.Raycast(transform.position, dir, dirM, Obstacle);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if(fP != null)
        {
            if(fP.p != null)
            {
                Gizmos.DrawLine(transform.position, fP.p.transform.position);
            }
        }
    }
}
