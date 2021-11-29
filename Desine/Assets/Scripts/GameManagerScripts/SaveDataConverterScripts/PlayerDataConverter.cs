using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDataConverter {

    public string currentLevel;
    public float[] playerPos;
    public bool isSaved = false;

    public PlayerDataConverter(SaveData playerData)
    {
        currentLevel = playerData.currentScene;
        isSaved = true;
        playerPos = new float[3];
        playerPos[0] = playerData.transform.position.x;
        playerPos[1] = playerData.transform.position.y;
        playerPos[2] = playerData.transform.position.z;
    }
}
