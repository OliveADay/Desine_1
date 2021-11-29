using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blocker : MonoBehaviour
{
    public BoxCollider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CollEnable() => coll.enabled = true;
    public void collDisable() => coll.enabled = false;
    public void Disable() => gameObject.SetActive(false);
}
