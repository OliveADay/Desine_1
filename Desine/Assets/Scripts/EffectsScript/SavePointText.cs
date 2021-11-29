using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointText : MonoBehaviour
{
    public GameObject savePoint;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = savePoint.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
