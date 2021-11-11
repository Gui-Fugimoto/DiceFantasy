using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : Dice 
{
    static Dictionary<string,List<TactictsMove>> units= new Dictionary<string, List<TactictsMove>>();
    static Queue<string> turnKey = new Queue<string>();
    static Queue<TactictsMove> turnTeam = new Queue<TactictsMove>();

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
            turnTeam.Enqueue(unit);
        }

        StartTurn();
         
    }
    public static void StartTurn()
    {
        if (turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginTurn();
        }
    }

    public static void EndTurn()
    {
        TactictsMove unit = turnTeam.Dequeue();
        unit.EndTurn();

        if (turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }
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
}
