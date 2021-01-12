using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Parachute : Modifier
{
    [Header("Set in Inspector")]
    [SerializeField] private GameObject parachutePrefab;
    [SerializeField] public float gravityReductionFactor = 0.1f;
    [SerializeField] private float lifeTime = 5f;



    public override void Activate()
    {
        GameObject go = Instantiate(parachutePrefab);
        FixedJoint2D goFixedJoint = go.GetComponent<FixedJoint2D>();
        goFixedJoint.connectedBody = player.rigidBody;

        Vector3 pos = player.transform.position;
        pos.x += goFixedJoint.anchor.x;
        pos.y += goFixedJoint.anchor.y;
        go.transform.position = pos;

        Vector3 vel = player.rigidBody.velocity;
        vel.y = 0;
        Rigidbody2D goRigidBody = go.GetComponent<Rigidbody2D>();
        goRigidBody.velocity = vel;
        goRigidBody.AddForce(new Vector2(0, 50));
        goRigidBody.gravityScale = gravityReductionFactor;
        player.rigidBody.gravityScale = gravityReductionFactor;
    }
}
