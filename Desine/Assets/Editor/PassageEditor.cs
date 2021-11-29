using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Passage))]
public class PassageEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Passage passage = (Passage)target;
        if (passage.sceneTo != null)
        {
            passage.passageIndex = EditorGUILayout.Popup("ExitPassage", passage.passageIndex, passage.sceneTo.passageNames.ToArray());
        }
    }
}

