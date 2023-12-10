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
    public Vector2 m_LastPosition;
    public List<CharacterClass> m_CharacterChoices;
    public CharacterClass m_ChosenCharacter;
    public List<CharacterInstance> m_Party;
    public FightInfo m_Fight = null;
    public GameObject audioListener;
    private Coroutine m_SceneCoroutine;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Save(SaveData data, int _index)
    {
        m_LastPosition = new Vector2(); 
        m_SaveIndex = 0;
        m_OnSave?.Invoke();
        m_SaveData.party = m_Party;
        m_SaveData.chosenClass = m_ChosenCharacter;
        m_SaveData.fight = m_Fight;
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
            m_Party = m_SaveData.party;
            m_ChosenCharacter = m_SaveData.chosenClass;
            m_Fight = m_SaveData.fight;
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
        m_ChosenCharacter = m_CharacterChoices[0]; // CHANGE FOR CHOICE
        m_Party.Add(new CharacterInstance(m_ChosenCharacter));
        m_Fight = null;
        m_SaveIndex = _index;
        SaveManager.NewSave(m_SaveData, _index);
        Save(m_SaveData, _index);
        m_SceneCoroutine = StartCoroutine(LoadScene(_index));
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
        string currentScene = SceneManager.GetActiveScene().name;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(m_SaveData.mapName, LoadSceneMode.Additive);
        bool done = true;
        while (done)
        {
            if (asyncLoad.isDone)
            {
                Load(m_SaveData, m_SaveIndex);
                SceneManager.UnloadSceneAsync(currentScene);
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

    public void SetFight(FightInfo fight)
    {
        m_Fight = fight;
    }

    public void EndFight()
    {
        m_Fight = null;
        m_LastPosition = new Vector2();
    }
}