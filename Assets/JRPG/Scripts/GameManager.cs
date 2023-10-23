using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager>
{
    public SaveData m_SaveData;
    public Action m_OnSave;
    public Action m_OnLoad;
    public int m_SaveIndex;
    [SerializeField] public Vector2 m_SpawnPosition = new Vector2(25, -8);
    public GameObject audioListener;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Save(SaveData data, int _index)
    {
        m_SaveIndex = 0;
        m_OnSave?.Invoke();
        bool success = SaveManager.Save(data, _index);
        if (success)
        {
            AudioMan._instance.Play("Load");
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
            m_OnLoad?.Invoke();
            AudioMan._instance.Play("Load");
            Debug.Log("Load Succeed ! !");
        }
        else
        {
            Debug.Log("Load Failed.");
        }
    }
    public SaveData NewGame(int _index)
    {
        m_SaveIndex = _index;
        SaveManager.NewSave(m_SaveData, _index);
        Save(m_SaveData, _index);
        return m_SaveData;
    }

    public int GetIndex()
    {
        return m_SaveIndex;
    }

    public void QuickSave()
    {
        Save(m_SaveData, m_SaveIndex);
    }

    public void QuickLoad()
    {
        Load(m_SaveData, m_SaveIndex);
    }
    
    public IEnumerator LoadScene(int _index)
    {
        m_SaveIndex = _index;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("WorldMap", LoadSceneMode.Additive);
        bool done = true;
        while (done)
        {
            if (asyncLoad.isDone)
            {
                Load(m_SaveData, m_SaveIndex);
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