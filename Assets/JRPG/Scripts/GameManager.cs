using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] SaveData currentSaveData;
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
    public void NewGame(int _index)
    {
        currentSaveData = SaveManager.NewSave(_index);
    }

    public int GetIndex()
    {
        return saveIndex;
    }

    public void SetIndex(int _index)
    {
        saveIndex = _index;
    }

    public void Save(int _index)
    {
        onSave?.Invoke();
        bool success = SaveManager.Save(currentSaveData, _index);
        if (success)
        {
            Debug.Log("Save Succeed ! !");
        }
        else
        {
            Debug.Log("Save Failed.");
        }
    }

    public void Load(int _index)
    {
        onLoad?.Invoke();
        bool success = SaveManager.Load(_index);
        if (success)
        {
            Debug.Log("Save Succeed ! !");
        }
        else
        {
            Debug.Log("Save Failed.");
        }
    }

}
