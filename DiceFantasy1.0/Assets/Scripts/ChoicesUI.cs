using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoicesUI : MonoBehaviour
{
    [SerializeField]
    TactictsMove tacticts;

    public GameObject actions;

    public GameObject attackButton;
    public GameObject defenseButton;

    // Start is called before the first frame update
    void Start()
    {
        actions.SetActive(false);
    }

    public void Activate()
    {
        if (tacticts.canSelect)
        {
            actions.SetActive(true);
        }

        else if (tacticts.canSelect == false)
        {
            actions.SetActive(false);
        }
    }

    public void AttackPressed()
    {
        // se movimenta até o npc e o ataca;
    }

    public void DefensePressed()
    {
        // armazena o valor do dado para se defender do ataque do npc;
    }
}

