using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public void AttackAnimation()
    {
        anim.SetBool("Attack", true);
    }

    public void DamageAnimation()
    {
        anim.SetBool("Damage", true);
    }

    public void IdleAnimation()
    {
        anim.SetBool("Attack", false);
        anim.SetBool("Damage", false);
    }
}
