﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{ 
    Rigidbody rb;

    public bool hasLanded;
    public bool thrown;

    Vector3 initPosition;

    public int diceValue;

    public DiceSide[] diceSides;

    //private SpriteRenderer rend;
    //private Sprite[] imageSides;

    void Start()
    {
        //rend = GetComponent<SpriteRenderer>();
        //imageSides = Resources.LoadAll<Sprite>("Dies/");
        rb = GetComponent<Rigidbody>();
        //rend.sprite = imageSides[5];
        initPosition = transform.position;
        rb.useGravity = false;
    }
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space))
       { 
          RollDice();
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
            rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
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
                //rend.sprite =imageSides[diceValue];
                Debug.Log(diceValue + "has been rolled");
            }
        }
    }
}
