using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleJump_P : MonoBehaviour
{
    PlayerController pCon;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            pCon = player.GetComponent<PlayerController>();
        }
        if (PlayerPrefs.GetInt("doubleJump" + SaveManager.saveName) == 1)
        {
            if(pCon != null)
            {
                if (pCon.extraJumpsValue == 0)
                {
                    AddDoubleJump();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddDoubleJump()
    {
        if(pCon != null)
        {
            pCon.extraJumpsValue = 1;
            pCon.extraJumps = 1;
            SaveManager.SaveInt("doubleJump" + SaveManager.saveName, 1);
        }
    }
}
