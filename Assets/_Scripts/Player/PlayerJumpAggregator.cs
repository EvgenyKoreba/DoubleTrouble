using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJumpAggregator : MonoBehaviour, IPickUpModifierHandler, IPickUpItemHandler
{
    #region Fields
    [Header("       Set in Inspector")]
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
    [SerializeField] private bool _isJumping;


    [HideInInspector] public Rigidbody2D RigidBody;
    #endregion

    public bool IsGrounded
    {
        get => _isGrounded;
        private set
        {
            if (IsTouchedGroundWhenFall(value))
            {
                ResetJumps();
                //EventsHandler.RaiseEvent<ITouchingGroundWhenFall>(t => t.PlayerTouchedGround());
            }
            _isGrounded = value;
        }
    }

    private bool IsTouchedGroundWhenFall(bool isGroundedSet) => IsOnAir() && isGroundedSet;

    private bool IsOnAir() => IsGrounded == false;

    public void ResetJumps()
    {
        _currentNumberOfJumps = _maxNumberMultiJumps;
        _holdTimeOfTheJumpButton = 0;

        IsJumping = false;
    }

    public bool IsJumping { 
        get => _isJumping; 
        private set => _isJumping = value; 
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
        ResetJumps();
    }

    private void Update()
    {
        IsGrounded = _groundCheck.IsGrounded;
    }

    public void IncreaseJumpButtonHoldTime()
    {
        _holdTimeOfTheJumpButton += Time.deltaTime;
    }

    public void TryJump()
    {
        if (IsCanJump)
        {
            IsJumping = true;
            Jump();
        }
    }

    public void TryActivateModifier()
    {
        if (_modifier != null)
        {
            _modifier.ActivationAttempt(IsGrounded);
        }
    }

    public void DisableModifier()
    {
        _modifier.Disable();
    }

    private bool IsCanJump => _currentNumberOfJumps > 0 && (_modifier is null || _modifier?.IsActive == false);


    private void Jump()
    {
        float jumpForce = _holdTimeOfTheJumpButton > _maxWeakJumpButtonHoldingTime ?
            Mathf.Min(_maxJumpForce, _weakJumpForce + _holdTimeOfTheJumpButton * _strongJumpTimeScaling) :
            _weakJumpForce;

        NullifyVerticalVelocity();
        RigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

        _holdTimeOfTheJumpButton = 0;
        _currentNumberOfJumps--;
    }

    public float Gravity
    {
        get { return RigidBody.gravityScale; }
        set { RigidBody.gravityScale = value; }
    }

    public void NullifyVerticalVelocity()
    {
        Vector3 velocity = RigidBody.velocity;
        velocity.y = 0;
        RigidBody.velocity = velocity;
    }
}
