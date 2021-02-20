using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Parachute : Modifier
{
    [Header("Set in Inspector: Parachute")]
    [SerializeField] private GameObject parachutePrefab;
    [SerializeField] public float gravityReductionFactor = 0.1f;


    private GameObject parachute;
    private float gravityRemember = 1;

    public override void Activate()
      {
        base.Activate();
        gravityRemember = playerJumpAgregator.rigidBody.gravityScale;
        parachute = Instantiate(parachutePrefab);
        FixedJoint2D parachuteFixedJoint = parachute.GetComponent<FixedJoint2D>();
        parachuteFixedJoint.connectedBody = playerJumpAgregator.rigidBody;

        Vector3 pos = playerJumpAgregator.transform.position;
        pos.x += parachuteFixedJoint.anchor.x;
        pos.y += parachuteFixedJoint.anchor.y;
        parachute.transform.position = pos;

        Vector3 velocity = playerJumpAgregator.rigidBody.velocity;
        velocity.y = 0;
        playerJumpAgregator.rigidBody.velocity = velocity;
        Rigidbody2D parachuteRigidBody = parachute.GetComponent<Rigidbody2D>();
        parachuteRigidBody.velocity = velocity;
        parachuteRigidBody.gravityScale = gravityReductionFactor;
        playerJumpAgregator.rigidBody.gravityScale = gravityReductionFactor;

        StartCoroutine(ButtonsClickCheck());
    }


    private IEnumerator ButtonsClickCheck()
    {
        while (true)
        {
            if (Input.GetKeyUp(useButton))
            {
                if (playerJumpAgregator.isGrounded == true)
                {
                    playerJumpAgregator.ResetJumps();
                }
                Disable();
                StopAllCoroutines();
            }
            yield return null;
        }
    }


    public override void Disable()
    {
        playerJumpAgregator.rigidBody.gravityScale = gravityRemember;
        Destroy(parachute);
        base.Disable();
    }
}
