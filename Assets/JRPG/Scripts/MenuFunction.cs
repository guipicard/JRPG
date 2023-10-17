using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunction : MonoBehaviour
{
    private bool m_Pause;
    [SerializeField] private GameObject m_MenuScreen;
    // Start is called before the first frame update
    void Start()
    {
        m_Pause = false;
        Time.timeScale = 1.0f;
        m_MenuScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        m_Pause = !m_Pause;
        if (m_Pause)
        {
            Time.timeScale = 0.0f;
            m_MenuScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            m_MenuScreen.SetActive(false);
        }
    }

    public void LoadGame()
    {
        GameManager.Instance.QuickLoad();
        Pause();
    }

    public void SaveGame()
    {
        GameManager.Instance.QuickSave();
    }

    public void MainMenuexit()
    {
        GameManager.Instance.m_OnLoad = null;
        GameManager.Instance.m_OnSave = null;
        SceneManager.LoadScene("MainMenu");
    }
}
