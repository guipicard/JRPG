using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject m_Menu;
    [SerializeField] GameObject m_NewGamesMenu;
    [SerializeField] GameObject m_LoadsMenu;
    [SerializeField] List<Button> m_NewGameButtons;
    [SerializeField] List<Button> m_LoadButtons;

    private Stack<GameObject> m_MenuStack = new Stack<GameObject>();
    private int m_Selected;

    void Start()
    {
        m_Selected = -1;
        m_NewGamesMenu.SetActive(false);
        m_LoadsMenu.SetActive(false);
        m_Menu.SetActive(true);
        m_MenuStack.Push(m_Menu);
        for (int i = 0; i < m_LoadButtons.Count; i++)
        {
            m_LoadButtons[i].interactable = GameManager.Instance.HasSave(i);
        }
    }

    private void NoSelection()
    {
        m_Selected = -1;
    }

    private void ActivateMenu(GameObject _menu)
    {
        m_MenuStack.Peek().SetActive(false);
        _menu.SetActive(true);
        m_MenuStack.Push(_menu);
    }

    private void DeactivateMenu()
    {
        m_MenuStack.Peek().SetActive(false);
        m_MenuStack.Pop();
        m_MenuStack.Peek().SetActive(true);
    }

    private void Load()
    {
        AudioMan._instance.Play("Button");
        StartCoroutine(GameManager.Instance.LoadScene(m_Selected));
    }

    private void NewGame()
    {
        GameManager.Instance.NewGame(m_Selected);
    }

    public void NewGameMenu()
    {
        AudioMan._instance.Play("Button");
        ActivateMenu(m_NewGamesMenu);
    }

    public void LoadGameMenu()
    {
        AudioMan._instance.Play("Button");
        ActivateMenu(m_LoadsMenu);
    }

    public virtual void QuitGame()
    {
        AudioMan._instance.Play("Button");
        Application.Quit();
    }


    public void SelectButton(int _index)
    {
        if (m_MenuStack.Peek() == m_LoadsMenu)
        {
            if (m_Selected != -1) m_LoadButtons[m_Selected].Select();
            m_LoadButtons[_index].Select();
            AudioMan._instance.Play("Button");
        }
        else if (m_MenuStack.Peek() == m_NewGamesMenu)
        {
            if (m_Selected != -1) m_NewGameButtons[m_Selected].Select();
            m_NewGameButtons[_index].Select();
            AudioMan._instance.Play("Button");
        }
        m_Selected = _index;
    }

    public void StartButton()
    {
        if (m_Selected == -1)
        {
            AudioMan._instance.Play("ButtonFailed");
            return;
        }
        if (m_MenuStack.Peek() == m_LoadsMenu)
        {
            AudioMan._instance.Play("Button");
            Load();
        }
        else if (m_MenuStack.Peek() == m_NewGamesMenu)
        {
            AudioMan._instance.Play("Button");
            NewGame();
        }
    }

    public void LastMenu()
    {
        AudioMan._instance.Play("Button");
        if (m_MenuStack.Peek() == m_LoadsMenu)
        {
            if (m_Selected != -1) m_LoadButtons[m_Selected].Select();
        }
        else if (m_MenuStack.Peek() == m_NewGamesMenu)
        {
            if (m_Selected != -1) m_NewGameButtons[m_Selected].Select();

        }
        DeactivateMenu();
        m_Selected = -1;
    }

    public void DeleteSave()
    {
        if (m_Selected != -1)
        {
            GameManager.Instance.DeleteSave(m_Selected);
            m_LoadButtons[m_Selected].Select();
            m_LoadButtons[m_Selected].interactable = false;
            m_Selected = -1;
            AudioMan._instance.Play("Button");
        }
        else
        {
            AudioMan._instance.Play("ButtonFailed");
        }
    }
}
