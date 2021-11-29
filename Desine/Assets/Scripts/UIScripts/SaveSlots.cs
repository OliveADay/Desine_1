using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveSlots : MonoBehaviour
{
    public void OnClick(GameObject button)
    {
        SaveManager.saveName = button.name;
    }
    public void Restart(GameObject File)
    {
        SaveManager.Restart(File);
    }
}
