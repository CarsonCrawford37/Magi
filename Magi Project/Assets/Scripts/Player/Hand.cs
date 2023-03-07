using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{    
    Animator animator;

    public float speed;
    private float gripTarget;
    private float gripCurrent;
    private string animatorGripParam = "Grip";
    private float spellTarget;
    private float spellCurrent;
    private string animatorHandSpellParam = "HandSpell";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimateHand();
    }

    internal void SetGrip(float v)
    {
        gripTarget = v;
    }
    internal void SetHS(float v)
    {
        spellTarget = v;
    }

    void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorGripParam, gripCurrent);

        }

        if (spellTarget != spellCurrent)
        {
            spellCurrent = Mathf.MoveTowards(spellCurrent, spellTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorHandSpellParam, spellCurrent);

        }
    }

   
}
