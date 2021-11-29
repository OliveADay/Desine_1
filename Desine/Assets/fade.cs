using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class fade : MonoBehaviour
{
    public Image img;
    Color c;
    float rawA;
    int intA;
    public float aMultiplier;
    public UnityEvent whenFin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (img.color.a < 1)
        {
            rawA += Time.deltaTime;
            c = new Color(0, 0, 0, rawA * aMultiplier);
            img.color = c;
        }
        else
        {
            print("Fin");
            whenFin.Invoke();
        }
    }
}
