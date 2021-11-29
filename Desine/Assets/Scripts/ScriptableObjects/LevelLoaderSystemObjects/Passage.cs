using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName ="New Passage", menuName = "ScriptableObject/Scene/Passage", order = 2)]
[System.Serializable]
public class Passage : ScriptableObject
{
    public string PassageType;
    public SceneHandle sceneTo;
    public int passageIndex;
}
