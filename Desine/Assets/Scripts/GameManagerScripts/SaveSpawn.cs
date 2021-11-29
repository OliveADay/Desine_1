using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSpawn : MonoBehaviour
{
    Vector3 savedPos;
    public GameObject playerRef;
    GameObject playerObj;
    public GameObject pauseMenu;
    bool pauseActive = false;
    public GameObject darkenScreenRef;
    GameObject darkenedScreen;
    private void Awake()
    {

        if (PlayerPrefs.GetInt(SaveManager.saveName) == 1)
        {
            PlayerDataConverter player = SaveManager.LoadPlayer();
            savedPos = new Vector3(player.playerPos[0], player.playerPos[1], player.playerPos[2]);
            if (player.currentLevel == SceneManager.GetActiveScene().name)
            {
                if (SceneManager.GetActiveScene().buildIndex > 3)
                {
                    playerObj = Instantiate(playerRef);
                    playerObj.transform.position = savedPos;
                }
                else if (SceneManager.GetActiveScene().buildIndex == 3)
                {
                    playerObj = GameObject.FindGameObjectWithTag("Player");
                    playerObj.transform.position = savedPos;
                }
            }
        }
    }
    private void OnEnable()
    {

    }
    private void Update()
    {
        autoSave autoSave = GetComponent<autoSave>();
        if (autoSave != null)
        {
            Debug.Log(autoSave.gameObject.name);
        }
        if (pauseMenu != null && darkenScreenRef != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseActive == false)
                {
                    pauseActive = true;
                    darkenedScreen = Instantiate(darkenScreenRef);
                    pauseMenu.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    pauseActive = false;
                    Destroy(darkenedScreen);
                    pauseMenu.SetActive(false);
                    Time.timeScale = 1;
                }
            }
        }
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
