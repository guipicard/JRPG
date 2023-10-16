using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject NewGamesMenu;
    [SerializeField] GameObject LoadsMenu;
    [SerializeField] List<Button> m_NewGameButtons;
    [SerializeField] List<Button> m_LoadButtons;
    
    private Stack<GameObject> m_menuStack = new Stack<GameObject>();
    private int m_Selected;
    
    void Start()
    {
        m_Selected = -1;
        NewGamesMenu.SetActive(false);
        LoadsMenu.SetActive(false);
        Menu.SetActive(true);
        m_menuStack.Push(Menu);
    }

    private void ActivateMenu(GameObject _menu)
    {
        m_menuStack.Peek().SetActive(false);
        _menu.SetActive(true);
        m_menuStack.Push(_menu);
    }

    private void DeactivateMenu()
    {
        m_menuStack.Peek().SetActive(false);
        m_menuStack.Pop();
        m_menuStack.Peek().SetActive(true);
    }
    
    private void Load()
    {
        StartCoroutine(GameManager.Instance.LoadScene(m_Selected));
    }

    private void NewGame()
    {
        GameManager.Instance.NewGame(m_Selected);
        SceneManager.LoadScene("SampleScene");
    }

    public void NewGameMenu()
    {
        ActivateMenu(NewGamesMenu);
    }

    public void LoadGameMenu()
    {
        ActivateMenu(LoadsMenu);
    }
    
    public virtual void QuitGame()
    {
        Application.Quit();
    }


    public void SelectButton(int _index)
    {
        if (m_menuStack.Peek() == LoadsMenu)
        {
            if (m_Selected != -1)m_LoadButtons[m_Selected].Select();
            m_LoadButtons[_index].Select();
        }
        else if (m_menuStack.Peek() == NewGamesMenu)
        {
            if (m_Selected != -1) m_NewGameButtons[m_Selected].Select();
            m_NewGameButtons[_index].Select();
        }
        m_Selected = _index;
    }

    public void StartButton()
    {
        if (m_Selected == -1) return;
        if (m_menuStack.Peek() == LoadsMenu)
        {
            Load();
        }
        else if (m_menuStack.Peek() == NewGamesMenu)
        {
            NewGame();
        }
    }
    
    public void LastMenu()
    {
        if (m_menuStack.Peek() == LoadsMenu)
        {
            if (m_Selected != -1) m_LoadButtons[m_Selected].Select();
        }
        else if (m_menuStack.Peek() == NewGamesMenu)
        {
            if (m_Selected != -1) m_NewGameButtons[m_Selected].Select();
        }
        DeactivateMenu();
        m_Selected = -1;
    }
}
