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

    public TextMeshProUGUI throwText;
    public TextMeshProUGUI valueText;

    [Header("Blinking Text")]
    public float minTime = 0.05f;
    public float maxTime = 1.2f;
    private float timer;

    // Update is called once per frame
    void Update()
    {

        if (dice.usedDice == true && tacticts.isPlayer && !nPC.moving)// usou o dado
        {
            throwText.enabled = false;
            valueText.enabled = true;
            valueText.text = dice.diceValue.ToString();
        }

        if (!dice.usedDice && tacticts.isPlayer && !nPC.moving)// não usou o dado
        {
            throwText.enabled = true;
            valueText.enabled = false;

        }


        else if (dice.usedDice == true && !tacticts.isPlayer && nPC.moving && !playerMoveScript.turn)// turno do npc
        {
            valueText.enabled = false;
            throwText.enabled = false;
        }
    }

    IEnumerator DelayText()
    {
        valueText.enabled = false;
        yield return new WaitForSeconds(4f);
        throwText.enabled = true;
        
    }
}
