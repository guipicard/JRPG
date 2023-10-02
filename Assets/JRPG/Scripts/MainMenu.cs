using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject NewGamesMenu;
    [SerializeField] GameObject LoadsMenu;
    private Stack<GameObject> gameList;
    // Start is called before the first frame update
    void Start()
    {
        NewGamesMenu.SetActive(false);
        LoadsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void 
}
