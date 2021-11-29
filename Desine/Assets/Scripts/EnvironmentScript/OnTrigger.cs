using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class OnTrigger : MonoBehaviour
{
    public Collider2D coll;
    public LayerMask pL;
    ContactFilter2D pC;
    Collider2D[] colls = new Collider2D[2];
    public Animator anim;
    public UnityEvent OnColl;
    bool isOnC;
    bool done = true;
    public bool onTr;
    public bool NoOnec;
    bool LaOnC;
    bool EnterC;
    SaveData playerData;
    private void Start()
    {
        pC.SetLayerMask(pL);
        LaOnC = isOnC;
        playerData = FindObjectOfType<SaveData>();
    }
    public void Update()
    {
        if (isOnC)
        {
            if (onTr)
            {
                if (EnterC)
                {
                    OnColl.Invoke();
                    EnterC = false;
                }
            }
            else
            {
                OnColl.Invoke();
            }
            if (!NoOnec)
            {
                coll.enabled = false;
            }
        }
    }
    public void SavePH()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Player playerMain = player.GetComponent<Player>();
            SaveManager.SaveInt("PH" + SaveManager.saveName, playerMain.currentHealth);
        }        
    } 
    private void FixedUpdate()
    {
        if(coll != null)
        {
            if (Physics2D.OverlapCollider(coll, pC, colls) > 0)
            {
                isOnC = true;
            }
            else
            {
                isOnC = false;
            }
        }        
        if(isOnC && !LaOnC)
        {
            print("touched P");
            EnterC = true;
        }
        LaOnC = isOnC;
    }

    public void SetAnimTrig(string name)
    {
        anim.SetTrigger(name);
    }
    public void SavePlayer()
    {
        if(playerData != null)
        {
            SaveManager.SavePlayer(playerData);
        }
    }
    IEnumerator ShowText(GameObject obj)
    {
        print(PlayerPrefs.GetInt(SaveManager.saveName + " " + obj.name));
        if(PlayerPrefs.GetInt(SaveManager.saveName + " " + obj.name) != 1)
        {
            done = false;
            obj.SetActive(true);
            yield return new WaitForSeconds(2);
            obj.SetActive(false);
            done = true;
            SaveManager.SaveInt(SaveManager.saveName + " " + obj.name, 1);
        }
        print(PlayerPrefs.GetInt(SaveManager.saveName + " " + obj.name));
    }
    public void ShowT(GameObject obj)
    {
        if (done)
        {
            StartCoroutine(ShowText(obj));
        }
    }
    public void SpawnPar(ParticleSystem p) => Destroy(Instantiate(p.gameObject, transform), p.main.duration);
}

