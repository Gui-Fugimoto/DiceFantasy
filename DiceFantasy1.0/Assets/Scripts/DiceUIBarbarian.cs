using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DiceUIBarbarian : MonoBehaviour
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

    public bool dice1Used; 
    public bool HUDisOpen = false;
    public bool diceValue1Used = false;
    public bool diceValue2Used = false;
    public bool choosingIsDone = false;

    public TextMeshProUGUI throwText;
    public TextMeshProUGUI valueText1;
    

    public TextMeshProUGUI moveSlot;
    public TextMeshProUGUI attackSlot;
    public TextMeshProUGUI shieldSlot;

    public int moveValueInUI;
    public int attackValueInUI;
    public int shieldValueInUI;

    public Image diceImage1Highlighted;
    public Image diceImage2Highlighted;
    public GameObject diceButton1;
    public GameObject moveValueButton;
    public GameObject attackValueButton;
    public GameObject shieldValueButton;
    //podera ser mudado no futuro, como quando um aliado puder mudar a range, adicionar rangeIcon por exemplo;
    public Image moveIcon;
    public Image attackIcon;
    public Image shieldIcon;


    [Header("Blinking Text")]
    public float minTime = 0.05f;
    public float maxTime = 1.2f;
    private float timer;

    void Start()
    {
        CloseChoiceHUD();
    }
    // Update is called once per frame
    void Update()
    {
        dice1Used = Dice1.GetComponent<Dice>().usedDice;
        

        if (dice1Used == true && tacticts.isPlayer && !nPC.moving && choosingIsDone == false)// usou o dado
        {

            throwText.enabled = false;
            valueText1.text = Dice1.GetComponent<Dice>().diceValue.ToString();
            
            OpenChoiceHUD();


        }

        if (!dice1Used && tacticts.isPlayer && !nPC.moving)// não usou o dado
        {
            throwText.enabled = true;
            // valueText1.enabled = false;
            // valueText2.enabled = false;

        }


        else if (dice1Used == true && HUDisOpen == false && !tacticts.isPlayer && nPC.moving && !playerMoveScript.turn)// turno do npc
        {
            valueText1.enabled = false;
            throwText.enabled = false;
        }
    }

    public void OpenChoiceHUD()
    {
        HUDisOpen = true;
        moveIcon.enabled = true;
        attackIcon.enabled = true;
        shieldIcon.enabled = true;
        moveValueButton.SetActive(true);
        attackValueButton.SetActive(true);
        shieldValueButton.SetActive(true);
        moveSlot.enabled = true;
        attackSlot.enabled = true;
        shieldSlot.enabled = true;

        diceButton1.SetActive(true);
        valueText1.enabled = true;
       
    }

    public void CloseChoiceHUD()
    {
        HUDisOpen = false;
        moveIcon.enabled = false;
        attackIcon.enabled = false;
        shieldIcon.enabled = false;
        moveValueButton.SetActive(false);
        attackValueButton.SetActive(false);
        shieldValueButton.SetActive(false);
        moveSlot.enabled = false;
        attackSlot.enabled = false;
        shieldSlot.enabled = false;
        Debug.Log("dotinah");
        diceValue1Used = false;

        diceImage1Highlighted.enabled = false;
        
        diceButton1.SetActive(false);
    }

    public void ResetValues()
    {
        attackValueInUI = 0;
        moveValueInUI = 0;
        shieldValueInUI = 0;
        attackSlot.text = attackValueInUI.ToString();
        moveSlot.text = moveValueInUI.ToString();
        shieldSlot.text = shieldValueInUI.ToString();

    }

    public void CheckIfChoiceIsDone()
    {
        if (diceValue1Used == true)
        {
            choosingIsDone = true;
            CloseChoiceHUD();
        }
    }
    public void ClickToChooseDiceValue1()
    {
        if (diceValue1Used == false)
        {
            diceImage1Highlighted.enabled = true;
        }

    }
    public void ClickToChooseDiceValue2()
    {
        if (diceValue2Used == false)
        {
            diceImage1Highlighted.enabled = false;
        }
    }
    public void ClickToChooseAttackValue()
    {
        if (diceImage1Highlighted.enabled == true)
        {
            valueText1.enabled = false;
            attackValueInUI = Dice1.GetComponent<Dice>().diceValue;
            diceValue1Used = true;
            diceImage1Highlighted.enabled = false;
            attackSlot.text = attackValueInUI.ToString();
            diceButton1.SetActive(false);
            CheckIfChoiceIsDone();
        }
    }
    public void ClickToChooseMoveValue()
    {
        if (diceImage1Highlighted.enabled == true)
        {
            valueText1.enabled = false;
            moveValueInUI = Dice1.GetComponent<Dice>().diceValue;
            diceValue1Used = true;
            diceImage1Highlighted.enabled = false;
            moveSlot.text = moveValueInUI.ToString();
            diceButton1.SetActive(false);
            CheckIfChoiceIsDone();
        }
    }
}
