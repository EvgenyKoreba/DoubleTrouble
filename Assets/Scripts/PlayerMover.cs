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

        [Header("Set in Inspector")]
    [SerializeField] private float          speedMult;
    [SerializeField] private JumpType       jumpType = JumpType.idle;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private float          weakJumpButtonHoldingTime;
    [SerializeField] private float          weakJumpForce;
    [SerializeField] private float          maxJumpForce;
    [SerializeField] private float          strongJumpTimeScaling;


        [Header("Set Dynamically")]
    [SerializeField] private float jumpButtonHoldingTime;
    private Rigidbody2D rigidBody;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    rigidBody.AddForce(new Vector2(0, 500));
        //}
    }

    private void FixedUpdate()
    {
        // Move
        float x = Input.GetAxis("Horizontal");
        float speed = x * speedMult * Time.fixedDeltaTime;

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpType == JumpType.idle)
            {
                jumpType = JumpType.weak;
                isJumping = true;
            }
        }
        else if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            jumpButtonHoldingTime += Time.fixedDeltaTime;
            if (jumpButtonHoldingTime > weakJumpButtonHoldingTime)
            {
                jumpType = JumpType.strong;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
             Jump();
        }

        Vector3 newPosition = transform.position;
        newPosition.x += speed;
        transform.position = newPosition;
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
        rigidBody.AddForce(jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isJumping)
        {
            jumpType = JumpType.idle;
            isJumping = false;
            jumpButtonHoldingTime = 0;
        }
    }
}
