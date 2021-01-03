using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{

    private enum JumpType
    {
        idle,
        weak,
        strong
    }

        [Header("Set in Inspector: Move Options")]
    [SerializeField] private float          speed;

        [Header("Set in Inspector: Jump Options")]
    [SerializeField] private KeyCode        jumpButton = KeyCode.Space;
    [SerializeField] private float          weakJumpForce;
    [SerializeField] private float          maxJumpForce;
    [SerializeField] private float          maxWeakJumpButtonHoldingTime;
    [SerializeField] private float          strongJumpTimeScaling;


    [Header("Set Dynamically")]
    [SerializeField] private JumpType       jumpType = JumpType.idle;
    [SerializeField] private float          jumpButtonHoldingTime;


    private Rigidbody2D                     rigidBody;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Jump     
        if (Input.GetKeyDown(jumpButton))
        {
            if (jumpType == JumpType.idle)
            {
                jumpType = JumpType.weak;
            }
        }

        if (Input.GetKey(jumpButton))
        {
            jumpButtonHoldingTime += Time.deltaTime;
            if (jumpButtonHoldingTime > maxWeakJumpButtonHoldingTime)
            {
                jumpType = JumpType.strong;
            }
        }

        if (Input.GetKeyUp(jumpButton))
        {
            Jump();
            jumpButtonHoldingTime = 0.0f;
        }
    }


    private void FixedUpdate()
    {
        // Move
        float moveInput = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(moveInput * speed, rigidBody.velocity.y);
    }


    private void Jump()
    {
        Vector2 jumpForce = Vector2.zero;
        switch (jumpType)
        {
            case JumpType.weak:
                jumpForce.y = weakJumpForce;
                //Debug.Log("Weak jump");
                break;
            case JumpType.strong:
                jumpForce.y = Mathf.Min(maxJumpForce, weakJumpForce + jumpButtonHoldingTime * strongJumpTimeScaling);
                //Debug.Log("Strong jump");
                break;
        }
        rigidBody.AddForce(jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumpType = JumpType.idle;
        }
    }
}
