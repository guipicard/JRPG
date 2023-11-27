using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void ReturnToGame()
    {
        AudioMan._instance.Play("Button");
        SceneManager.LoadScene("WorldMap");
    }
}
