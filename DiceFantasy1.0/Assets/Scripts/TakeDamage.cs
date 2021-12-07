using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("TakeDamageAdicionado");
       // gameObject.GetComponent<TactictsMove>().EnemyAttackStat = FindObjectOfType<DealDamage>().GetComponent<DealDamage>().MyAttackStat;
     //   gameObject.GetComponent<TactictsMove>().FindNearestTarget();
        StartCoroutine(TakeDamageCoroutine());
        //EnemyAttackStat = target.GetComponent<DealDamage>().AttackStat;

        //CheckShieldStat();

        //CurrentHealthStat -= SurplusDamage;

        //CheckDeath();

        //Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Destroy(FindObjectOfType<DealDamage>().GetComponent<DealDamage>());
    }

    IEnumerator TakeDamageCoroutine()
    {
        gameObject.GetComponent<TactictsMove>().EnemyAttackStat = FindObjectOfType<DealDamage>().GetComponent<DealDamage>().MyAttackStat;
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<TactictsMove>().CheckShieldStat();
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<TactictsMove>().CurrentHealthStat -= gameObject.GetComponent<TactictsMove>().SurplusDamage;
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<TactictsMove>().CheckDeath();
        yield return new WaitForSeconds(0.3f);
        Destroy(this);
    }
}
