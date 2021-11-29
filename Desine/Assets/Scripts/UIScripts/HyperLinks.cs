using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperLinks : MonoBehaviour
{
    public static void LinkS(string path)
    {
        Application.OpenURL(path);
    }
    public void Link(string path)
    {
        HyperLinks.LinkS(path);
    }
}
