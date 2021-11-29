using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public GameObject thisGameobject;
    public Text text;
    string area = "Abandoned Mines";
    int origFSize;
    string origtxt;
    float origLine;
    public int fontChange;
    public float lineSpChange;
    Vector3 origsize;
    public Vector3 changeSize;
    // Start is called before the first frame update
    void Start()
    {
        origFSize = text.fontSize;
        origtxt = text.text;
        origLine = text.lineSpacing;
        origsize = this.gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt(thisGameobject.name) == 1)
        {
            this.gameObject.transform.localScale = changeSize;
            text.lineSpacing = lineSpChange;
            text.fontSize = fontChange;
            text.text = "Continue from " + area;
        }
        else
        {
            this.gameObject.transform.localScale = origsize;
            text.lineSpacing = origLine;
            text.fontSize = origFSize;
            text.text = origtxt;
        }
    }
}
