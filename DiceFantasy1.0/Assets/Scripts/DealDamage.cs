using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{

    Animator anim;
    public int MyAttackStat;
    // Start is called before the first frame update
    void Start()
    {
        MyAttackStat = gameObject.GetComponentInParent<TactictsMove>().AttackStat;
        anim.SetBool("Attack", true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        Debug.Log("destruiu DealDamage, acabou turno");
        anim.SetBool("Attack", false);
        TurnManager.EndTurn();

    }
}
