using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static Dictionary<string,List<TactictsMove>> units= new Dictionary<string, List<TactictsMove>>();
    public static Queue<string> turnKey = new Queue<string>();
    public static Queue<TactictsMove> turnTeam = new Queue<TactictsMove>();
    private static bool unit;


    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if (turnTeam.Count == 0)
        {
            InitTeamTurnQueue();
            //RollDice();
        }
    }

    public static void InitTeamTurnQueue()
    {
        
        List<TactictsMove> teamList = units[(turnKey.Peek())];

        foreach(TactictsMove unit in teamList)
        {
            if (unit ?? null)
            {
                turnTeam.Enqueue(unit);
            }
            else
            {
              //  turnTeam.Dequeue(unit);
            }
                // turnTeam.Enqueue(unit);
        }
        
        
        StartTurn();
         
    }
    public static void StartTurn()
    {
        if (turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginTurn();
            Debug.Log("StartTurn");
        }
    }

    public static void EndTurn()
    {
        TactictsMove unit = turnTeam.Dequeue();

        unit.EndTurn();
        Debug.Log("turn ended");
        

        if (turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            if(unit ?? null)
            {
                InitTeamTurnQueue();
            }
            
        }
    }

    public void FinalizaTurno()
    {
        EndTurn();

    }


    public static void AddUnit(TactictsMove unit)
    {
       List<TactictsMove> list;

        if (!units.ContainsKey(unit.tag))
        {
            list = new List<TactictsMove>();
            units[unit.tag] = list;

          if (!turnKey.Contains(unit.tag))
          {
              turnKey.Enqueue(unit.tag);
               
          }
        }
        else
        {
          list = units[unit.tag];
        }

       list.Add(unit);
    }

    IEnumerator StartTurnDelay()
    {
        yield return new WaitForSeconds(0.5f);
        StartTurn();
    }
}
