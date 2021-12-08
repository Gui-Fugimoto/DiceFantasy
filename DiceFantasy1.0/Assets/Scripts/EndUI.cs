using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndUI : MonoBehaviour
{
    public GameObject gameOverUI;
    //public GameObject nextLevel;

    


    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }
}

