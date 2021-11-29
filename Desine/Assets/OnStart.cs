using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnStart : MonoBehaviour
{
    public UnityEvent whenStart;
    // Start is called before the first frame update
    void Start()
    {
        whenStart.Invoke();
    }
}
