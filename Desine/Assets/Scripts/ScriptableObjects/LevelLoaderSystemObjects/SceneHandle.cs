using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewSceneHandler", menuName ="ScriptableObject/Scene/SceneHandler", order = 1)]
public class SceneHandle : ScriptableObject
{
    public string sceneName;
    [HideInInspector]
    public List<Passage> passages = new List<Passage>();
    [HideInInspector]
    public List<string> passageNames = new List<string>();
    [HideInInspector]
    public int passageNumber;

}
