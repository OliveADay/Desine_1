using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeShaderC : MonoBehaviour
{
    public Material material;
    Color color; 
    bool done = true;
    float[] colVal = new float[3];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (done)
        {
            StartCoroutine(Wait(0.3f));
        }
    }
    IEnumerator Wait(float time)
    {
        done = false;
        yield return new WaitForSeconds(time);        
        colVal[0] = Random.Range(0f,1f);
        colVal[1] = Random.Range(0f,1f);
        colVal[2] = Random.Range(0f,1f);
        color = new Color(colVal[0], colVal[1], colVal[2]);
        material.SetColor("c_Glow", color);
        print("change Color");
        done = true;
    }
}
