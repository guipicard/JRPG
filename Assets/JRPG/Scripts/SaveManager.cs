using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    private static string SaveFolder => $"{Application.persistentDataPath }/Saves/";

    private static string Extension => ".save";

    private static List<SaveData> savesList = new List<SaveData>{ HasData(0), HasData(1), HasData(2) };

    private static SaveData HasData(int _index)
    {
        string saveName = $"Save{_index}{Extension}";
        string jsonData = "";

        if (!System.IO.File.Exists(SaveFolder + saveName))
        {
            return null;
        }
        try
        {
            jsonData = System.IO.File.ReadAllText(SaveFolder + saveName);
        }
        catch (Exception e)
        {
            Debug.Log($"[SAVEMANAGER] {e}");
            return null;
        }

        SaveData data = ScriptableObject.CreateInstance<SaveData>();
        JsonUtility.FromJsonOverwrite(jsonData, data);
        return data;
    }

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

    public static bool Load(SaveData data, int _index)
    {
        string saveName = $"Save{_index}{Extension}";
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
        JsonUtility.FromJsonOverwrite(jsonData, data);
        savesList[_index] = data;
        return true;
    }

    public static void NewSave(SaveData data, int _index)
    {
        if (savesList[_index] != null)
        {
            Debug.Log("purged");
        }
        data.playerInWorldPosition = new Vector2(25.0f, -8.0f);
        data.saveName = $"New Game {_index}";
        data.mapName = "New Map";
        data.party = new List<CharacterInstance>();
    }
}
