﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{

    [DefaultExecutionOrder(-100)]
    [RequireComponent(typeof(PlayerMover), typeof(PlayerMidAirAggregator), typeof(PlayerModifier))]
    public class PlayerInput : MonoBehaviour
    {
        #region Fields
        [Header("Set input buttons")]
        [SerializeField] private KeyCode _jumpButton = KeyCode.Space;
        [SerializeField] private KeyCode _modifierUseButton = KeyCode.Q;

        private PlayerMover _mover;
        private PlayerMidAirAggregator _midAirAggregator;
        private PlayerModifier _playerModifier;
        #endregion

        private void Awake()
        {
            _mover = GetComponent<PlayerMover>();
            _midAirAggregator = GetComponent<PlayerMidAirAggregator>();
            _playerModifier = GetComponent<PlayerModifier>();
        }

        private void FixedUpdate()
        {
            MoveInput();
        }

        private void MoveInput()
        {
            float horizontalOffset = Input.GetAxis("Horizontal");
            _mover.MoveHorizontal(horizontalOffset);
        }

        private void Update()
        {
            JumpInput();
            ModifierInput();
        }

        #region Jump Input
        private void JumpInput()
        {
            if (IsJumpButtonHeldDown())
            {
                _midAirAggregator.IncreaseJumpButtonHoldTime();
            }

            if (IsJumpButtonReleased())
            {
                _midAirAggregator.TryJump();
            }
        }

        private bool IsJumpButtonHeldDown() => Input.GetKey(_jumpButton);

        private bool IsJumpButtonReleased() => Input.GetKeyUp(_jumpButton);
        #endregion

        #region Modifier Input
        private void ModifierInput()
        {
            if (IsModifierButtonPressed())
            {
                _playerModifier.TryActivateModifier();
                StartCoroutine(WaitModifierButtonRelease());
            }
        }

        private bool IsModifierButtonPressed() => Input.GetKeyDown(_modifierUseButton);

        private IEnumerator WaitModifierButtonRelease()
        {
            while (true)
            {
                if (IsModifierButtonReleased())
                {
                    _playerModifier.DisableModifier();
                    yield break;
                }
                yield return null;
            }
        }

        private bool IsModifierButtonReleased() => Input.GetKeyUp(_modifierUseButton);
        #endregion
    }

}
