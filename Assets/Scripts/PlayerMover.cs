﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{

    private enum JumpType
    {
        idle,
        weak,
        strong
    }

    #region Fields
    [Header("Set in Inspector: Move Options")]
    [SerializeField] private float speed;

    [Header("Set in Inspector: Jump Options"), Space(10)]
    [SerializeField] private KeyCode jumpButton = KeyCode.Space;
    [SerializeField] private float weakJumpForce;
    [SerializeField] private float maxJumpForce;
    [SerializeField] private float maxWeakJumpButtonHoldingTime;
    [SerializeField] private float strongJumpTimeScaling;
    [SerializeField] private int maxNumberExtraJumps = 2;
    [SerializeField] private float airJumpForceScale = 50f;


    [Header("Set Dynamically"), Space(10)]
    [SerializeField] private JumpType jumpType = JumpType.idle;
    [SerializeField] private int currentNumExtraJumps;
    [SerializeField] private float jumpButtonHoldingTime = 0.0f;



    [Header("Parts Of Player"), Space(10)]
    [SerializeField] private GameObject legsGO;
    [SerializeField] private GameObject bodyGO;


    private Rigidbody2D _rigidBody;
    private Animator playerAnimator;
    #endregion

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        playerAnimator = GetComponent<Animator>();

    }

    #region Jump
    private void Update()
    {
        if (Input.GetKeyDown(jumpButton))
        {
            if (jumpType == JumpType.idle)
            {
                jumpType = JumpType.weak;            }
        }


        if (Input.GetKey(jumpButton))
        {
            jumpButtonHoldingTime += Time.deltaTime;
            if (jumpButtonHoldingTime > maxWeakJumpButtonHoldingTime)
            {
                jumpType = JumpType.strong;
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
            case JumpType.weak:
                jumpForce.y = weakJumpForce;
                break;
            case JumpType.strong:
                jumpForce.y = Mathf.Min(maxJumpForce, weakJumpForce + jumpButtonHoldingTime * strongJumpTimeScaling);
                break;
        }

        float speedY = _rigidBody.velocity.y;
        if (speedY < 0)
        {
            jumpForce.y += Mathf.Abs(speedY * airJumpForceScale);
        }
        _rigidBody.AddForce(jumpForce);

        jumpButtonHoldingTime = 0.0f;
        jumpType = JumpType.idle;
    }
    #endregion

    #region Move
    private void FixedUpdate()
    {
        // Move
        float moveInput = Input.GetAxis("Horizontal");
        _rigidBody.velocity = new Vector2(moveInput * speed, _rigidBody.velocity.y);


        if (moveInput > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            playerAnimator.Play("RunAnimation");
            //playerBodyAnimator.Play("RunBodyAnimation");
        }
        else if (moveInput < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
            playerAnimator.Play("RunAnimation");
            //playerBodyAnimator.Play("RunBodyAnimation");

        }
        if (moveInput == 0)
        {
            playerAnimator.Play("IdleAnimation");
        }
    }
    #endregion


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumpType = JumpType.idle;
            currentNumExtraJumps = maxNumberExtraJumps;
        }
    }
}
