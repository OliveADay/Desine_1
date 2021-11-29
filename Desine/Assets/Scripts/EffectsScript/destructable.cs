using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructable : MonoBehaviour
{
    public GameObject destructAB;
    public destructable[] des;
    private void Start()
    {
        destructAB.transform.rotation = transform.rotation;
    }
    public void destruct(bool value)
    {
        destructAB.transform.parent = null;
        destructAB.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.x, transform.localScale.x);
        destructAB.SetActive(true);
        if (value)
        {
            if (des != null)
            {
                for (int i = 0; i < des.Length; i++)
                {
                    if (des[i] != null)
                    {
                        des[i].destruct(false);
                    }
                }
            }
        }
        gameObject.SetActive(false);
    }
    public void ChangeR(RigidbodyType2D[] r)
    {

    }
}
