using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerJumpAggregator _jumpAggregator;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private PlayerInput _input;

    private Animator _animator;

    private int _horizontalSpeedParamID;
    private int _verticalVelocityParamID;
    private int _isJumpingParamID;
    private int _isGroundedParamID;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _horizontalSpeedParamID = GetParamHash("horizontalSpeed");
        _verticalVelocityParamID = GetParamHash("verticalVelocity");
        _isJumpingParamID = GetParamHash("isJumping");
        _isGroundedParamID = GetParamHash("isGrounded");
    }

    private int GetParamHash(string param)
    {
        return Animator.StringToHash(param);
    }

    private void Update()
    {
        _animator.SetFloat(_horizontalSpeedParamID, Mathf.Abs(_mover.X_direction));
        _animator.SetFloat(_verticalVelocityParamID, _rigidbody.velocity.y);
        _animator.SetBool(_isGroundedParamID, _jumpAggregator.IsGrounded);
        _animator.SetBool(_isJumpingParamID, _jumpAggregator.IsJumping);
    }
}
