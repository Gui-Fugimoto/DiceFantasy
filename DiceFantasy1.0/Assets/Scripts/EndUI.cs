using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndUI : MonoBehaviour
{
    public GameObject gameOver;
    //public GameObject nextLevel;

    [SerializeField]
    TactictsMove tacticts;

    [SerializeField]
    PlayerMove player;


    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
        //nextLevel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (tacticts.npcDead)
        {
            gameOver.SetActive(false);
            //nextLevel.SetActive(true);
        }

        else if (player.CurrentHealthStat <= 0)
        {
            gameOver.SetActive(true);
            //nextLevel.SetActive(false);
        }
    }
}

