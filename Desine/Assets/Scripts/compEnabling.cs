using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compEnabling : MonoBehaviour
{
    public GameObject signal;
    public GameObject reciver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (signal != null)
        {
            reciver.SetActive(signal.activeSelf);
        }
    }
}
