using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager 
{
    public static string saveName;
    public static int isSaved = 0;
    public static List<string> saves = new List<string>();
    public static void SavePlayer(SaveData data)
    {
        SaveSystem.SavePlayer(saveName, data);
        isSaved = 1;
        SaveInt(saveName, isSaved);
    }

    public static PlayerDataConverter LoadPlayer()
    {
        PlayerDataConverter playerData = SaveSystem.LoadPlayer(saveName);
        return playerData;
    }
    public static void SaveSaves()
    {
        SaveSystem.SaveSaves();
    }
    public static GlobalSave LoadSaves()
    {
        GlobalSave data = SaveSystem.LoadSaves();
        return data;
    }
    public static string FindSaveFiles(string SaveName)
    {
        string path = Application.persistentDataPath + '/' + SaveName + ".save";
        return path;
    }
    public static void Restart(GameObject Save)
    {
        string path = FindSaveFiles(Save.name);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        GlobalSave save = LoadSaves();
        saves = save.saves;
        SetIntsWithName(Save.name, 0);
    }
    public static void SaveInt(string path, int vairalbe)
    {
        if (!saves.Contains(path))
        {
            string lastsave = FindSaveFiles("saves");
            if (File.Exists(lastsave))
            {
                GlobalSave savesG = LoadSaves();
                foreach(string save in savesG.saves.ToArray())
                {
                    if (!saves.Contains(save))
                    {
                        saves.Add(save);
                    }
                }
            }
            saves.Add(path);
            SaveSaves();
        }
        PlayerPrefs.SetInt(path, vairalbe);
    }
    public static void SetIntsWithName(string name, int variable)
    {
        for (int i = 0; i < saves.ToArray().Length; i++)
        {
            if (saves.ToArray()[i] != null)
            {
                if (saves.ToArray()[i].Contains(name))
                {
                    SaveInt(saves.ToArray()[i], variable);
                }
            }
        }
    }
}
