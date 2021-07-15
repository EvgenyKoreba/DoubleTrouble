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
    [SerializeField] private float _holdTimeOfTheJumpButton = 0.0f;
    [SerializeField] private bool _isGrounded;


    [HideInInspector] public Rigidbody2D RigidBody;
    private Animator _animator;
    #endregion

    public bool IsGrounded
    {
        get => _isGrounded;
        set
        {
            if (IsTouchedGroundWhenFall(value))
            {
                ResetJumps();
                //EventsHandler.RaiseEvent<ITouchingGroundWhenFall>(t => t.PlayerTouchedGround());
            }
            _isGrounded = value;
        }
    }

    private bool IsOnAir() => IsGrounded == false;

    private bool IsTouchedGroundWhenFall(bool isGroundedSet) => IsOnAir() && isGroundedSet;

    public void ResetJumps()
    {
        _currentNumberOfJumps = _maxNumberMultiJumps;
        _holdTimeOfTheJumpButton = 0;
    }

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

        if (IsJumpButtonHeldDown())
        {
            _holdTimeOfTheJumpButton += Time.deltaTime;
        }

        if (IsJumpButtonReleased())
        {
            if (IsCanJump)
            {
                Jump();
                if (IsOnAir())
                {
                    _animator.SetTrigger("MultiJump");
                }
            }
        }

        if (_modifier is null)
        {
            return;
        }

        if (IsModifierButtonPressed())
        {
            _modifier.TryActivate(IsGrounded);
        }
    }

    private bool IsJumpButtonHeldDown() => Input.GetKey(_jumpButton);

    private bool IsJumpButtonReleased() => Input.GetKeyUp(_jumpButton);

    private bool IsCanJump => _currentNumberOfJumps > 0 && (_modifier is null || _modifier?.IsActive == false);

    private bool IsModifierButtonPressed() => _modifier is null ? false : Input.GetKeyDown(_modifier.UseButton);

    private void Jump()
    {
        float jumpForce = _holdTimeOfTheJumpButton > _maxWeakJumpButtonHoldingTime ?
            Mathf.Min(_maxJumpForce, _weakJumpForce + _holdTimeOfTheJumpButton * _strongJumpTimeScaling) :
            _weakJumpForce;

        RigidBody.velocity = new Vector2(RigidBody.velocity.x, jumpForce);

        _holdTimeOfTheJumpButton = 0;
        _currentNumberOfJumps--;
    }

    public float Gravity
    {
        get { return RigidBody.gravityScale; }
        set { RigidBody.gravityScale = value; }
    }

    public void NullifyVerticalSpeed()
    {
        Vector3 velocity = RigidBody.velocity;
        velocity.y = 0;
        RigidBody.velocity = velocity;
    }
}
