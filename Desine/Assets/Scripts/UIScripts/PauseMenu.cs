using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public string mainMenuIndex = "Main Menu_PlaceHolder";
    public LevelLoader levelLoader;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MainMenu()
    {
        Destroy(player);
        levelLoader.LoadLevel(mainMenuIndex);
    }
}
