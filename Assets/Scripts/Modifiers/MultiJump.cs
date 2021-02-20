using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiJump : Modifier
{
    [Header("Set in Inspector: MultiJump")]
    [SerializeField] private int numberOfJumps = 2;


    private int originNumberOfJumps = 1;


    protected override void PickUp()
    {
        base.PickUp();
        originNumberOfJumps = playerJumpAgregator.maxNumberMultiJumps;
        playerJumpAgregator.maxNumberMultiJumps = numberOfJumps;
    }


    public override void ThrowOut()
    {
        playerJumpAgregator.maxNumberMultiJumps = originNumberOfJumps;
        base.ThrowOut();
    }
}
