using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject NewGamesMenu;
    [SerializeField] GameObject LoadsMenu;
    private Stack<GameObject> gameList = new Stack<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        NewGamesMenu.SetActive(false);
        LoadsMenu.SetActive(false);
        gameList.Push(Menu);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ActivateMenu(GameObject _menu)
    {
        gameList.Peek().SetActive(false);
        _menu.SetActive(true);
        gameList.Push(_menu);
    }

    private void DeactivateMenu()
    {
        gameList.Peek().SetActive(false);
        gameList.Pop();
        gameList.Peek().SetActive(true);
    }

    public void NewGame()
    {
        ActivateMenu(NewGamesMenu);
    }

    public void LoadGame()
    {
        ActivateMenu(LoadsMenu);
    }

    public void LastMenu()
    {
        DeactivateMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
