using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class TypeWriter : MonoBehaviour
{
    public float speed;
    public Text text;
    public AudioManager audioManager;
    public Sound dialougeS;
    public UnityEvent localRun;
    bool oneRun = true;
    public float waitTime;
    public UnityEvent whenDone;
    bool Waitdone = false;
    bool typetDone = false;
    // Update is called once per frame
    void Update()
    {
        if(localRun != null && oneRun)
        {
            localRun.Invoke();
            oneRun = false;
        }
        if (typetDone)
        {
            if (Waitdone)
            {
                
            }
            else
            {
                StartCoroutine(Wait(Waitdone, waitTime));
            }
        }
    }

    public void Run(string t, Text Text)
    {
        StartCoroutine(typeText(t, Text, dialougeS, audioManager));
    }

    public void Run(string t)
    {
        StartCoroutine(typeText(t, text, dialougeS, audioManager));
    }
    public void Run()
    {
        StartCoroutine(typeText(text.text, text, dialougeS, audioManager));
    }
    IEnumerator typeText(string t, Text Text, Sound s, AudioManager a)
    {
        typetDone = false;
        float i = 0;
        int characterIndex = 0;
        int lastIndex = 0;
        
        while (characterIndex < t.Length)
        {
            i += Time.deltaTime * speed;
            characterIndex = Mathf.FloorToInt(i);
            if (lastIndex != characterIndex)
            {
                if (s != null && a != null)
                {
                    a.Play(s);
                }
            }
            characterIndex = Mathf.Clamp(characterIndex, 0, t.Length);
            Text.text = t.Substring(0, characterIndex);
            lastIndex = characterIndex;
            if (Input.GetMouseButtonDown(0))
            {
                characterIndex = t.Length;
            }
            yield return null;
        }
        Text.text = t;
        typetDone = true;
        //whenDone.Invoke();
    }

    IEnumerator Wait(bool done, float time)
    {
        done = false;
        yield return new WaitForSeconds(time);
        whenDone.Invoke();
    }
}
