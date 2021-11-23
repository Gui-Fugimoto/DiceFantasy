using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : TactictsMove
{
    [SerializeField]
    Dice dice;



    //private static DiceSide diceSide;
    // Start is called before the first frame update
    void Start()
    {
        //dice.RollDice();
        
       
        Init();
        move = 0;
        ShieldStat = 0;
    }



    // Update is called once per frame
    void Update()
    {

        

        Debug.DrawRay(transform.position, transform.forward);

        if (!turn)
        {
            dice.Reset();// se é o turno do inimigo dado retorna 
            return;
        }

        if (!moving && dice.hasLanded)
        {
            FindSelectableTiles();
            move = dice.diceValue;
            AttackStat = dice.diceValue;
            CheckMouse();
        }
        else if(dice.hasLanded)
        {
            Move();
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
