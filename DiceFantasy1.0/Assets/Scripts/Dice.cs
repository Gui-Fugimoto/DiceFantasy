using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{ 
    Rigidbody rb;

    public bool hasLanded;
    public bool thrown;

    Vector3 initPosition;

    public int diceValue;

    public DiceSide[] diceSides;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initPosition = transform.position;
        rb.useGravity = false;
    }
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space))
       { 
          RollDice();
            //InitTeamTurnQueue();
          //StartTurn();
       }

       if(rb.IsSleeping() && !hasLanded && thrown)
       {
            hasLanded = true;
            rb.useGravity = false;
            rb.isKinematic = true;

            SideValueCheck();
       }
       else if(rb.IsSleeping() && hasLanded && diceValue == 0)
       {
            RollAgain();
       }
    }

    public void RollDice()
    {
        if(!thrown && !hasLanded)
        {
            thrown = true;
            rb.useGravity = thrown;
            rb.AddTorque(Random.Range(0, 400), Random.Range(0, 400), Random.Range(0, 400));
        }
        else if(thrown && hasLanded)
        {
            Reset();
        }
    }

    public void Reset()
    {
        transform.position = initPosition;
        thrown = false;
        hasLanded = false;
        rb.useGravity = false;
        rb.isKinematic = false;
    }

    public void RollAgain()
    {
        thrown = true;
        rb.useGravity = thrown;
        rb.AddTorque(Random.Range(0, 400), Random.Range(0, 400), Random.Range(0, 400));

        Reset();
    }

    public void SideValueCheck()
    {
        diceValue = 0;
        foreach(DiceSide side in diceSides)
        {
            if (side.OnGround())
            {
                diceValue = side.sideValue;
                Debug.Log(diceValue + "has been rolled");
            }
        }
    }
}
