using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Menu.SetActive(true);
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

    public void Load1()
    {
        GameManager.Instance.SetIndex(1);
        SceneManager.LoadScene("SampleScene");
    }

    public void Load2()
    {
        GameManager.Instance.SetIndex(2);
        SceneManager.LoadScene("SampleScene");
        //GameManager.Instance.Load(2);
    }

    public void Load3()
    {
        GameManager.Instance.SetIndex(3);
        SceneManager.LoadScene("SampleScene");
        //GameManager.Instance.Load(3);
    }

    public void NewGame1()
    {
        GameManager.Instance.NewGame(1);
        SceneManager.LoadScene("SampleScene");
    }

    public void NewGame2()
    {
        GameManager.Instance.NewGame(2);
        SceneManager.LoadScene("SampleScene");
    }

    public void NewGame3()
    {
        GameManager.Instance.NewGame(3);
        SceneManager.LoadScene("SampleScene");
    }
}
