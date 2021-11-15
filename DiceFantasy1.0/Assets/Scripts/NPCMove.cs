using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : TactictsMove
{
    [SerializeField]
    Dice dice;


    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        move = Random.Range(1,7);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);

        if (!turn)
        {
            return;
        }

        if (!moving)
        {
            FindNearestTargetNPC();
            CalculatePath();
            FindSelectableTiles();
            actualTargetTile.target = true;
        }
        else
        {
            Move();
            move = Random.Range(1,7);
        }
    }

    void CalculatePath()
    {
        Tile targetTile = GetTargetTile(target);
        FindPath(targetTile);
    }

    void FindNearestTargetNPC()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach(GameObject objs in targets)
        {
            float d = Vector3.Distance(transform.position, objs.transform.position);

            if (d < distance)
            {
                distance = d;
                nearest = objs;
            }
        }

        target = nearest;
    }

    public void TakeDamage()
    {
        CurrentHealthStat = CurrentHealthStat - AttackStat;
        CheckDeath();
    }

}
