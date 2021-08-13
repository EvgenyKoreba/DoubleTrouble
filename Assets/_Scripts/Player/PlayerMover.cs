using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{

    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
    public class PlayerMover : MonoBehaviour
    {
        #region Fields
        [Header("Set in Inspector: Move Options")]
        [Range(0,1000)]
        [SerializeField] private float _speed = 500f;
        [SerializeField] private Vector2 _fallingSpeedBounds = new Vector2(100f, 100f);

        [Header("Set Dynamically"), Space(10)]
        [SerializeField] private bool _facingRight = true;
        [HideInInspector] public float X_direction;

        private Rigidbody2D _rigidBody;
        #endregion

        #region Properties
        public bool FacingRight
        {
            get => _facingRight;
            private set => _facingRight = value;
        }
        #endregion

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        public void MoveHorizontal(float xDirection)
        {
            X_direction = xDirection;
            FacingControl(xDirection);

            Vector2 velocity = _rigidBody.velocity;
            velocity.x = xDirection * _speed * Time.fixedDeltaTime;
            _rigidBody.velocity = velocity;
        }

        private void FacingControl(float xDirection)
        {
            if (xDirection > 0)
            {
                FacingRight = true;
            }
            else if (xDirection < 0)
            {
                FacingRight = false;
            }
            Flip();
        }

        private void Flip()
        {
            Vector3 angle = FacingRight ? Vector3.zero : new Vector3(0, -180, 0);
            transform.rotation = Quaternion.Euler(angle);
        }
    }

}
