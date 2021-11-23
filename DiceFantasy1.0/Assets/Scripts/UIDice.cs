using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDice : MonoBehaviour
{
    [SerializeField]
    Dice dice;

    [SerializeField]
    TactictsMove tacticts;

    [SerializeField]
    NPCMove nPC;

    [SerializeField]
    PlayerMove playerMoveScript;

    public GameObject Dice1;
    public GameObject Dice2;

    public bool dice1Used;
    public bool dice2Used;

    public TextMeshProUGUI throwText;
    public TextMeshProUGUI valueText1;
    public TextMeshProUGUI valueText2;

    [Header("Blinking Text")]
    public float minTime = 0.05f;
    public float maxTime = 1.2f;
    private float timer;


    // Update is called once per frame
    void Update()
    {
        dice1Used = Dice1.GetComponent<Dice>().usedDice;
        dice2Used = Dice2.GetComponent<Dice>().usedDice;

        if (dice1Used == true && dice2Used == true && tacticts.isPlayer && !nPC.moving)// usou o dado
        {
            throwText.enabled = false;
            valueText1.enabled = true;
            valueText2.enabled = true;
            valueText1.text = Dice1.GetComponent<Dice>().diceValue.ToString();
            valueText2.text = Dice2.GetComponent<Dice>().diceValue.ToString();
        }

        if (!dice1Used && !dice2Used && tacticts.isPlayer && !nPC.moving)// não usou o dado
        {
            throwText.enabled = true;
            valueText1.enabled = false;
            valueText2.enabled = false;

        }


        else if (dice1Used == true && dice2Used == true && !tacticts.isPlayer && nPC.moving && !playerMoveScript.turn)// turno do npc
        {
            valueText1.enabled = false;
            valueText2.enabled = false;
            throwText.enabled = false;
        }
    }

}
