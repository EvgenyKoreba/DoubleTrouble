using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Player;

namespace Project.Animations
{

    [RequireComponent(typeof(Animator))]
    public class PlayerAnimations : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private PlayerModifier _playerModifier;
        [SerializeField] private PlayerMover _mover;
        [SerializeField] private PlayerMidAirAggregator _midAirStatesAggregator;
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
            _horizontalSpeedParamID = AnimatorParametersCustomizer.GetHash("horizontalSpeed");
            _verticalVelocityParamID = AnimatorParametersCustomizer.GetHash("verticalVelocity");
            _isJumpingParamID = AnimatorParametersCustomizer.GetHash("isJumping");
            _isGroundedParamID = AnimatorParametersCustomizer.GetHash("isGrounded");
        }

        private void Update()
        {
            _animator.SetFloat(_horizontalSpeedParamID, Mathf.Abs(_mover.X_direction));
            _animator.SetFloat(_verticalVelocityParamID, _rigidbody.velocity.y);
            _animator.SetBool(_isGroundedParamID, _midAirStatesAggregator.IsGrounded);
            _animator.SetBool(_isJumpingParamID, _midAirStatesAggregator.IsJumping);

            if (_playerModifier.CurrentModifier != null)
            {
                _animator.SetBool(_playerModifier.CurrentModifier.ParamHash, _playerModifier.CurrentModifier.IsActive);
            }
        }
    }

}
