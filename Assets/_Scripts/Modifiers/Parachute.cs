using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

public class Parachute : Modifier, ITouchingGroundWhenFall
{
    #region Fields
    [Header("Set in Inspector: Parachute")]
    [SerializeField] private GameObject _parachutePrefab;
    [SerializeField] private float _parachuteGravity = 0.1f;

    private GameObject _parachute;
    private float _defaultGravity = 1;
    private FixedJoint2D _fixedJoint;
    #endregion

    protected override void Activate()
      {
        base.Activate();
        _defaultGravity = Player.Gravity;

        Create();
        PreparePlayer();

        StartCoroutine(ButtonsClickCheck());
    }

    private void Create()
    {
        _parachute = Instantiate(_parachutePrefab);
        ConnectWithPlayer();
        _parachute.transform.position = GetPositionToCreate();
    }

    private void ConnectWithPlayer()
    {
        _fixedJoint = _parachute.GetComponent<FixedJoint2D>();
        _fixedJoint.connectedBody = Player.RigidBody;
    }

    private Vector3 GetPositionToCreate()
    {
        Vector3 position = Player.transform.position;
        position.x += _fixedJoint.anchor.x;
        position.y += _fixedJoint.anchor.y;
        return position;
    }

    private void PreparePlayer()
    {
        Player.NullifyVerticalSpeed();
        Player.Gravity = _parachuteGravity;
    }

    private IEnumerator ButtonsClickCheck()
    {
        while (true)
        {
            if (IsUseButtonReleased())
            {
                Disable();
                StopAllCoroutines();
            }
            yield return null;
        }
    }

    public override void Disable()
    {
        Player.Gravity = _defaultGravity;
        Destroy(_parachute);
        base.Disable();
    }

    public void PlayerTouchedGround() => Reset();
}
