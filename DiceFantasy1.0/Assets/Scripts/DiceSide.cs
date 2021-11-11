using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    public bool onGround;
    public int sideValue;


     public void OnTriggerEnter(Collider col)
     {
        if (col.tag == "Ground")
        {
            onGround = true;
           // move = diceValue;
        }
     }

    public void OnTriggerExit(Collider col)
    {
        if (col.tag == "Ground")
        {
            onGround = false;
        }
    }

    public bool OnGround()
    {
        return onGround;
    }
}
