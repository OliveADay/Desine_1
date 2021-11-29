using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class SaveSystem 
{
    public static void SavePlayer(string saveName, SaveData saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/"+ saveName +".save";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerDataConverter data = new PlayerDataConverter(saveData);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerDataConverter LoadPlayer(string loadName)
    {
        string path = Application.persistentDataPath + "/" + loadName + ".save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerDataConverter data = formatter.Deserialize(stream) as PlayerDataConverter;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }
    public static void SaveSaves()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saves.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        GlobalSave data = new GlobalSave();
        bf.Serialize(stream, data);
        stream.Close();
    }
    public static GlobalSave LoadSaves()
    {
        string path = Application.persistentDataPath + "/saves.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GlobalSave data = formatter.Deserialize(stream) as GlobalSave;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("couldn't find save list");
            return null;
        }
    }
}
[System.Serializable]
public class GlobalSave
{
    public List<string> saves;
    public GlobalSave()
    {
        saves = SaveManager.saves;
    }
}
