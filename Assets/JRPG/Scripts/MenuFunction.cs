using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFunction : MonoBehaviour
{
    private bool m_Pause = false;
    [SerializeField] private GameObject m_MenuScreen;
    // Start is called before the first frame update
    void Start()
    {
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
        GameManager.Instance.Load(GameManager.Instance.GetIndex());
        Pause();
    }

    public void Load1()
    {
        GameManager.Instance.Load(1);
    }

    public void Load2()
    {
        GameManager.Instance.Load(2);
    }

    public void Load3()
    {
        GameManager.Instance.Load(3);
    }

    public void SaveGame()
    {
        GameManager.Instance.Save(GameManager.Instance.GetIndex());
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
