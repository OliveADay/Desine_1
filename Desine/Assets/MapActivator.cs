using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MapActivator : MonoBehaviour
{
    [HideInInspector]
    public bool activeate = false;
    public GameObject[] map;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("mapActive" + SaveManager.saveName) == 1)
        {
            activeate = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activeate)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                for (int i = 0; i < map.Length; i++)
                {
                    if (map[i] != null)
                    {
                        MapReciever mainMap = map[i].GetComponent<MapReciever>();
                        if(mainMap != null)
                        {
                            if (mainMap.minBuildIndex <= SceneManager.GetActiveScene().buildIndex && mainMap.maxBuildIndex >= SceneManager.GetActiveScene().buildIndex)
                            {
                                if (map[i].transform.localScale.x == 6)
                                {
                                    map[i].transform.localScale = new Vector2(0, 0);
                                }
                                else
                                {
                                    map[i].transform.localScale = new Vector2(6, 6);
                                }
                            }
                        }                        
                    }
                                    
                }                           
            }
            SaveManager.SaveInt("mapActive" + SaveManager.saveName, 1);
        }
    }
}
