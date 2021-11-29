using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class autoSave : MonoBehaviour
{ 
    public UnityEvent whenGround;
    PlayerController pcon;
    private void Start()
    {
        pcon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        if(pcon != null)
        {
            if (pcon.isGroundedW)
            {
                whenGround.Invoke();
            }
        }
    }
}
