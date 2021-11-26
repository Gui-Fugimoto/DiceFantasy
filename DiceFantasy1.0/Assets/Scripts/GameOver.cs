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

    //[SerializeField]
    //Dice dice;

    public string TTBRPG;
    public string Menu;

    public void Start()
    {
        menu = false;
        restarted = false;
    }

    public void RestartButton()
    {
        TurnManager.units = new Dictionary<string, List<TactictsMove>>();
        TurnManager.turnKey = new Queue<string>();
        TurnManager.turnTeam = new Queue<TactictsMove>();

        SceneManager.LoadScene(TTBRPG);
        restarted = true;
    }

    public void MainMenuButton()
    {
        TurnManager.units = new Dictionary<string, List<TactictsMove>>();
        TurnManager.turnKey = new Queue<string>();
        TurnManager.turnTeam = new Queue<TactictsMove>();

        SceneManager.LoadScene(Menu);
        //DontDestroyOnLoad(gameObject);
        menu = true;
    }

}

