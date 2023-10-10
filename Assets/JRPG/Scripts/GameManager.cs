using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public SaveData SaveData;
    public Action onSave;
    public Action onLoad;
    public int saveIndex = 0;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    NewGame();
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    Save();
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    Load();
        //}
    }
    public SaveData NewGame(int _index)
    {
        SaveManager.NewSave(SaveData, _index);
        SetIndex(_index);
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
            Debug.Log("Load Succeed ! !");
            onLoad?.Invoke();
        }
        else
        {
            Debug.Log("Load Failed.");
        }
    }

}
