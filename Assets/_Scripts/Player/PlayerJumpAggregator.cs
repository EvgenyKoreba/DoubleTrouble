using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerJumpAggregator : MonoBehaviour, IPickUpModifierHandler, IPickUpItemHandler
{

    #region Fields
    [Header("       Set in Inspector")]
    [SerializeField] private KeyCode _jumpButton = KeyCode.Space;
    [SerializeField] private float _weakJumpForce;
    [SerializeField] private float _maxJumpForce;
    [SerializeField] private float _maxWeakJumpButtonHoldingTime;
    [SerializeField] private float _strongJumpTimeScaling;
    [SerializeField] private int _maxNumberMultiJumps = 2;
    [SerializeField] private GroundCheck _groundCheck;
    

    [Header("       Set Dynamically"), Space(30)]
    [SerializeField] private Modifier _modifier;
    [SerializeField] private int _currentNumberOfJumps;
    [SerializeField] private float _jumpButtonHoldingTime = 0.0f;
    [SerializeField] private bool _isGrounded;


    [HideInInspector] public Rigidbody2D RigidBody;
    private Animator _animator;
    #endregion

    #region properties
    public bool IsGrounded
    {
        get => _isGrounded;
        set
        {
            if (IsGrounded == false && value == true)
            {
                ResetJumps();

            }
            _isGrounded = value;
        }
    }
    #endregion

    #region Events
    private void OnEnable()
    {
        EventsHandler.Subscribe(this);
    }
    private void OnDisable()
    {
        EventsHandler.Unsubscribe(this);
    }
    public void ModifierPickUped(Modifier modifier)
    {
        _modifier = modifier;
    }
    public void PickUpItem(IPickupableItem item)
    {
        try
        {
            MultiJump multijump = (MultiJump)item;
            _currentNumberOfJumps += multijump.JumpCount;
        }
        catch
        {
            throw new System.InvalidCastException("Not a multijump");
        }
    }
    #endregion

    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        ResetJumps();
    }

    private void Update()
    {
        IsGrounded = _groundCheck.IsGrounded;
        _animator.SetBool("isGrounded", IsGrounded);


        if (Input.GetKey(_jumpButton))
        {
            _jumpButtonHoldingTime += Time.deltaTime;
        }


        if (Input.GetKeyUp(_jumpButton))
        {
            // прыжок
            if (_currentNumberOfJumps > 0)
            {
                if (_modifier is null || _modifier?.IsActive == false)
                {
                    Jump();
                    if (!IsGrounded)
                    {
                        _animator.SetTrigger("MultiJump");
                    }
                }
            }
        }

        if (_modifier is null)
        {
            return;
        }

        if (Input.GetKeyDown(_modifier.UseButton))
        {
            _modifier.TryActivate(IsGrounded);
        }
    }

    private void Jump()
    {
        float jumpForce = _jumpButtonHoldingTime > _maxWeakJumpButtonHoldingTime ?
            Mathf.Min(_maxJumpForce, _weakJumpForce + _jumpButtonHoldingTime * _strongJumpTimeScaling) :
            _weakJumpForce;

        RigidBody.velocity = new Vector2(RigidBody.velocity.x, jumpForce);

        _jumpButtonHoldingTime = 0;
        _currentNumberOfJumps--;
    }

    public void ResetJumps()
    {
        // сюда
        _currentNumberOfJumps = _maxNumberMultiJumps;
        _jumpButtonHoldingTime = 0;
    }
}
