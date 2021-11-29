using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_afterTime : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
