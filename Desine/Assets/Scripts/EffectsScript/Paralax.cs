using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    Transform cameraTrans;
    Vector3 lastPos;
    public float parallaxMultipler;
    public bool yMovement;
    // Start is called before the first frame update
    void Start()
    {
        cameraTrans = Camera.main.transform;
        lastPos = cameraTrans.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 movement = cameraTrans.position - lastPos;
        if (yMovement)
        {
            transform.position += new Vector3(movement.x * 1 / parallaxMultipler, movement.y, 0);
        }
        else
        {
            transform.position += new Vector3(movement.x * 1 / parallaxMultipler, 0, 0);
        }
        lastPos = cameraTrans.position;
    }
}
