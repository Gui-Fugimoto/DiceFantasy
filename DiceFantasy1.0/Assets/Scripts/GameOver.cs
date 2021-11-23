using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public bool menu;
    public bool restarted;

    [SerializeField]
    Dice dice;

    public string TTBRPG;
    public string Menu;

    public void Start()
    {
        menu = false;
        restarted = false;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(TTBRPG);
        restarted = true;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(Menu);
        //DontDestroyOnLoad(gameObject);
        menu = true;
    }

}

