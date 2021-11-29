using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class waitMono : MonoBehaviour
{
    public float time;
    public UnityEvent whenTimeUp;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(time);
        whenTimeUp.Invoke();
    }
}
