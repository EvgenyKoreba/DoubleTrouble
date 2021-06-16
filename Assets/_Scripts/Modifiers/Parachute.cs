using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Parachute : Modifier
{
    #region Fields
    [Header("Set in Inspector: Parachute")]
    [SerializeField] private GameObject _parachutePrefab;
    [SerializeField] private float _gravityReductionFactor = 0.1f;

    private GameObject _parachute;
    private float _gravityRemember = 1;
    #endregion

    protected override void Activate()
      {
        base.Activate();
        _gravityRemember = PlayerJumpAggregator.RigidBody.gravityScale;
        _parachute = Instantiate(_parachutePrefab);
        FixedJoint2D parachuteFixedJoint = _parachute.GetComponent<FixedJoint2D>();
        parachuteFixedJoint.connectedBody = PlayerJumpAggregator.RigidBody;

        Vector3 pos = PlayerJumpAggregator.transform.position;
        pos.x += parachuteFixedJoint.anchor.x;
        pos.y += parachuteFixedJoint.anchor.y;
        _parachute.transform.position = pos;

        Vector3 velocity = PlayerJumpAggregator.RigidBody.velocity;
        velocity.y = 0;
        PlayerJumpAggregator.RigidBody.velocity = velocity;
        Rigidbody2D parachuteRigidBody = _parachute.GetComponent<Rigidbody2D>();
        parachuteRigidBody.velocity = velocity;
        parachuteRigidBody.gravityScale = _gravityReductionFactor;
        PlayerJumpAggregator.RigidBody.gravityScale = _gravityReductionFactor;

        StartCoroutine(ButtonsClickCheck());
    }

    private IEnumerator ButtonsClickCheck()
    {
        while (true)
        {
            if (Input.GetKeyUp(UseButton))
            {
                Disable();
                StopAllCoroutines();
            }
            yield return null;
        }
    }

    public override void Disable()
    {
        PlayerJumpAggregator.RigidBody.gravityScale = _gravityRemember;
        Destroy(_parachute);
        base.Disable();
    }
}
