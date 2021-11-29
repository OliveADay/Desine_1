using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderS : MonoBehaviour
{
    public string saveName;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetFloat(saveName) >= 0.1f)
        {
            slider.value = PlayerPrefs.GetFloat(saveName) - 0.1f;
        }
    }
}
