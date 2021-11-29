using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneHandle))]
public class SceneEditor : Editor
{
    string originalPath;
    string[] passages;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SceneHandle scenehandler = (SceneHandle)target;
        if (GUILayout.Button("+"))
        {
            Passage passage = ScriptableObject.CreateInstance<Passage>();
            passage.name = "Passage" + scenehandler.passageNumber.ToString();
            AssetDatabase.AddObjectToAsset(passage, scenehandler);
            scenehandler.passages.Add(passage);
            scenehandler.passageNames.Add(passage.name);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            scenehandler.passageNumber++;
        }
    }
}
