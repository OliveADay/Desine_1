using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainTheme : MonoBehaviour
{
    public AudioManager audioM;
    public Sound mainTheme;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        audioM.Play(mainTheme);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += CheckActive;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CheckActive(Scene scene, LoadSceneMode mode)
    {
        if(SceneManager.GetActiveScene().buildIndex > 1)
        {
            Destroy(gameObject);
        }
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= CheckActive;
    }
}
