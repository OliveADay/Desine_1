using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SavePointScript : MonoBehaviour
{
    public BoxCollider2D myCollider;
    SaveData playerSaveData;
    public LayerMask whatIsPlayer;
    BoxCollider2D playerCollider;
    [HideInInspector]
    static public int isSaved = 0;
    public string playerName = "Player";
    public ParticleSystem saveEffect;
    public AudioManager audioManager;
    public Sound saveSound;
    public Text text;
    public string button = "[S]";
    bool textSet = false;
    public GameObject popup;
    Player pcore;
    void Start()
    {
        playerCollider = GameObject.FindGameObjectWithTag(playerName).GetComponent<BoxCollider2D>();
        pcore = playerCollider.gameObject.GetComponent<Player>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!pcore.death)
        {
            if (myCollider.IsTouchingLayers(whatIsPlayer))
            {
                if (PlayerPrefs.GetInt(SaveManager.saveName) == 0)
                {
                    popup.SetActive(true);


                    if (!textSet)
                    {
                        text.text += button;
                        textSet = true;
                    }
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    playerSaveData = playerCollider.gameObject.GetComponent<SaveData>();
                    SaveManager.SavePlayer(playerSaveData);
                    audioManager.Play(saveSound);
                    playerCollider.gameObject.GetComponent<Player>().currentHealth = playerCollider.gameObject.GetComponent<Player>().maxHealth;
                    Destroy(Instantiate(saveEffect.gameObject, playerCollider.transform), saveEffect.startLifetime);
                }
            }
            else if (popup != null)
            {
                popup.SetActive(false);
            }
        }
    }
}
