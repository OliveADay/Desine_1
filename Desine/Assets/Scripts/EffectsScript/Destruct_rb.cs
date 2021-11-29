using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destruct_rb : MonoBehaviour
{
    public Rigidbody2D[] rbs;
    public UnityEvent ev;
    public RigidbodyType2D t;
    public bool b;
    public void DestructRb(bool save)
    {
        for (int i = 0; i < rbs.Length; i++)
        {
            if(rbs[i] != null)
            {
                rbs[i].bodyType = t;
            }
        }
        if (save)
        {
            SaveManager.SaveInt(gameObject.name + SaveManager.saveName, 1);
        }
    }
    private void Start()
    {
        if(PlayerPrefs.GetInt(gameObject.name + SaveManager.saveName) == 1)
        {
            gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (b)
        {
            ev.Invoke();
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            b = true;
        }
    }
}
