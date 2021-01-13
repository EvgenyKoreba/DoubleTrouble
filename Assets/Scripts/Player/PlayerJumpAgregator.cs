using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJumpAgregator : MonoBehaviour
{
    private enum JumpState
    {
        // Состояние прыжка в зависимости от зажатия клавиши прыжка
        Idle,
        Weak,
        Strong
    }

    #region Fields
    [Header("       Set in Inspector")]
    [SerializeField] private KeyCode jumpButton = KeyCode.Space;
    [SerializeField] private float weakJumpForce;
    [SerializeField] private float maxJumpForce;
    [SerializeField] private float maxWeakJumpButtonHoldingTime;
    [SerializeField] private float strongJumpTimeScaling;
    [SerializeField] private int maxNumberMultiJumps = 2;
    [SerializeField] private float multiJumpForceScale = 50f;


    [Header("       Set Dynamically"), Space(30)]
    [SerializeField] private JumpState jumpType = JumpState.Idle;
    [SerializeField] private Modifier _modifier;
    [SerializeField] private float jumpButtonHoldingTime = 0.0f;
    [SerializeField] private int currentNumOfJumps;


    private Rigidbody2D _rigidBody;
    #endregion


    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(jumpButton))
        {
            jumpType = JumpState.Weak;
        }


        if (Input.GetKey(jumpButton))
        {
            jumpButtonHoldingTime += Time.deltaTime;
            if (jumpButtonHoldingTime > maxWeakJumpButtonHoldingTime)
            {
                jumpType = JumpState.Strong;
            }
        }


        if (currentNumOfJumps > 0)
        {
            if (Input.GetKeyUp(jumpButton))
            {
                Jump();
                currentNumOfJumps--;
            }
        }
        else if (currentNumOfJumps == 0)
        {
            if (Input.GetKeyDown(_modifier.useButton))
            {
                _modifier.Activate();
            }
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


    public void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // Нужно исправить, что прыжок копится при удержании кнопки прыжка во время
            // парашута уже на земле, а при отпускании прыгает
            _modifier.Disable();
            currentNumOfJumps = maxNumberMultiJumps;
        }
    }
}
