using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : TactictsMove
{
    [SerializeField]
    Dice dice;

    [SerializeField]
    UIDice diceUI;

    public GameObject Dice1;
    public GameObject Dice2;

    public AudioSource[] soundsPlayer;


    //private static DiceSide diceSide;
    // Start is called before the first frame update
    void Start()
    {
        //dice.RollDice();

        
        Init();
        move = 0;
        AttackStat = 0;
        ShieldStat = 0;
    }



    // Update is called once per frame
    void Update()
    {

        

        Debug.DrawRay(transform.position, transform.forward);
        if (!turn)
        {
            Dice1.GetComponent<Dice>().Reset();// se é o turno do inimigo dado retorna 
            Dice2.GetComponent<Dice>().Reset();
            diceUI.GetComponent<UIDice>().ResetValues();
            return;
        }

        if (!moving && dice.hasLanded && diceUI.choosingIsDone == true)
        {
            FindSelectableTiles();
            move = diceUI.GetComponent<UIDice>().moveValueInUI;
            AttackStat = diceUI.GetComponent<UIDice>().attackValueInUI;
            ShieldStat = diceUI.GetComponent<UIDice>().shieldValueInUI;
            CheckMouse();
        }
        else if(dice.hasLanded && diceUI.choosingIsDone == true)
        {
            Move();
            soundsPlayer[0].Play();// sfx movemento
            //move = dice.diceValue;
            //dice.Reset();
            //dice.RollAgain();
        }

    }

    //void TakeDamage(int damage)
    //{
    //   currentHealth -= damage;

    //   healthBar.SetHealth(currentHealth);
    // }

    public void TakeDamage()
    {
       // DefineEnemyAttacker();
        StartCoroutine(WaitAndSee());
        soundsPlayer[1].Play();// sfx dano
       // CheckShieldStat();
       // CurrentHealthStat -= SurplusDamage;
       //  CheckDeath();
    }

    public void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();

                    if (t.selectable)
                    {
                        MoveToTile(t);
                    }
                }
            }
        }
    }

    
    private IEnumerator WaitAndSee()
    {
        DefineEnemyAttacker();
        
        yield return new WaitForSeconds(0.5f);
        CheckShieldStat();
        
        yield return new WaitForSeconds(0.5f);
        CurrentHealthStat -= SurplusDamage;
        Debug.Log("3NPC");
        yield return new WaitForSeconds(0.5f);
        CheckDeath();
        
        
    }

  
}
