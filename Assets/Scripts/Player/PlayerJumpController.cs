using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum JumpModifier
{
    // Модификаторы прыжка
    None,
    DoubleJump,
    Parachute,
}

public class PlayerJumpController : MonoBehaviour
{

    private enum JumpState
    {
        // Состояние прыжка в зависимости от зажатия клавиши прыжка
        Idle,
        Weak,
        Strong
    }


    [Header("Set in Inspector"), Space(10)]
    [SerializeField] private KeyCode jumpButton = KeyCode.Space;
    [SerializeField] private float weakJumpForce;
    [SerializeField] private float maxJumpForce;
    [SerializeField] private float maxWeakJumpButtonHoldingTime;
    [SerializeField] private float strongJumpTimeScaling;
    [SerializeField] private int maxNumberExtraJumps = 2;
    [SerializeField] private float multiJumpForceScale = 50f;


    [Header("Set Dynamically"), Space(10)]
    [SerializeField] private JumpState jumpType = JumpState.Idle;
    [SerializeField] private JumpModifier modifier = JumpModifier.None;
    [SerializeField] private float jumpButtonHoldingTime = 0.0f;


    [Header("Set Dynamically: MultiJump modifier"), Space(10)]
    [SerializeField] private int currentNumExtraJumps;

    [Header("Set in Inspector: Parachute modifier"), Space(10)]
    [SerializeField] private GameObject parachutePrefab;


    private Rigidbody2D _rigidBody;


    private void Update()
    {
        if (Input.GetKeyDown(jumpButton))
        {
            if (jumpType == JumpState.Idle)
            {
                jumpType = JumpState.Weak;
            }
        }


        if (Input.GetKey(jumpButton))
        {
            jumpButtonHoldingTime += Time.deltaTime;
            if (jumpButtonHoldingTime > maxWeakJumpButtonHoldingTime)
            {
                jumpType = JumpState.Strong;
            }
        }


        if (Input.GetKeyUp(jumpButton) && currentNumExtraJumps > 0)
        {
            Jump();
            currentNumExtraJumps--;
        }
    }

    private void Jump()
    {
        Vector2 jumpForce = Vector2.zero;
        switch (jumpType)
        {
            case JumpState.Weak:
                jumpForce.y = weakJumpForce;
                break;
            case JumpState.Strong:
                jumpForce.y = Mathf.Min(maxJumpForce, weakJumpForce + jumpButtonHoldingTime * strongJumpTimeScaling);
                break;
        }

        float speedY = _rigidBody.velocity.y;
        if (speedY < 0)
        {
            jumpForce.y += Mathf.Abs(speedY * multiJumpForceScale);
        }
        _rigidBody.AddForce(jumpForce);

        jumpButtonHoldingTime = 0.0f;
        jumpType = JumpState.Idle;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumpType = JumpState.Idle;
            currentNumExtraJumps = maxNumberExtraJumps;
        }
    }
}
