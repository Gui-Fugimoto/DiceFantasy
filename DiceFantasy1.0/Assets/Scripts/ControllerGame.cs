using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGame : MonoBehaviour
{
    private static GameObject Player, NPC;

    private static TurnManager turnManager;

    public static int diceSideThrown=0;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("PlayerMove");
        NPC= GameObject.Find("NPCMove");

        Player.GetComponent<PlayerMove>().moving = false;
        NPC.GetComponent<NPCMove>().moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Player.GetComponent<PlayerMove>().moving)
        //{
        //    Move();
       // }

       // if (NPC.GetComponent<NPCMove>().moving)
       // {
            //Move();
       // }
    }
}
