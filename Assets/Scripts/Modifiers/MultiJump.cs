using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiJump : Modifier
{
    [Header("Set in Inspector: MultiJump")]
    [SerializeField] private int numberOfJumps = 2;


    private int originNumberOfJumps = 1;


    //public override void Activate()
    //{
    //    base.Activate();
    //    originNumberOfJumps = playerJumpAgregator.maxNumberMultiJumps;
    //    playerJumpAgregator.maxNumberMultiJumps = numberOfJumps;
    //}


    //public override void Disable()
    //{
    //    base.Disable();
    //    playerJumpAgregator.maxNumberMultiJumps = originNumberOfJumps;
    //}
}
