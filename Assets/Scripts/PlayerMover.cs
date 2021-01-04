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
    [SerializeField] private float speed;

    [Header("Set in Inspector: Jump Options")]
    [SerializeField] private KeyCode jumpButton = KeyCode.Space;
    [SerializeField] private float weakJumpForce;
    [SerializeField] private float maxJumpForce;
    [SerializeField] private float maxWeakJumpButtonHoldingTime;
    [SerializeField] private float strongJumpTimeScaling;

    [Header("Set in Inspector: Attack Options")]
    [SerializeField] private KeyCode attackButton = KeyCode.E;
    [SerializeField] private bool canAttack = true;
    [SerializeField] private float attackCoolDown = 1f;


    [Header("Set Dynamically")]
    [SerializeField] private JumpType jumpType = JumpType.idle;
    [SerializeField] private float jumpButtonHoldingTime;


    [Header("Animations")]
    [SerializeField] private Animator playerLegsAnimator;
    [SerializeField] private Animator playerBodyAnimator;


    [Header("PartsOfPlayer")]
    [SerializeField] private GameObject legsGO;
    [SerializeField] private GameObject bodyGO;


    private Rigidbody2D rigidBody;
    private SpriteRenderer legsSpriteRenderer;
    private SpriteRenderer bodySpriteRenderer;


    private void Awake()
    {
        legsSpriteRenderer = legsGO.GetComponent<SpriteRenderer>();
        bodySpriteRenderer = bodyGO.GetComponent<SpriteRenderer>();

        rigidBody = GetComponent<Rigidbody2D>();
        playerLegsAnimator = legsGO.GetComponent<Animator>();
        playerBodyAnimator = bodyGO.GetComponent<Animator>();

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
        if (Input.GetKeyUp(attackButton) && canAttack)
            Attack();
    }


    private void FixedUpdate()
    {
        // Move
        float moveInput = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(moveInput * speed, rigidBody.velocity.y);


        if (moveInput > 0)
        {
            legsSpriteRenderer.flipX = false;
            bodySpriteRenderer.flipX = false;
            playerLegsAnimator.Play("RunAnimation");
            //playerBodyAnimator.Play("RunBodyAnimation");
        }
        else if (moveInput < 0)
        {
            legsSpriteRenderer.flipX = true;
            bodySpriteRenderer.flipX = true;
            playerLegsAnimator.Play("RunAnimation");
            //playerBodyAnimator.Play("RunBodyAnimation");

        }
        else if ((moveInput - 0.05f) < 0)
        {
            //playerLegsAnimator.Play("IdleAnimation");
        }
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
    private void Attack()
    {
        Time attackTime
        playerBodyAnimator.Play("AttackBodyAnimation");
    }
}
