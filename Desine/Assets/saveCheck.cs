using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class saveCheck : MonoBehaviour
{
    public string save;
    public UnityEvent whenTrue;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt(save + SaveManager.saveName) > 0)
        {
            whenTrue.Invoke();
        }
    }
    public void Destroy(GameObject g) => Destroy(g);
}
