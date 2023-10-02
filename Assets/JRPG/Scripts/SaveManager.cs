using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    public static string SaveFolder => $"{Application.persistentDataPath }/Saves/";
    public static string Extension => ".save";

    public static List<SaveData> savesList = new List<SaveData>();
    public static bool Save(SaveData _data, int _index)
    {
        string saveName = $"Save{_index}{Extension}";
        string jsonData = ""; 

        try
        {
            jsonData = JsonUtility.ToJson(_data);
        }
        catch(Exception e)
        {
            Debug.Log($"[SAVEMANAGER] {e}");
            return false;
        }
        if (!System.IO.Directory.Exists(SaveFolder))
        {
            savesList[_index] = _data;
            System.IO.Directory.CreateDirectory(SaveFolder);
        }
        System.IO.File.WriteAllText(SaveFolder + saveName, jsonData);

        return true;
    }

    public static bool Load(int _index)
    {
        string saveName = $"Save_{_index}{Extension}";
        string jsonData = "";

        if (!System.IO.File.Exists(SaveFolder + saveName))
        {
            Debug.Log($"[SAVEMANAGER] File you tried to load does not exist. Index: {_index}");
            return false;
        }
        try
        {
            jsonData = System.IO.File.ReadAllText(SaveFolder + saveName);
        }
        catch (Exception e)
        {
            Debug.Log($"[SAVEMANAGER] {e}");
            return false;
        }
        SaveData data = savesList[_index];
        JsonUtility.FromJsonOverwrite(jsonData, data);
        return true;
    }

    public static SaveData NewSave(int _index)
    {
        if (savesList[_index] != null)
        {
            Debug.Log("purged");
        }
        SaveData data = new SaveData();
        data.playerInWorldPosition = Vector2.zero;
        data.saveName = $"New Game {_index}";
        data.mapName = "New Map";
        data.party = new List<CharacterInstance>();

        savesList[_index] = data;
        GameManager.Instance.SetIndex(_index);
        return data;   
    }
}
