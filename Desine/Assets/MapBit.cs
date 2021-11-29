using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapBit : MonoBehaviour
{
    public string linkedScene;
    public Image image;
    public GameObject playerActive;
    
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt(gameObject.name + SaveManager.saveName) == 1)
        {
            image.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == linkedScene)
        {
            playerActive.SetActive(true);
            image.enabled = true;
            SaveManager.SaveInt(gameObject.name + SaveManager.saveName, 1);
        }
        else
        {
            playerActive.SetActive(false);
        }
    }
}
