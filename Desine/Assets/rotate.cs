using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    Quaternion inistialRot;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        inistialRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, transform.rotation.z + (speed * Time.deltaTime)));
    }
}
