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

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
    }

    void Update()
    {
    }

    public SaveData NewGame(int _index)
    {
        SetIndex(_index);
        SaveManager.NewSave(SaveData, _index);
        Save(SaveData, _index);
        return SaveData;
    }

    public int GetIndex()
    {
        return saveIndex;
    }

    public void SetIndex(int _index)
    {
        saveIndex = _index;
    }

    public void QuickSave()
    {
        Save(SaveData, saveIndex);
    }

    public void QuickLoad()
    {
        Load(SaveData, saveIndex);
    }

    public void Save(SaveData data, int _index)
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

    public void Load(SaveData data, int _index)
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

    public IEnumerator LoadScene(int _index)
    {
        SetIndex(_index);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Additive);
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
}