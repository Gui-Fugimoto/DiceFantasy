using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : TactictsMove
{
    


    GameObject target;

    public int playerPos;

    public GameObject enemyTurn;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        move=Random.Range(1,7);
        enemyTurn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);

        if (!turn)
        {
            enemyTurn.SetActive(false);
            return;
        }

        if (!moving)
        {
            FindNearestTarget();
            CalculatePath();
            FindSelectableTiles();
            actualTargetTile.target = true;
        }
        else
        {
            enemyTurn.SetActive(true);
            Move();
            move = Random.Range(1,7);
        }
        
    }

    void CalculatePath()
    {
        Tile targetTile = GetTargetTile(target);
        FindPath(targetTile);
    }

    void FindNearestTarget()
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
        //  DefineEnemyAttacker();
        StartCoroutine(WaitAndSee());
      //  CheckShieldStat();
      // CurrentHealthStat -= SurplusDamage;
      //  CheckDeath();
    }
    private IEnumerator WaitAndSee()
    {
        DefineEnemyAttacker();
        //chama attackstat do player de enemyattackstat neste script
        yield return new WaitForSeconds(0.6f);
        CheckShieldStat();
        
        yield return new WaitForSeconds(0.5f);
        
        CurrentHealthStat -= SurplusDamage;
        Debug.Log("3Player");
        yield return new WaitForSeconds(0.5f);
        CheckDeath();
        

    }
}
