using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public SaveData SaveData;
    public Action onSave;
    public Action onLoad;
    public int saveIndex = 0;
    public Vector2 spawnPosition;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Save(SaveData data, int _index)
    {
        onSave?.Invoke();
        bool success = SaveManager.Save(data, _index);
        if (success)
        {
            Debug.Log("Save Succeed ! !");
        }
        else
        {
            Debug.Log("Save Failed.");
        }
    }

    private void Load(SaveData data, int _index)
    {
        bool success = SaveManager.Load(data, _index);
        if (success)
        {
            onLoad?.Invoke();
            Debug.Log("Load Succeed ! !");
        }
        else
        {
            Debug.Log("Load Failed.");
        }
    }
    public SaveData NewGame(int _index)
    {
        saveIndex = _index;
        SaveManager.NewSave(SaveData, _index);
        Save(SaveData, _index);
        return SaveData;
    }

    public int GetIndex()
    {
        return saveIndex;
    }

    public void QuickSave()
    {
        Save(SaveData, saveIndex);
    }

    public void QuickLoad()
    {
        Load(SaveData, saveIndex);
    }
    
    public IEnumerator LoadScene(int _index)
    {
        saveIndex = _index;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("WorldMap", LoadSceneMode.Additive);
        bool done = true;
        while (done)
        {
            if (asyncLoad.isDone)
            {
                Load(SaveData, saveIndex);
                SceneManager.UnloadSceneAsync("MainMenu");
                done = false;
            }
            else
            {
                yield return null;
            }
        }
    }

    public bool HasSave(int _index)
    {
        if (SaveManager.HasData(_index) == null)
        {
            return false;
        }
        return true;
    }

    public void DeleteSave(int _index)
    {
        SaveManager.DeleteSave(_index);
    }
}