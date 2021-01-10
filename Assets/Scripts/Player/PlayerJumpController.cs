using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum JumpModifier
{
    // Модификаторы прыжка
    None,
    MultiJump,
    Parachute,
}

[RequireComponent(typeof(Rigidbody2D),typeof(FixedJoint2D), typeof(PlayerMover))]
public class PlayerJumpController : MonoBehaviour
{

    private enum JumpState
    {
        // Состояние прыжка в зависимости от зажатия клавиши прыжка
        Idle,
        Weak,
        Strong
    }

    #region Fields
    [Header("Simple Jump Options"), Space(5)]
    [Header("       Set in Inspector")]
    [SerializeField] private KeyCode jumpButton = KeyCode.Space;
    [SerializeField] private float weakJumpForce;
    [SerializeField] private float maxJumpForce;
    [SerializeField] private float maxWeakJumpButtonHoldingTime;
    [SerializeField] private float strongJumpTimeScaling;


    [Header("MultiJump modifier options"), Space(10)]
    [SerializeField] private int maxNumberMultiJumps = 2;
    [SerializeField] private float multiJumpForceScale = 50f;


    [Header("Parachute modifier options"), Space(10)]
    [SerializeField] private Parachute parachutePrefab;


    [Header("All modifiers"), Space(5)]
    [Header("       Set Dynamically"), Space(30)]
    [SerializeField] private JumpState jumpType = JumpState.Idle;
    [SerializeField] private JumpModifier _modifier = JumpModifier.None;
    [SerializeField] private float jumpButtonHoldingTime = 0.0f;


    [Header("MultiJump modifier options"), Space(10)]
    [SerializeField] private int currentNumOfJumps;


    private Rigidbody2D _rigidBody;
    private FixedJoint2D _fixedJoint;
    private PlayerMover _mover;
    #endregion
    #region Properties
    public JumpModifier modifier
    {
        get { return _modifier; }
        set
        {
            _modifier = value;
            switch (modifier)
            {
                case JumpModifier.None:
                    currentNumOfJumps = 1;
                    break;
                case JumpModifier.MultiJump:
                    currentNumOfJumps = maxNumberMultiJumps;
                    break;
                case JumpModifier.Parachute:
                    currentNumOfJumps = 1;
                    break;
            }
        }
    }
    #endregion

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _fixedJoint = GetComponent<FixedJoint2D>();
        _mover = GetComponent<PlayerMover>();
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


        if (Input.GetKeyUp(jumpButton))
        {
            if (currentNumOfJumps > 0)
            {
                Jump();
                currentNumOfJumps--;
            }
            else
            {
                JumpSelectionByModifierType();
            }
        }
    }


    private void JumpSelectionByModifierType()
    {
        switch (modifier)
        {
            case JumpModifier.Parachute:
                _fixedJoint.enabled = true;
                Parachute parachute = Instantiate(parachutePrefab);
                Vector3 pos = transform.position;
                pos.x += _fixedJoint.anchor.x;
                pos.y += _fixedJoint.anchor.y;
                parachute.transform.position = pos;
                _fixedJoint.connectedBody = parachute.rb;

                _mover.enabled = false;
                parachute.rb.velocity = _rigidBody.velocity;
                _rigidBody.gravityScale = parachute.gravityReductionFactor;
                parachute.rb.gravityScale = parachute.gravityReductionFactor;
                break;
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
            // Для теста парашута, тут должно быть modifier = JumpModifier.None - 
            // если модификатор одноразовый, если нет надо дополнительно реализовать
            // уменьшение количество использований
            modifier = JumpModifier.Parachute;
        }
    }
}
