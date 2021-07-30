using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-100)]
[RequireComponent(typeof(PlayerMover), typeof(PlayerJumpAggregator))]
public class PlayerInput : MonoBehaviour
{
    #region Fields
    [Header("Set input buttons")]
    [SerializeField] private KeyCode _jumpButton = KeyCode.Space;
    [SerializeField] private KeyCode _modifierUseButton = KeyCode.Q;

    private PlayerMover _mover;
    private PlayerJumpAggregator _jumpAggregator;
    #endregion

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _jumpAggregator = GetComponent<PlayerJumpAggregator>();
    }

    private void FixedUpdate()
    {
        MoveInput();
    }

    private void MoveInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        _mover.Move(horizontal);
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
            _jumpAggregator.IncreaseJumpButtonHoldTime();
        }

        if (IsJumpButtonReleased())
        {
            _jumpAggregator.TryJump();
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
            _jumpAggregator.TryActivateModifier();
            StartCoroutine(WaitModifierButtonRelease());
        }
    }

    private bool IsModifierButtonPressed() => Input.GetKeyDown(_modifierUseButton);

    private bool IsModifierButtonReleased() => Input.GetKeyUp(_modifierUseButton);

    private IEnumerator WaitModifierButtonRelease()
    {
        while (true)
        {
            if (IsModifierButtonReleased())
            {
                _jumpAggregator.DisableModifier();
                yield break;
            }
            yield return null;
        }
    }
    #endregion
}
