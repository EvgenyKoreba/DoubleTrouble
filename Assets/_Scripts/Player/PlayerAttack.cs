using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
        [Header("Set in Inspector: Attack Options")]
    [SerializeField] private KeyCode        attackButton = KeyCode.E;
    [SerializeField] private bool           canAttack = true;
    //[SerializeField] private float          attackDelay = 1f;

        [Header("Animation"), Space(10)]
    [SerializeField] private Animator       playerBodyAnimator;


    private float startAttack;

    private void Update()
    {
        if (Input.GetKeyUp(attackButton) && canAttack)
        {
            Attack();
        }
    }


    private void Attack()
    {
        startAttack = Time.time;
        playerBodyAnimator.Play("AttackBodyAnimation");
    }
}
