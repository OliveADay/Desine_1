using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ambient : MonoBehaviour
{
    public int minLev;
    public int maxLev;
    public AudioManager audioM;
    public Sound mainAmb;
    public float maxVolume;
    public float minVolume;
    public int volChange;
    float origVolume;
    // Start is called before the first frame update
    void Start()
    {        
        DontDestroyOnLoad(gameObject);
        audioM.Play(mainAmb);
        if (origVolume == 0 && mainAmb.volume != 0)
        {
            origVolume = minVolume;
        }
        else
        {
            mainAmb.volume = origVolume;
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnNewLev;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > maxLev || SceneManager.GetActiveScene().buildIndex < minLev)
        {
            Destroy(gameObject);
        }
        if(mainAmb.volume < maxVolume)
        {
            mainAmb.volume += (maxVolume - origVolume)/volChange;
        }
    } 
    public void OnNewLev(Scene scene, LoadSceneMode scenemode)
    {
        if (origVolume == 0 && mainAmb.volume != 0)
        {
            origVolume = minVolume;
        }
        if (origVolume != 0)
        {
            mainAmb.volume = origVolume;
        }
    }
    private void OnDisable()
    {
        if (origVolume == 0 && mainAmb.volume != 0)
        {
            origVolume = minVolume;
        }
        if (origVolume != 0)
        {
            mainAmb.volume = origVolume;
        }
    }
}
