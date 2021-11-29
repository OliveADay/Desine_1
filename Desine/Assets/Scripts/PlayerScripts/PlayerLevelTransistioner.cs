using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevelTransistioner : MonoBehaviour
{
    public string levelTransitionerName;
    LevelTransitioner currentLevelTransitioner;
    AsyncOperation doneAmount;
    bool sceneTransitioned = false;
    GameObject[] players;
    public PlayerController pCon;
    // Start is called before the first frame update
    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += FindPassage;
        SceneManager.sceneLoaded += MakeOnePlayer;
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(levelTransitionerName))
        {
            currentLevelTransitioner = collision.gameObject.GetComponent<LevelTransitioner>();
            sceneTransitioned = true;
            if(pCon.land.transform.parent != transform)
            {
                pCon.land.SetActive(false);
                pCon.land.transform.position = new Vector2(transform.position.x, transform.position.y + -1);
                pCon.land.transform.parent = transform;
            }
            else if(pCon.land.transform.position.x != 0)
            {
                pCon.land.SetActive(false);
                pCon.land.transform.localPosition = new Vector2(0,-1);
            }
            doneAmount = SceneManager.LoadSceneAsync(currentLevelTransitioner.passage.sceneTo.sceneName);
        }
    }
    public void FindPassage(Scene scene, LoadSceneMode scenemode)
    {
        if (sceneTransitioned)
        {
            LevelTransitioner[] levelTransitioners = FindObjectsOfType<LevelTransitioner>();
            if(levelTransitioners != null)
            {
                foreach (LevelTransitioner levelTransitioner in levelTransitioners)
                {
                    if (levelTransitioner.passageId == currentLevelTransitioner.passage.passageIndex)
                    {
                        this.gameObject.transform.position = levelTransitioner.gameObject.transform.position + currentLevelTransitioner.offset;
                    }
                }
            }
        }
    }
    void MakeOnePlayer(Scene scene, LoadSceneMode scenemode)
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        if(players.Length > 1)
        {
            for (int i = 1; i < players.Length; i++)
            {
                if(players[i] != null)
                {
                    Destroy(players[i]);
                }
            }
        }
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= FindPassage;
        SceneManager.sceneLoaded -= MakeOnePlayer;
    }
}
