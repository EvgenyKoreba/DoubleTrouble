using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
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
    [SerializeField] private JumpState jumpState = JumpState.Idle;
    [SerializeField] private Modifier _modifier;
    [SerializeField] private float jumpButtonHoldingTime = 0.0f;
    [SerializeField] private int currentNumOfJumps;


    [HideInInspector] public Rigidbody2D rigidBody;

    private Animator _animator;
    #endregion


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }


    private void Start()
    {
        ResetJumps();
    }


    private void Update()
    {
        if (Input.GetKeyDown(jumpButton))
        {
            jumpState = JumpState.Weak;
        }


        if (jumpState != JumpState.Idle)
        {
            if (Input.GetKey(jumpButton))
            {
                jumpButtonHoldingTime += Time.deltaTime;
                if (jumpButtonHoldingTime > maxWeakJumpButtonHoldingTime)
                {
                    jumpState = JumpState.Strong;
                }
            }


            if (currentNumOfJumps > 0)
            {
                if (Input.GetKeyUp(jumpButton))
                {
                    Jump();
                    if (currentNumOfJumps == maxNumberMultiJumps)
                    {
                        _animator.SetBool("Jumping", true);
                    } else
                    {
                        _animator.SetBool("DoubleJumping", true);
                    }
                    currentNumOfJumps--;
                }
            }
            else if (currentNumOfJumps == 0)
            {
                if (Input.GetKeyDown(_modifier.useButton))
                {
                    _modifier.Activate();
                    currentNumOfJumps--;
                }
            }
        }
    }


    private void Jump()
    {
        Vector2 jumpForce = Vector2.zero;
        switch (jumpState)
        {
            case JumpState.Weak:
                jumpForce.y = weakJumpForce;
                break;
            case JumpState.Strong:
                jumpForce.y = Mathf.Min(maxJumpForce, weakJumpForce + jumpButtonHoldingTime * strongJumpTimeScaling);
                break;
        }

        float speedY = rigidBody.velocity.y;
        if (speedY < 0)
        {
            jumpForce.y += Mathf.Abs(speedY * multiJumpForceScale);
        }
        rigidBody.AddForce(jumpForce);

        jumpButtonHoldingTime = 0;
        jumpState = JumpState.Idle;
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            ResetJumps();
            if (_modifier != null)
            {
                _modifier.Disable();
            }
        }
    }


    public void ResetJumps()
    {
        currentNumOfJumps = maxNumberMultiJumps;
        jumpButtonHoldingTime = 0;
    }
}
