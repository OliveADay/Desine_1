using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelTransitioner))]
public class LevelTransitionerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LevelTransitioner levelTransitioner = (LevelTransitioner)target;

        EditorGUILayout.BeginHorizontal();
        levelTransitioner.sceneHandle = EditorGUILayout.ObjectField(levelTransitioner.sceneHandle, typeof(SceneHandle), true) as SceneHandle;
        if (levelTransitioner.sceneHandle != null)
        {
            if (levelTransitioner.sceneHandle.passageNames != null)
            {
                levelTransitioner.passageId = EditorGUILayout.Popup(levelTransitioner.passageId, levelTransitioner.sceneHandle.passageNames.ToArray());
            }
        }
        EditorGUILayout.EndHorizontal();
        if (levelTransitioner.sceneHandle != null)
        {
            if (levelTransitioner.sceneHandle.passages != null)
            {
                if (levelTransitioner.passageId > -1)
                {
                    levelTransitioner.passage = levelTransitioner.sceneHandle.passages.ToArray()[levelTransitioner.passageId];
                }
            }
        }
    }
}