using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Parachute : Modifier
{
    [Header("Set in Inspector")]
    [SerializeField] private GameObject parachutePrefab;
    [SerializeField] public float gravityReductionFactor = 0.1f;
    [SerializeField] private float lifeTime = 5f;


    private GameObject parachute;

    public override void Activate()
    {
        parachute = Instantiate(parachutePrefab);
        FixedJoint2D parachuteFixedJoint = parachute.GetComponent<FixedJoint2D>();
        parachuteFixedJoint.connectedBody = player.rigidBody;

        Vector3 pos = player.transform.position;
        pos.x += parachuteFixedJoint.anchor.x;
        pos.y += parachuteFixedJoint.anchor.y;
        parachute.transform.position = pos;

        Vector3 velocity = player.rigidBody.velocity;
        velocity.y = 0;
        Rigidbody2D parachuteRigidBody = parachute.GetComponent<Rigidbody2D>();
        parachuteRigidBody.velocity = velocity;
        parachuteRigidBody.AddForce(new Vector2(0, 50));
        parachuteRigidBody.gravityScale = gravityReductionFactor;
        player.rigidBody.gravityScale = gravityReductionFactor;

        StartCoroutine(ButtonsClickCheck());
    }


    private IEnumerator ButtonsClickCheck()
    {
        while (true)
        {
            if (Input.GetKeyUp(useButton))
            {
                Disable();
            }
            yield return null;
        }
    }


    public override void Disable()
    {
        base.Disable();
        player.ResetJumps();
        player.rigidBody.gravityScale = 1;
        Destroy(parachute);    
    }
}
