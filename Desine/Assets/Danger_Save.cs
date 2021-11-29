using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger_Save : MonoBehaviour
{
    public LayerMask player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == player)
        {
            SaveManager.SaveInt(gameObject.name + SaveManager.saveName, 1);
        }
    }
}
