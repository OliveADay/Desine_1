using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LevelTransitioner : MonoBehaviour
{
    public LayerMask playerLayer;
    GameObject playerSpawnPoint;
    BoxCollider2D myCollider;
    [HideInInspector]
    public SceneHandle sceneHandle;
    [HideInInspector]
    public int passageId;
    [HideInInspector]
    public Passage passage;
    AsyncOperation finishedLoading;
    [HideInInspector]
    public bool sceneTransitioned;
    public Vector3 offset;
}
