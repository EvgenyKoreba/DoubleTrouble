using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerJumpAgregator : MonoBehaviour
{

    #region Fields
    [Header("       Set in Inspector")]
    [SerializeField] private KeyCode jumpButton = KeyCode.Space;
    [SerializeField] private float weakJumpForce;
    [SerializeField] private float maxJumpForce;
    [SerializeField] private float maxWeakJumpButtonHoldingTime;
    [SerializeField] private float strongJumpTimeScaling;
    public int maxNumberMultiJumps = 2;

    [Header("       Set in Inspector: ground")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float groundRememberTime = 0.2f;


    [Header("       Set Dynamically"), Space(30)]
    [SerializeField] private Modifier _modifier;
    [SerializeField] private int currentNumberOfJumps;
    [SerializeField] private float jumpButtonHoldingTime = 0.0f;
    [SerializeField] private bool _isGrounded = false;
    [SerializeField] private float groundedRemember = 0;



    [HideInInspector] public Rigidbody2D rigidBody;
    private Animator _animator;
    #endregion

    #region properties
    public bool isGrounded
    {
        get { return _isGrounded; }
        set
        {
            if (isGrounded == false && value == true)
            {
                if (_modifier != null && _modifier.isActive)
                {
                    _modifier.Disable();
                }
                else
                {
                    ResetJumps();
                }
            }
            _isGrounded = value;
        }
    }
    #endregion


    private void OnEnable()
    {
        EventManager.Subscribe(EVENT_TYPE.FoundModifier, SetModifier);
    }


    private void OnDisable()
    {
        EventManager.Unsubscribe(EVENT_TYPE.FoundModifier, SetModifier);
    }


    private void SetModifier(object[] parameters)
    {
        try
        {
            Modifier modifier = (Modifier)parameters[0];
            _modifier = modifier;
        }
        catch (System.InvalidCastException)
        {
            throw new System.Exception("Sended not a modifier");
        }
    }


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
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, ground);

        groundedRemember -= Time.deltaTime;
        if (isGrounded)
        {
            groundedRemember = groundRememberTime;
        }


        if (Input.GetKey(jumpButton))
        {
            jumpButtonHoldingTime += Time.deltaTime;
        }


        if (Input.GetKeyUp(jumpButton))
        {
            if (currentNumberOfJumps == maxNumberMultiJumps && groundedRemember > 0)
            {
                Jump();
                _animator.SetBool("Jumping", true);
            }
            else if (currentNumberOfJumps > 0 && currentNumberOfJumps < maxNumberMultiJumps 
                && groundedRemember <= 0)
            {
                Jump();
                _animator.SetBool("DoubleJumping", true);
            }
        }

        if (_modifier == null)
        {
            return;
        }

        if (Input.GetKeyDown(_modifier.useButton))
        {
            if (currentNumberOfJumps == 0)
            {
                _modifier.Activate();
                currentNumberOfJumps--;
            }
        }
    }


    private void Jump()
    {
        float jumpForce = jumpButtonHoldingTime > maxWeakJumpButtonHoldingTime ?
            Mathf.Min(maxJumpForce, weakJumpForce + jumpButtonHoldingTime * strongJumpTimeScaling) :
            weakJumpForce;

        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);

        jumpButtonHoldingTime = 0;
        currentNumberOfJumps--;
    }


    public void ResetJumps()
    {
        currentNumberOfJumps = maxNumberMultiJumps;
        jumpButtonHoldingTime = 0;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
