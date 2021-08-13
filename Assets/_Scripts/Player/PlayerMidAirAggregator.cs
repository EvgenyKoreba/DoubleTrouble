using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

namespace Project.Player
{

    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerModifier))]
    public class PlayerMidAirAggregator : MonoBehaviour, IPickUpItem
    {
        #region Fields
        [Header("       Set in Inspector")]
        [SerializeField] private float _weakJumpForce;
        [SerializeField] private float _maxJumpForce;
        [SerializeField] private float _maxWeakJumpButtonHoldingTime;
        [SerializeField] private float _strongJumpTimeScaleFactor;
        [SerializeField] private int _maxNumberMultiJumps = 2;
        [SerializeField] private GroundChecker _groundChecker;


        [Header("       Set Dynamically"), Space(30)]
        [SerializeField] private int _currentNumberOfJumps;
        [SerializeField] private float _holdTimeOfTheJumpButton = 0.0f;
        [SerializeField] private bool _isGrounded;
        [SerializeField] private bool _isJumping;


        private Rigidbody2D _rigidBody;
        private PlayerModifier _playerModifier;
        #endregion

        public bool IsGrounded
        {
            get => _isGrounded;
            private set {
                if (IsTouchedGroundWhenFall(value))
                {
                    ResetJumps();
                }
                _isGrounded = value;
            }
        }

        private bool IsTouchedGroundWhenFall(bool isGroundedSetValue) => IsOnAir() && isGroundedSetValue;

        private bool IsOnAir() => IsGrounded == false;

        public void ResetJumps()
        {
            _currentNumberOfJumps = _maxNumberMultiJumps;
            _holdTimeOfTheJumpButton = 0;

            IsJumping = false;
        }

        public bool IsJumping
        {
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

        public void PickUpItem(Item item)
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
            _playerModifier = GetComponent<PlayerModifier>();
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            ResetJumps();
        }

        private void Update()
        {
            IsGrounded = _groundChecker.IsGrounded;
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

        private bool IsCanJump => _currentNumberOfJumps > 0 &&
            (_playerModifier.CurrentModifier == null || _playerModifier.CurrentModifier?.IsActive == false);


        private void Jump()
        {
            float jumpForce = _holdTimeOfTheJumpButton > _maxWeakJumpButtonHoldingTime ?
            Mathf.Min(_maxJumpForce, _weakJumpForce + _holdTimeOfTheJumpButton * _strongJumpTimeScaleFactor) :
            _weakJumpForce;

            NullifyVerticalVelocity();
            _rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            _holdTimeOfTheJumpButton = 0;
            _currentNumberOfJumps--;
        }

        public void NullifyVerticalVelocity()
        {
            Vector3 velocity = _rigidBody.velocity;
            velocity.y = 0;
            _rigidBody.velocity = velocity;
        }
    }

}
