using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public LevelLoader levelLoader;
    string lastLevel = "Intro";
    string saveSlotLevel = "SavePointSelect_placeholder";
    GameObject restartObj;
    public SaveSlots ss;
    bool SFX;

    private void Start()
    {

    }
    private void Update()
    {
        int lastlevel_ind = SceneManager.GetSceneByName(lastLevel).buildIndex;
        SaveManager.SaveInt("lastlevel_ind" + SaveManager.saveName, lastlevel_ind);
    }
    public void Play()
    {
        if (PlayerPrefs.GetInt(SaveManager.saveName) == 1)
        {
            PlayerDataConverter playerData = SaveManager.LoadPlayer();
            lastLevel = playerData.currentLevel;            
        }
        levelLoader.LoadLevel(lastLevel);
    }
    public void unpause()
    {
        Time.timeScale = 1;
        GameObject darkenscreenMain = GameObject.FindGameObjectWithTag("GameManager");
        if(darkenscreenMain != null)
        {
            GameObject darkenScreen = darkenscreenMain.GetComponent<SaveSpawn>().darkenedScreen;
            if(darkenScreen != null)
            {
                Destroy(darkenScreen);
            }
        }
    }
    public void SlotSelect()
    {
        levelLoader.LoadLevel(saveSlotLevel);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Show(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void Hide(GameObject obj)
    {
        obj.SetActive(false);
    }
    public void SetRestart(GameObject obj)
    {
        restartObj = obj;
    }
    public void Restart()
    {
        ss.Restart(restartObj);
    }
    public void SetSlider(bool value)
    {
        SFX = value;
    }
    public void changeValue(Slider s)
    {
        if (SFX)
        {
            PlayerPrefs.SetFloat("SFX", s.value + 0.1f);
            AudioManager.SFXvolume = s.value;
        }
        else
        {
            PlayerPrefs.SetFloat("M", s.value + 0.1f);
            AudioManager.MVolume = s.value;
        }
    }
}
