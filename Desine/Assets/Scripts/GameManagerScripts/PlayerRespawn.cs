using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.isKilled)
        {
            Respawn();
        }
    }
    void Respawn()
    {
        string level = "Intro";
        if (PlayerPrefs.GetInt(SaveManager.saveName) == 1)
        {
            PlayerDataConverter player = SaveManager.LoadPlayer();
            level = player.currentLevel;
        }
        SceneManager.LoadScene(level);
    }
}
